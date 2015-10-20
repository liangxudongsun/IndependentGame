//核心机制
using UnityEngine;
using System.Collections;

public class CellBug:MonoBehaviour
{
    private Ability ability = new Ability();
    private Camera camera;
    private GameControl gameControl;
    
    private CellBug targetEnemy = null;
    private Food targetFood = null;
    private float timeforpower = Const.timeForPowerDelete;

    private bool isAI = true;
    private AIControl aiControl = new AIControl();
    public GameObject cellBugProfabs;

    void Awake()
    {
        gameControl = (GameObject.Find("gameControl") as GameObject).GetComponent<GameControl>();
        gameControl.AddCellBugAll(this);
        ability.setMine(this);
    }

    void Start()
    {

    }

    void Update()
    {
        if (!isAI)
        {
            UpdateStatus();
            UpdatePosition();
        }
        else
        {
            aiControl.UpdateStatus(this);
            aiControl.UpdatePosition(this);
        }

        ClearMateList();
        UpdatePowerForTime();
    }

    private void ClearMateList()
    {
        ability.timeForClearList-= Time.deltaTime;
        if (ability.timeForClearList <= 0)
        {
            ability.timeForClearList = Const.timeForClearList;
            ability.requestedList.Clear();
        }
    }

    private void UpdatePowerForTime()
    {
        timeforpower -= Time.deltaTime;
        if (timeforpower <= 0)
        {
            timeforpower = Const.timeForPowerDelete;
            if (!ability.SetPower(-1)) this.Dead(Const.DeadEnum.StarveEnum);
        }
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
        isAI = false;
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
            bool isLive = targetEnemy.Attacked(this);
            if (!isLive)
            {
                ability.status = Const.StutasEnum.IdleEnum;
                targetEnemy = null;
            }
        }
        else { Move(targetEnemy.transform.position); } 
    }

    //遭到攻击
    public bool Attacked(CellBug cellBug)
    {
        int attackForce = Const.geneArray[(int)Const.GenesEnum.AttackForceEnum].GetAttackForce(cellBug);
        bool isLive = ability.SetPower(-attackForce);
        if (!isLive) this.Dead(Const.DeadEnum.KilledEnum);
        if (isLive) this.ReadyAttack(cellBug.gameObject);
        return isLive;
    }

    //吃草
    public void EatPlant()
    {
        //需要调用基因查看
        if (!targetFood) return;
        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 2) return;

        float dis = Vector3.Magnitude(targetFood.transform.position - transform.position);
        if (dis <= 1) 
        { 
            StopMove();
            this.GetAbility().SetPower(targetFood.GetPower());
            ability.status = Const.StutasEnum.IdleEnum;
            targetFood.Eated();
            targetFood = null;
        }
        else { Move(targetFood.transform.position); } 
    }

    //吃肉
    public void EatMeat()
    {
        //需要调用基因查看,看能否吃肉,肉有没有毒,自己有没有抗毒性
        if (!targetFood) return;
        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 0)
        {
            Debug.Log("你还没有进化出食肉能力");
            targetFood = null;
            ability.status = Const.StutasEnum.IdleEnum;
            return; 
        }

        float dis = Vector3.Magnitude(targetFood.transform.position - transform.position);
        if (dis <= 1) 
        {
            StopMove();
            int antibiotic = Const.geneArray[(int)Const.GenesEnum.AntibioticEnum].GetAntibiotic(this);
            if (antibiotic >= targetFood.GetComponent<Meat>().GetPoison())
            {
                this.GetAbility().SetPower(targetFood.GetPower());
            }
            else
            { 
                this.Dead(Const.DeadEnum.PoisonEnum); 
            }
            ability.status = Const.StutasEnum.IdleEnum;
            targetFood.Eated();
            targetFood = null;
        }
        else { Move(targetFood.transform.position); }
    }

    //召唤配偶
    public void CallMate()
    {
        //需要调用基因查看,播放相应声音
        ability.isResquest = true;
        ability.isMateSusess = false;
        ability.status = Const.StutasEnum.SearchMateEnum;
    }

    //停止召唤(召唤有时间限制)
    public void StopCallMate()
    {
        ability.isResquest = false;
        ability.isMateSusess = true;
        ability.status = Const.StutasEnum.IdleEnum;
    }

    //配对成功,反对就是放弃本次生育
    public void IWillYouMate(CellBug cellBug)
    {
        ability.nowMateCellBug = cellBug;
        ability.requestedList.Add(cellBug);
        //求偶成功,停止召唤
        ability.isResquest = false;
        ability.isMateSusess = true;
    }

    //同意配对
    public void AcceptMate()
    {
        RaiseUpSeed(ability.nowMateCellBug);
        ability.status = Const.StutasEnum.IdleEnum;
        ability.nowMateCellBug = null;
    }

    //不同意配对
    public void RefuseMate()
    {
        ability.status = Const.StutasEnum.IdleEnum;
        ability.nowMateCellBug = null;
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
        ability.status = Const.StutasEnum.ReceviceMataEnum;
    }

    //拒绝被召唤
    public void refuseCallMate(CellBug cellBug)
    {
        ability.isRequested = false;
        ability.status = Const.StutasEnum.IdleEnum; 
    }

    //接受召唤
    public void AcceptCallMate(CellBug cellBug)
    {
        cellBug.IWillYouMate(this);
        ability.isRequested = false;
        ability.status = Const.StutasEnum.IdleEnum;
    }

    //处于寻找配偶状态
    public void SearchMateStatus()
    {

    }

    //处于接收到召唤装态
    public void RecieveCallMateStatus()
    {

    }

    public Ability GetAbility()
    {
        return ability;
    }

    public GameControl GetGameControl()
    {
        return gameControl;
    }

    //死亡
    public void Dead(Const.DeadEnum deadEnum)
    {
        gameControl.DeleteCellBugAll(this);
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

            if (Const.CellBugTag == tapedObject.tag && tapedObject != this.gameObject)
            {
                ReadyAttack(tapedObject);
            }
            else if (Const.FloorTag == tapedObject.tag)
            {
                if (ability.status != Const.StutasEnum.SearchMateEnum 
                    && ability.status != Const.StutasEnum.ReceviceMataEnum)
                    ability.status = Const.StutasEnum.IdleEnum;
                Move(tapPosition);
            }
            else if (Const.FoodTag == tapedObject.tag)
            {
                ReadyEat(tapedObject);
            }
        }
    }

    public void ReadyAttack(GameObject tapedObject)
    {
        targetEnemy = tapedObject.GetComponent<CellBug>();
        ability.status = Const.StutasEnum.AttackEnum;
    }

    public void ReadyEat(GameObject tapedObject)
    {
        targetFood = tapedObject.GetComponent<Food>();
        if (targetFood.GetFoodType() == Const.FoodEnum.GrassEnum)
            ability.status = Const.StutasEnum.EatPlantEnum;
        if (targetFood.GetFoodType() == Const.FoodEnum.MeatEnum)
            ability.status = Const.StutasEnum.EatMeatEnum;
    }

    private void UpdateStatus()
    {
        switch (this.GetAbility().status)
        {
        case Const.StutasEnum.IdleEnum:
        	break;
        case Const.StutasEnum.ReceviceMataEnum:
            RecieveCallMateStatus();
            break;
        case Const.StutasEnum.SearchMateEnum:
            SearchMateStatus();
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
        if (!camera) return;
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
        int birthNum = Const.geneArray[(int)Const.GenesEnum.BrithNumEnum].GetBirthNum(this);
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