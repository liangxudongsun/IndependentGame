//核心机制
using UnityEngine;
using System.Collections;

public class CellBug:MonoBehaviour
{
    private Ability ability = new Ability();
    private Camera camera;
    private TapControl tapControl;
    
    private CellBug targetEnemy = null;
    private Meat targetMeat = null;
    private Grass targetGrass = null;

    public GameObject cellBugProfabs;

    void Awake()
    {
        tapControl = (GameObject.Find("tapControl") as GameObject).GetComponent<TapControl>();
        tapControl.AddCellBugNative(this);
        tapControl.AddCellBugAll(this);

        ability.setMine(this);
    }

    void Start()
    {

    }

    void Update()
    {
        if (!camera) return;
        UpdateStatus();
        UpdatePosition();
    }

    //移动
    private void Move(Vector3 destination)
    {
        //涉及到寻路,移动速度将设置为寻路的速度
        ability.targetPos = destination;
        ability.isArrive = false;
    }

    private void StopMove()
    {
        ability.targetPos = transform.position;
        ability.isArrive = true;
    }

    public void SetCamera(Camera camera)
    {
        this.camera = camera;
    }

    //攻击
    public void Attack()
    {
        //如果死亡则停止攻击
        if (!targetEnemy) return;
        float dis = Vector3.Magnitude(targetEnemy.transform.position - transform.position);
        if (dis <= 1) 
        {
            StopMove();
            int attackForce = this.GetAbility().GetGeneArray()[(int)Const.GenesEnum.AttackForceEnum].GetAttackForce(this);
            bool isLive = targetEnemy.GetAbility().SetPower(-attackForce);
            if (!isLive)
            {
                ability.status = Const.StutasEnum.IdleEnum;
                targetEnemy = null;
            }
        }
        else { Move(targetEnemy.transform.position); } 
    }

    //吃草
    public void EatPlant()
    {
        //需要调用基因查看
        if (!targetGrass) return;
        int powerFrom = this.GetAbility().GetGeneArray()[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 2) return;

        float dis = Vector3.Magnitude(targetGrass.transform.position - transform.position);
        if (dis <= 1) 
        { 
            StopMove(); 
            this.GetAbility().SetPower(targetGrass.GetPower());
            ability.status = Const.StutasEnum.IdleEnum;
            targetGrass = null;
            Destroy(targetGrass); 
        }
        else {Move(targetGrass.transform.position);} 
    }

    //吃肉
    public void EatMeat()
    {
        //需要调用基因查看,看能否吃肉,肉有没有毒,自己有没有抗毒性
        if (!targetMeat) return;
        int powerFrom = this.GetAbility().GetGeneArray()[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 0) return;

        float dis = Vector3.Magnitude(targetMeat.transform.position - transform.position);
        if (dis <= 1) 
        {
            StopMove();
            int antibiotic = this.GetAbility().GetGeneArray()[(int)Const.GenesEnum.AntibioticEnum].GetAntibiotic(this);
            if (antibiotic >= targetMeat.GetPoison()){this.GetAbility().SetPower(targetMeat.GetPower());}
            else{ this.Dead(Const.DeadEnum.PoisonEnum); }
            ability.status = Const.StutasEnum.IdleEnum;
            targetMeat = null;
            Destroy(targetMeat);
        }
        else { Move(targetMeat.transform.position); }
    }

    //召唤配偶
    public void CallMate()
    {
        //需要调用基因查看,播放相应声音
        ability.isResquest = true;
        ability.isMateSusess = false;
    }

    //配对成功,召唤者没有权利反对
    public void IWillYouMate(CellBug cellBug)
    {
        //求偶成功,停止召唤
        ability.isResquest = false;
        ability.isMateSusess = true;
        RaiseUpSeed(cellBug);
    }

    //接收到召唤
    public void CallProCast(CellBug cellBug)
    {
        //禁止接受其他召唤
        if (!ability.requestedList.Contains(cellBug))
        {
            ability.isRequested = true;
            ability.requestedList.Add(cellBug);
        }
    }

    //拒绝被召唤
    public void refuseedMate()
    {
        ability.isRequested = false;
    }

    public Ability GetAbility()
    {
        return ability;
    }

    //死亡
    public void Dead(Const.DeadEnum deadEnum)
    {
        tapControl.DeleteCellBugNative(this);
        tapControl.DeleteCellBugAll(this);
        Destroy(this.gameObject,0.5f);
    }

    public void TapCheck(Vector3 tapPosition)
    {
        //进行射线检测,看点到什么
        GameObject tapedObject = null;
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(tapPosition, Vector2.zero);

        if (hitInfo.collider != null)
        {
            tapedObject = hitInfo.collider.gameObject;

            if (Const.CellBugTag == tapedObject.tag && tapedObject != this)
            {
                targetEnemy = tapedObject.GetComponent<CellBug>();
                ability.status = Const.StutasEnum.AttackEnum;
            }
            else if (Const.FloorTag == tapedObject.tag)
            {
                ability.status = Const.StutasEnum.IdleEnum;
                Move(tapPosition);
            }
            else if (Const.GrassTag == tapedObject.tag)
            {
                targetGrass = tapedObject.GetComponent<Grass>();
                ability.status = Const.StutasEnum.EatPlantEnum;
            }
            else if (Const.MeatTag == tapedObject.tag)
            {
                targetMeat = tapedObject.GetComponent<Meat>();
                ability.status = Const.StutasEnum.EatMeatEnum; 
            }
        }
    }

    private void UpdateStatus()
    {
        switch (this.GetAbility().status)
        {
        case Const.StutasEnum.IdleEnum:
        	break;
        case Const.StutasEnum.ReceviceMataEnum:
            break;
        case Const.StutasEnum.SearchMateEnum:
            break;
        case Const.StutasEnum.AttackEnum:
            Attack();
            break;
        case Const.StutasEnum.EatMeatEnum:
            EatMeat();
            break;
        case Const.StutasEnum.EatPlantEnum:
            EatPlant();
            break;
        }
    }

    private void UpdatePosition()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        if (!ability.isArrive)
        {
            Vector3 dir = ability.targetPos - transform.position;
            dir = dir.normalized; dir.z = 0;

            float angle = Vector3.Angle(new Vector3(0, -1, 0), dir);
            if (dir.x < 0) angle = 360 - angle;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, angle, Time.deltaTime * 10));

            transform.position += dir * ability.speed * Time.deltaTime;
            if (Vector3.Distance(ability.targetPos, transform.position) <= 0.1f)
            {
                ability.isArrive = true;
            }
        }
    }

    //产生后代
    private void RaiseUpSeed(CellBug cellBug)
    {
        int birthNum = GetAbility().GetGeneArray()[(int)Const.GenesEnum.BrithNumEnum].GetBirthNum(this);
        for (int i = 0; i < birthNum; i++)
        {
            GameObject bug = Instantiate(cellBugProfabs, this.transform.position, Quaternion.identity) as GameObject;
            CellBug temCellBug = bug.GetComponent<CellBug>();

            int[] lineOne = this.GetAbility().GetDna().DnaVariation();
            int[] lineTwo = cellBug.GetAbility().GetDna().DnaVariation();

            temCellBug.GetAbility().SetDNA(lineOne, lineTwo);
        }
    }
}