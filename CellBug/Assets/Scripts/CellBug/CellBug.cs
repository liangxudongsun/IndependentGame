//���Ļ���
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
    private CellBug targetMateCellBug = null;  //��ǰ�����ٻ�����ż
    private float timeforpower = Const.timeForPowerDelete;

    private bool isAI = true;
    private AIControl aiControl = new AIControl();

    public GameObject cellBugProfabs;
    public GameObject meatProfabs;
    public GameObject titleProfabs;

    private UILabel titleLabel = null;
    private static int ID = 0;
    void Awake()
    {
        gameControl = (GameObject.Find("gameControl") as GameObject).GetComponent<GameControl>();
        gameControl.AddCellBugAll(this);
        ability.setMine(this);

        ID++;
        ability.id = ID;
    }

    void Start()
    {
        GameObject gameObject = Instantiate(titleProfabs, transform.position + new Vector3(0,1,-1), Quaternion.identity) as GameObject;
        titleLabel = gameObject.GetComponent<UILabel>();
        titleLabel.text = Const.GroupName[(int)ability.cellBugGroup] + ability.id;
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
        ClearMateList();
        UpdatePowerForTime();
        UpDateTitle();
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

    public void SetCamera(Camera camera)
    {
        this.camera = camera;
        if (camera == null) isAI = true;
        else isAI = false;
    }

    //����
    public void Attack()
    {
        //���������ֹͣ����
        if (!targetEnemy) return;
        float dis = Vector3.Magnitude(new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y, transform.position.z)
                    - transform.position);
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

        float dis = Vector3.Magnitude(new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, transform.position.z) 
            - transform.position);
        if (dis <= 2) 
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

        float dis = Vector3.Magnitude(new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, transform.position.z)
            - transform.position);
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

    //���뷢����
    public void StartCallMate()
    {

    }

    //�������
    public void StopCallMate()
    {

    }
    
    //���յ��ٻ�
    public void CallProCast(CellBug cellBug)
    {
        if (!ability.requestedList.Contains(cellBug))
        {
            ability.requestedList.Add(cellBug);
        }
    }

    //�ܾ����ٻ�
    public void refuseCallMate()
    {
        ability.status = Const.StutasEnum.IdleEnum; 
    }

    //�����ٻ�
    public void AcceptCallMate()
    {
        ability.status = Const.StutasEnum.IdleEnum;
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
        if (dis <= 1)
        {
            StopMove();
            targetMateCellBug.CallProCast(this);
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
        ability.status = Const.StutasEnum.IdleEnum;
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

    //����
    public void Dead(Const.DeadEnum deadEnum)
    {
        GameObject obj = Instantiate(meatProfabs, new Vector3(transform.position.x, transform.position.y, meatProfabs.transform.position.z), Quaternion.identity) as GameObject;
        Destroy(obj, 5.0f);

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
                if (tapedObject.GetComponent<CellBug>().GetAbility().cellBugGroup != ability.cellBugGroup)
                {
                    ReadyAttack(tapedObject);
                }

                if (tapedObject.GetComponent<CellBug>().GetAbility().cellBugGroup == ability.cellBugGroup
                    && ability.GetCanMate())
                {
                    ReadyMate(tapedObject);
                }
            }

            if (Const.FoodTag == tapedObject.tag)
            {
                ReadyEat(tapedObject);
            }
        }
        else
        {
            if (ability.status != Const.StutasEnum.SearchMateEnum
                && ability.status != Const.StutasEnum.ReceviceMataEnum)
                ability.status = Const.StutasEnum.IdleEnum;
            Move(tapPosition);
        }
    }

    public void ReadyMate(GameObject tapedObject)
    {
        targetMateCellBug = tapedObject.GetComponent<CellBug>() as CellBug;
        ability.status = Const.StutasEnum.SearchMateEnum;
        ability.SetCanMate(false);
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

            transform.position += dir * ability.speed * Time.deltaTime;
            if (Vector3.Distance(ability.targetPos, transform.position) <= 0.1f)
            {
                ability.isArrive = true;
            }
        }
    }

    private void UpDateTitle()
    {
        titleLabel.transform.position = UICamera.mainCamera.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(transform.position));
    }

    //�������
    private void RaiseUpSeed(CellBug cellBug)
    {
        ability.status = Const.StutasEnum.IdleEnum;

        int birthNum = Const.geneArray[(int)Const.GenesEnum.BrithNumEnum].GetBirthNum(this);
        for (int i = 0; i < birthNum; i++)
        {
            GameObject bug = Instantiate(cellBugProfabs, this.transform.position, Quaternion.identity) as GameObject;
            CellBug temCellBug = bug.GetComponent<CellBug>();

            int[] lineOne = this.GetAbility().GetDna().DnaVariation();
            int[] lineTwo = cellBug.GetAbility().GetDna().DnaVariation();
            temCellBug.GetAbility().SetDNA(lineOne, lineTwo);

            temCellBug.GetAbility().cellBugGroup = this.ability.cellBugGroup;
        }
    }
}