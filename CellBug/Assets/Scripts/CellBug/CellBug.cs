//核心机制
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellBug:MonoBehaviour
{
    private Ability ability = new Ability();
    private Camera camera;
    private GameControl gameControl;
    
    private CellBug targetEnemy = null;
    private Food targetFood = null;
    private CellBug targetMateCellBug = null;  //当前主动召唤的配偶
    private float timeforpower = Const.timeForPowerDelete;  //每隔多少秒减血

    private bool isAI = true;
    private AIControl aiControl = new AIControl();

    public GameObject cellBugProfabs;
    public GameObject meatProfabs;

    public GameObject titleProfabs;
    public GameObject bloodProfabs;
    private UILabel titleLabel = null;
    private UISlider bloodSlider = null;

    private static int ID = 0;

    void Awake()
    {
        ability.setMine(this);

        ID++;
        ability.SetId(ID);
    }

    void Start()
    {
        gameControl = (GameObject.Find("gameControl") as GameObject).GetComponent<GameControl>();
        gameControl.AddCellBugAll(this);

        GameObject gameObjectTitle = Instantiate(titleProfabs, transform.position + new Vector3(0,1,-1), Quaternion.identity) as GameObject;
        titleLabel = gameObjectTitle.GetComponent<UILabel>();
        titleLabel.text = Const.GroupName[(int)ability.GetGroup()] + "id:" +ability.GetId();

        GameObject gameObjectBlood = Instantiate(bloodProfabs,transform.position + new Vector3(0,1,-1),Quaternion.identity) as GameObject;
        bloodSlider = gameObjectBlood.GetComponent<UISlider>();
        bloodSlider.value = ability.GetPower() / Const.maxPower;
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

        ability.UpdateMateTime(Time.deltaTime);
        UpdatePowerForTime();
        UpDateTitle();
    }

    private void UpdatePowerForTime()
    {
        timeforpower -= Time.deltaTime;
        if (timeforpower <= 0)
        {
            timeforpower = Const.timeForPowerDelete;
            float photosynthesisPower = Const.geneArray[(int)Const.GenesEnum.PhotosynthesisEnum].GetPhotosynthesis(this);
            if (!ability.SetPower(-1.0f + photosynthesisPower)) this.Dead(Const.DeadEnum.StarveEnum);
        }
    }

    //移动
    public void Move(Vector3 destination)
    {
        //涉及到寻路,移动速度将设置为寻路的速度
        destination = new Vector3(destination.x,destination.y,this.transform.position.z);
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
        if (camera == null) isAI = true;
        else isAI = false;
    }

    //攻击
    public void Attack()
    {
        //如果死亡则停止攻击
        if (!targetEnemy) 
        {
            gameControl.AlertVision(this,"敌人已死亡");
            return;
        }
        float dis = Vector3.Magnitude(new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y, transform.position.z)
                    - transform.position);
        if (dis <= 1) 
        {
            StopMove();
            bool isLive = targetEnemy.Attacked(this);
            if (!isLive)
            {
                ability.SetStatus(Const.StutasEnum.IdleEnum);
                targetEnemy = null;
                gameControl.AlertVision(this,"杀死敌人");
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
        if (isLive && this.ability.GetStatus() != Const.StutasEnum.AttackEnum) this.ReadyAttack(cellBug.gameObject);
        return isLive;
    }

    //吃草
    public void EatPlant()
    {
        //需要调用基因查看
        if (!targetFood) return;
        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 2)
        {
            gameControl.AlertVision(this,"您已进化为完全食肉动物");
            return;
        }

        float dis = Vector3.Magnitude(new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, transform.position.z) 
            - transform.position);
        if (dis <= 2) 
        { 
            StopMove();
            this.GetAbility().SetPower(targetFood.GetPower());
            ability.SetStatus(Const.StutasEnum.IdleEnum);
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
            gameControl.AlertVision(this,"您还没有进化出食肉能力");
            targetFood = null;
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            return; 
        }

        float dis = Vector3.Magnitude(new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, transform.position.z)
            - transform.position);
        if (dis <= 2) 
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
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            targetFood.Eated();
            targetFood = null;
        }
        else { Move(targetFood.transform.position); }
    }

    //处于寻找配偶状态
    public void SearchMateStatus()
    {
        //如果死亡则停止寻找
        if (!targetMateCellBug)
        {
            StopMove();
            stopMate();
            return;
        }

        float dis = Vector3.Magnitude(new Vector3(targetMateCellBug.transform.position.x, targetMateCellBug.transform.position.y, 
            transform.position.z)
            - transform.position);
        if (dis <= 1)
        {
            StopMove();
            RaiseUpSeed(targetMateCellBug);
            stopMate();
        }
        else 
        {
            Move(targetMateCellBug.transform.position); 
        } 
    }

    //停止寻找配偶
    private void stopMate()
    {
        targetMateCellBug = null;
        ability.SetStatus(Const.StutasEnum.IdleEnum);
        ability.SetCanMate(false);
    }

    public Ability GetAbility()
    {
        return ability;
    }

    public GameControl GetGameControl()
    {
        return gameControl;
    }

    public UISlider GetBloodSlider()
    {
        return bloodSlider;
    }

    //死亡
    public void Dead(Const.DeadEnum deadEnum)
    {
        GameObject obj = Instantiate(meatProfabs, new Vector3(transform.position.x, transform.position.y, meatProfabs.transform.position.z), Quaternion.identity) as GameObject;
        Meat meat = obj.GetComponent<Meat>();
        meat.SetPoison(Const.geneArray[(int)Const.GenesEnum.PoisonEnum].GetPoison(this));

        if (titleLabel) Destroy(titleLabel.gameObject);
        if (bloodSlider) Destroy(bloodSlider.gameObject);

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
                if (tapedObject.GetComponent<CellBug>().GetAbility().GetGroup() != ability.GetGroup())
                {
                    ReadyAttack(tapedObject);
                }

                if (tapedObject.GetComponent<CellBug>().GetAbility().GetGroup() == ability.GetGroup())
                {
                    if (ability.GetCanMate()) ReadyMate(tapedObject);
                    else gameControl.AlertVision(this, "未到发情期");
                }
            }

            if (Const.FoodTag == tapedObject.tag)
            {
                ReadyEat(tapedObject);
            }
        }
        else
        {
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            Move(tapPosition);
        }
    }

    public void ReadyMate(GameObject tapedObject)
    {
        targetMateCellBug = tapedObject.GetComponent<CellBug>() as CellBug;
        ability.SetStatus(Const.StutasEnum.SearchMateEnum);
        ability.SetCanMate(false);
    }

    public void ReadyAttack(GameObject tapedObject)
    {
        targetEnemy = tapedObject.GetComponent<CellBug>();
        ability.SetStatus(Const.StutasEnum.AttackEnum);
    }

    public void ReadyEat(GameObject tapedObject)
    {
        targetFood = tapedObject.GetComponent<Food>();
        if (targetFood.GetFoodType() == Const.FoodEnum.GrassEnum)
            ability.SetStatus(Const.StutasEnum.EatPlantEnum);
        if (targetFood.GetFoodType() == Const.FoodEnum.MeatEnum)
            ability.SetStatus(Const.StutasEnum.EatMeatEnum);
    }

    private void UpdateStatus()
    {
        switch (this.GetAbility().GetStatus())
        {
        case Const.StutasEnum.IdleEnum:
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
        
        float x = Mathf.Lerp(camera.transform.position.x, transform.position.x,Time.deltaTime * 5);
        float y = Mathf.Lerp(camera.transform.position.y, transform.position.y, Time.deltaTime * 5);
        float z = camera.transform.position.z;
        camera.transform.position = new Vector3(x,y,z);

        if (!ability.isArrive)
        {
            Vector3 dir = ability.targetPos - transform.position;
            dir = dir.normalized; dir.z = 0;

            float angle = Vector3.Angle(new Vector3(0, -1, 0), dir);
            if (dir.x < 0) angle = 360 - angle;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, angle, Time.deltaTime * 10));

            transform.position += dir * ability.GetSpeed() * Time.deltaTime;
            if (Vector3.Distance(ability.targetPos, transform.position) <= 0.1f)
            {
                ability.isArrive = true;
            }
        }
    }

    private void UpDateTitle()
    {
        if (titleLabel)
           titleLabel.transform.position = UICamera.mainCamera.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(transform.position)) + new Vector3(0,0.2f,0); 
        if (bloodSlider)
           bloodSlider.transform.position = UICamera.mainCamera.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(transform.position)) + new Vector3(0,0.1f,0);
    }

    //产生后代
    private void RaiseUpSeed(CellBug cellBug)
    {
        ability.SetStatus(Const.StutasEnum.IdleEnum);

        int birthNum = Const.geneArray[(int)Const.GenesEnum.BrithNumEnum].GetBirthNum(this);
        for (int i = 0; i < birthNum; i++)
        {
            GameObject bug = Instantiate(cellBugProfabs, this.transform.position, Quaternion.identity) as GameObject;
            CellBug temCellBug = bug.GetComponent<CellBug>();

            int[] lineOne = this.GetAbility().GetDna().DnaVariation();
            int[] lineTwo = cellBug.GetAbility().GetDna().DnaVariation();
            temCellBug.GetAbility().SetDNA(lineOne, lineTwo);
            temCellBug.GetAbility().SetGroup(this.ability.GetGroup());
        }
    }
}