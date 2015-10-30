//���Ļ���
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellBug:MonoBehaviour
{
    private Ability ability = new Ability();
    private GameControl gameControl;
    
    private CellBug targetEnemy = null;
    private Food targetFood = null;
    private CellBug targetMateCellBug = null;  //��ǰ�����ٻ�����ż

    private float timeforpower = Const.TimeForPowerDelete;  //ÿ���������Ѫ
    private float geneforPower = Const.GeneForPowerUpdate;

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

        int seed = (int)System.DateTime.Now.Ticks + ID * 10;
        System.Random random = new System.Random(seed);
        int time = random.Next(1, 101);
        ability.SetLiveTime(time);
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
        bloodSlider.value = ability.GetPower() / Const.MaxPower;
    }

    void Update()
    {
        if (!ability.GetIsLive()) return;

        if (!isAI){UpdateStatus();UpdatePosition();}
        else{aiControl.UpdateStatus(this);aiControl.UpdatePosition(this);}

        ability.UpdateMateTime(Time.deltaTime);
        ability.liveTimeModify(Time.deltaTime);
        UpdatePowerForTime();
        UpdatePowerForGene();
        UpDateTitle();
    }

    private void UpdatePowerForTime()
    {
        timeforpower -= Time.deltaTime;
        if (timeforpower <= 0)
        {
            timeforpower = Const.TimeForPowerDelete;
            if (!ability.SetPower(-1.0f)) this.Dead(Const.DeadEnum.StarveEnum);
        }
    }

    private void UpdatePowerForGene()
    {
        geneforPower -= Time.deltaTime;
        if (geneforPower <= 0)
        {
            geneforPower = Const.GeneForPowerUpdate;
            float photosynthesisPower = Const.GeneArray[(int)Const.GenesEnum.PhotosynthesisEnum].GetPhotosynthesis(this);
            float custom = 0.0f;
            for (int i = 0; i < Const.GeneArray.Length; i++)
            {
                custom += Const.GeneArray[i].GetPowerCustom(this);
            }

            if (!ability.SetPower(-custom + photosynthesisPower)) this.Dead(Const.DeadEnum.StarveEnum);
        }
    }

    //�ƶ�
    public void Move(Vector3 destination)
    {
        //�漰��Ѱ·,�ƶ��ٶȽ�����ΪѰ·���ٶ�
        destination = new Vector3(destination.x,destination.y,this.transform.position.z);
        ability.targetPos = destination;
        ability.isArrive = false;
    }

    private void StopMove()
    {
        ability.targetPos = transform.position;
        ability.isArrive = true;
    }

    public void SetIsAI(bool isAI)
    {
        this.isAI = isAI;
    }

    //����
    public void Attack()
    {
        //���������ֹͣ����
        if (!targetEnemy || !targetEnemy.GetAbility().GetIsLive()) 
        {
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            gameControl.AlertVision(this,"����������");
            return;
        }
        float dis = Vector3.Magnitude(new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y, transform.position.z)
                    - transform.position);
        if (dis <= Const.AttackDis) 
        {
            StopMove();
            bool isLive = targetEnemy.Attacked(this);
            if (!isLive)
            {
                ability.SetStatus(Const.StutasEnum.IdleEnum);
                gameControl.AlertVision(this,"ɱ������");
            }
        }
        else { Move(targetEnemy.transform.position); } 
    }

    //�⵽����
    public bool Attacked(CellBug cellBug)
    {
        int attackForce = Const.GeneArray[(int)Const.GenesEnum.AttackForceEnum].GetAttackForce(cellBug);
        bool isLive = ability.SetPower(-attackForce);
        if (!isLive) this.Dead(Const.DeadEnum.KilledEnum);
        if (isLive && this.ability.GetStatus() != Const.StutasEnum.AttackEnum) this.ReadyAttack(cellBug.gameObject);
        return isLive;
    }

    //�Բ�
    public void EatPlant()
    {
        //��Ҫ���û���鿴
        if (!targetFood)
        {
            StopMove();
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            return;
        }

        int powerFrom = Const.GeneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 2)
        {
            gameControl.AlertVision(this,"���ѽ���Ϊ��ȫʳ�⶯��");
            return;
        }

        float dis = Vector3.Magnitude(new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, transform.position.z) 
            - transform.position);
        if (dis <= Const.FoodDis) 
        { 
            StopMove();
            this.GetAbility().SetPower(targetFood.GetPower());
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            targetFood.Eated();
            targetFood = null;
        }
        else { Move(targetFood.transform.position); } 
    }

    //����
    public void EatMeat()
    {
        //��Ҫ���û���鿴,���ܷ����,����û�ж�,�Լ���û�п�����
        if (!targetFood)
        {
            StopMove();
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            return;
        }

        int powerFrom = Const.GeneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 0)
        {
            gameControl.AlertVision(this,"����û�н�����ʳ������");
            targetFood = null;
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            return; 
        }

        float dis = Vector3.Magnitude(new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, transform.position.z)
            - transform.position);
        if (dis <= Const.FoodDis) 
        {
            StopMove();
            int antibiotic = Const.GeneArray[(int)Const.GenesEnum.AntibioticEnum].GetAntibiotic(this);
            if (antibiotic >= targetFood.GetComponent<Meat>().GetPoison())
            {
                this.GetAbility().SetPower(targetFood.GetPower());
            }
            else
            {
                ability.SetIsLive(false);
                this.Dead(Const.DeadEnum.PoisonEnum); 
            }
            ability.SetStatus(Const.StutasEnum.IdleEnum);
            targetFood.Eated();
            targetFood = null;
        }
        else { Move(targetFood.transform.position); }
    }

    //����Ѱ����ż״̬
    public void SearchMateStatus()
    {
        //���������ֹͣѰ��
        if (!targetMateCellBug)
        {
            StopMove();
            stopMate();
            return;
        }

        float dis = Vector3.Magnitude(new Vector3(targetMateCellBug.transform.position.x, targetMateCellBug.transform.position.y, 
            transform.position.z)
            - transform.position);
        if (dis <= Const.MateDis)
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

    //ֹͣѰ����ż
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

    //����
    public void Dead(Const.DeadEnum deadEnum)
    {
        switch (deadEnum)
        {
            case Const.DeadEnum.KilledEnum:
                gameControl.AlertVision(this, "����ɱ����!");
                break;
            case Const.DeadEnum.PoisonEnum:
                gameControl.AlertVision(this, "����������!");
                break;
            case Const.DeadEnum.StarveEnum:
                gameControl.AlertVision(this, "����������!");
                break;
            case Const.DeadEnum.TimeRunOutEnum:
                gameControl.AlertVision(this, "����������!");
                break;
        }

        this.GetComponent<CellBugMusic>().PlayDeadMusic(deadEnum);

        GameObject obj = Instantiate(meatProfabs, new Vector3(transform.position.x, transform.position.y, meatProfabs.transform.position.z), Quaternion.identity) as GameObject;
        Meat meat = obj.GetComponent<Meat>();
        meat.SetPoison(Const.GeneArray[(int)Const.GenesEnum.PoisonEnum].GetPoison(this));

        if (titleLabel) Destroy(titleLabel.gameObject);
        if (bloodSlider) Destroy(bloodSlider.gameObject);

        gameControl.DeleteCellBugAll(this);
        Destroy(this.gameObject,1.5f);
    }

    public void TapCheck(Vector3 tapPosition)
    {
        //�������߼��,���㵽ʲô
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
                    if (ability.GetCanMate())
                    {
                        if (gameControl.GetCellBugNum() >= Const.EnvironmentalCapacity)
                        {
                            gameControl.AlertVision(this, "�Ѵﵽ������������,���ܲ��ܷ��ܺ��");
                        }
                        else
                        {
                            ReadyMate(tapedObject);
                        }
                    }
                    else gameControl.AlertVision(this, "δ��������");
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

        this.GetComponent<CellBugMusic>().PlayMateMusic(Const.GeneArray[(int)Const.GenesEnum.SongEnum].GetMusic(this));
    }

    public void ReadyAttack(GameObject tapedObject)
    {
        targetEnemy = tapedObject.GetComponent<CellBug>();
        ability.SetStatus(Const.StutasEnum.AttackEnum);

        this.GetComponent<CellBugMusic>().PlayAttackMusic();
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
        if (isAI) return;

        float x = Mathf.Lerp(Camera.main.transform.position.x, transform.position.x, Time.deltaTime * 5);
        float y = Mathf.Lerp(Camera.main.transform.position.y, transform.position.y, Time.deltaTime * 5);
        float z = Camera.main.transform.position.z;
        if (x <= -Const.DisMap || x >= Const.DisMap) x = Camera.main.transform.position.x;
        if (y <= -Const.DisMap || y >= Const.DisMap) y = Camera.main.transform.position.y;
        Camera.main.transform.position = new Vector3(x, y, z);

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

    //�������
    private void RaiseUpSeed(CellBug cellBug)
    {
        ability.SetStatus(Const.StutasEnum.IdleEnum);

        int birthNum = Const.GeneArray[(int)Const.GenesEnum.BrithNumEnum].GetBirthNum(this);
        for (int i = 0; i < birthNum; i++)
        {
            if (gameControl.GetCellBugNum() >= Const.EnvironmentalCapacity)
                return;

            GameObject bug = Instantiate(cellBugProfabs, this.transform.position, Quaternion.identity) as GameObject;
            CellBug temCellBug = bug.GetComponent<CellBug>();

            int[] lineOne = this.GetAbility().GetDna().DnaVariation();
            int[] lineTwo = cellBug.GetAbility().GetDna().DnaVariation();
            temCellBug.GetAbility().SetDNA(lineOne, lineTwo);
            temCellBug.GetAbility().SetGroup(this.ability.GetGroup());
        }
    }
}