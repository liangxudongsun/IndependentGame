using UnityEngine;
using System.Collections;

//核心机制
public class Ability{

    private CellBug mine;
    //是否存活;
    private bool isLive = true;
    //当前能量和最大能量;
    private float power = Const.startPower;
    //当前剩余存活时间;
    private float remainLiveTime = 300.0f;
 
    /// <关于求偶>
    private float mateTime = Const.MaleTime;//发情时间间隔
    private bool canMate = false;   //是否可主动求偶
    /// <关于求偶>

    /// <关于移动>
    public bool isArrive = true;
    public Vector3 targetPos = Vector3.zero;
    /// <关于移动>

    private Dna dna = new Dna();
    private Const.CellBugGroup cellBugGroup = Const.CellBugGroup.GodChildEnum;
    private Const.StutasEnum status = Const.StutasEnum.IdleEnum;
    private int id = 0;


    public void setMine(CellBug mine)
    {
        this.mine = mine;
        dna.cellbug = mine;
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

    public bool SetPower(float powerModify)
    {
        power += powerModify;
        if (power > Const.maxPower) power = Const.maxPower;

        mine.GetBloodSlider().value = power / Const.maxPower;

        if (power <= 0)
        {
            isLive = false;
            return false;
        }
        return true;
    }

    public float GetPower()
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

    public void UpdateMateTime(float time)
    {
        mateTime -= time;
        if (canMate == false && mateTime <= 0)
        {
            SetCanMate(true);
            mateTime = Const.MaleTime;
        }
    }

    public int GetSpeed()
    {
        int speed = 0;
        speed = Const.geneArray[(int)Const.GenesEnum.SpeedEnum].GetSpeed(mine);
        return speed;
    }

    public bool GetCanMate()
    {
        return canMate;
    }

    public void SetCanMate(bool canMate)
    {
        this.canMate = canMate;
    }

    public void SetStatus(Const.StutasEnum status)
    {
        this.status = status;
        mine.GetGameControl().StatusVision(mine);
    }

    public Const.StutasEnum GetStatus()
    {
        return status;
    }

    public void SetGroup(Const.CellBugGroup group)
    {
        this.cellBugGroup = group;
    }

    public Const.CellBugGroup GetGroup()
    {
        return cellBugGroup;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public int GetId()
    {
        return id;
    }
}