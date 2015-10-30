using UnityEngine;
using System.Collections;

//���Ļ���
public class Ability{

    private CellBug mine;
    //��ǰ�������������;
    private float power = Const.StartPower;
    //��ǰʣ����ʱ��;
    private float remainLiveTime = Const.MaxLiveTime;
    private bool isLive = true;
 
    /// <������ż>
    private float mateTime = Const.MaleTime;//����ʱ����
    private bool canMate = false;   //�Ƿ��������ż
    /// <������ż>

    /// <�����ƶ�>
    public bool isArrive = true;
    public Vector3 targetPos = Vector3.zero;
    /// <�����ƶ�>

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

    public void SetLiveTime(float time)
    {
        this.remainLiveTime += time;
    }

    public Dna GetDna()
    {
        return dna;
    }

    public bool SetPower(float powerModify)
    {
        power += powerModify;
        if (power > Const.MaxPower) power = Const.MaxPower;
        mine.GetBloodSlider().value = power / Const.MaxPower;
        if (power <= 0 && isLive == true)
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
            mine.Dead(Const.DeadEnum.TimeRunOutEnum);
            isLive = false;
        }
    }

    public void UpdateMateTime(float time)
    {
        if (!isLive) return;
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
        speed = Const.GeneArray[(int)Const.GenesEnum.SpeedEnum].GetSpeed(mine);
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

    public bool GetIsLive()
    {
        return isLive;
    }

    public void SetIsLive(bool isLive)
    {
        this.isLive = isLive;
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