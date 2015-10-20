using UnityEngine;
using System.Collections;

//���Ļ���
public class Ability{

    private CellBug mine;
    //�Ƿ���;
    private bool isLive = true;
    //��ǰ�������������;
    private int power = Const.startPower;
    //��ǰʣ����ʱ��;
    private float remainLiveTime = 300.0f;
 
    /// <������ż>
    public bool isResquest = false;//�Ƿ���ż
    public bool isMateSusess = true;//�Ƿ���ż�ɹ�
    public bool isRequested = false;//�Ƿ��ٻ�
    public CellBug nowMateCellBug = null;  //��ǰ�����ٻ�����ż
    public ArrayList requestedList = new ArrayList();
    public float timeForClearList = Const.timeForClearList;//�������һ���б�
    /// <������ż>

    /// <�����ƶ�>
    public int speed = 5;
    public bool isArrive = true;
    public Vector3 targetPos = Vector3.zero;
    /// <�����ƶ�>
   
    public Const.CellBugGroup cellBugGroup = Const.CellBugGroup.MineEnum;
    public Const.StutasEnum status = Const.StutasEnum.IdleEnum;

    private Dna dna = new Dna();

    public void setMine(CellBug mine)
    {
        this.mine = mine;
    }

    public void TimeDrive(float deltaTime)
    {

    }

    public bool GetIsLive()
    {
        return isLive;
    }

    //�����ĲŻ�ʹ��,ϵͳ�����Ĳ�ʹ��
    public void SetDNA(int[] DNALineOne, int[] DNALineTwo)
    {
        dna.SetDna(DNALineOne, DNALineTwo);

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SpeedEnum) == 1 
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SpeedEnum) == 1)
        {
            mine.transform.FindChild("speed").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PoisonEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PoisonEnum) == 1)
        {
            mine.transform.FindChild("poison").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AntibioticEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AntibioticEnum) == 1)
        {
            mine.transform.FindChild("antibiotic").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AttackForceEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AttackForceEnum) == 1)
        {
            mine.transform.FindChild("attackForce").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.BrithNumEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.BrithNumEnum) == 1)
        {
            mine.transform.FindChild("brithNum").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PhotosynthesisEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PhotosynthesisEnum) == 1)
        {
            mine.transform.FindChild("photosynthesis").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PowerGetFromEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PowerGetFromEnum) == 1)
        {
            mine.transform.FindChild("powerGetFrom").gameObject.SetActive(true);
        }

        if (dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SongEnum) == 1
            || dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SongEnum) == 1)
        {
            mine.transform.FindChild("song").gameObject.SetActive(true);
        }
    }

    public Dna GetDna()
    {
        return dna;
    }

    public bool SetPower(int powerModify)
    {
        power += powerModify;
        if (power <= 0)
        {
            isLive = false;
            return false;
        }
        return true;
    }

    public int GetPower()
    {
        return power;
    }

    public void liveTimeModify(float timePass)
    {
        remainLiveTime -= timePass;
        if (remainLiveTime < 0)
        {
            isLive = false;
        }
    }
}