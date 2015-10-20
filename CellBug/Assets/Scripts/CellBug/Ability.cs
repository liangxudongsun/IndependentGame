using UnityEngine;
using System.Collections;

//核心机制
public class Ability{

    private CellBug mine;
    //是否存活;
    private bool isLive = true;
    //当前能量和最大能量;
    private int power = Const.startPower;
    //当前剩余存活时间;
    private float remainLiveTime = 300.0f;
 
    /// <关于求偶>
    public bool isResquest = false;//是否求偶
    public bool isMateSusess = true;//是否求偶成功
    public bool isRequested = false;//是否被召唤
    public CellBug nowMateCellBug = null;  //当前主动召唤的配偶
    public ArrayList requestedList = new ArrayList();
    public float timeForClearList = Const.timeForClearList;//多久清理一次列表
    /// <关于求偶>

    /// <关于移动>
    public int speed = 5;
    public bool isArrive = true;
    public Vector3 targetPos = Vector3.zero;
    /// <关于移动>
   
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

    //出生的才会使用,系统产生的不使用
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