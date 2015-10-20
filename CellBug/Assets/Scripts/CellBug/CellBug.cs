//���Ļ���
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

    //�ƶ�
    private void Move(Vector3 destination)
    {
        //�漰��Ѱ·,�ƶ��ٶȽ�����ΪѰ·���ٶ�
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

    //����
    public void Attack()
    {
        //���������ֹͣ����
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

    //�⵽����
    public bool Attacked(CellBug cellBug)
    {
        int attackForce = Const.geneArray[(int)Const.GenesEnum.AttackForceEnum].GetAttackForce(cellBug);
        bool isLive = ability.SetPower(-attackForce);
        if (!isLive) this.Dead(Const.DeadEnum.KilledEnum);
        if (isLive) this.ReadyAttack(cellBug.gameObject);
        return isLive;
    }

    //�Բ�
    public void EatPlant()
    {
        //��Ҫ���û���鿴
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

    //����
    public void EatMeat()
    {
        //��Ҫ���û���鿴,���ܷ����,����û�ж�,�Լ���û�п�����
        if (!targetFood) return;
        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(this);
        if (powerFrom == 0)
        {
            Debug.Log("�㻹û�н�����ʳ������");
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

    //�ٻ���ż
    public void CallMate()
    {
        //��Ҫ���û���鿴,������Ӧ����
        ability.isResquest = true;
        ability.isMateSusess = false;
        ability.status = Const.StutasEnum.SearchMateEnum;
    }

    //ֹͣ�ٻ�(�ٻ���ʱ������)
    public void StopCallMate()
    {
        ability.isResquest = false;
        ability.isMateSusess = true;
        ability.status = Const.StutasEnum.IdleEnum;
    }

    //��Գɹ�,���Ծ��Ƿ�����������
    public void IWillYouMate(CellBug cellBug)
    {
        ability.nowMateCellBug = cellBug;
        ability.requestedList.Add(cellBug);
        //��ż�ɹ�,ֹͣ�ٻ�
        ability.isResquest = false;
        ability.isMateSusess = true;
    }

    //ͬ�����
    public void AcceptMate()
    {
        RaiseUpSeed(ability.nowMateCellBug);
        ability.status = Const.StutasEnum.IdleEnum;
        ability.nowMateCellBug = null;
    }

    //��ͬ�����
    public void RefuseMate()
    {
        ability.status = Const.StutasEnum.IdleEnum;
        ability.nowMateCellBug = null;
    }

    //���յ��ٻ�
    public void CallProCast(CellBug cellBug)
    {
        //��ֹ���������ٻ�
        if (!ability.requestedList.Contains(cellBug))
        {
            ability.isRequested = true;
            ability.requestedList.Add(cellBug);
        }
        ability.status = Const.StutasEnum.ReceviceMataEnum;
    }

    //�ܾ����ٻ�
    public void refuseCallMate(CellBug cellBug)
    {
        ability.isRequested = false;
        ability.status = Const.StutasEnum.IdleEnum; 
    }

    //�����ٻ�
    public void AcceptCallMate(CellBug cellBug)
    {
        cellBug.IWillYouMate(this);
        ability.isRequested = false;
        ability.status = Const.StutasEnum.IdleEnum;
    }

    //����Ѱ����ż״̬
    public void SearchMateStatus()
    {

    }

    //���ڽ��յ��ٻ�װ̬
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

    //����
    public void Dead(Const.DeadEnum deadEnum)
    {
        gameControl.DeleteCellBugAll(this);
        Destroy(this.gameObject,0.5f);
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

    //�������
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