//核心机制

public class Gene
{
    private Const.GenesEnum GeneIndex;
    public void SetGeneIndex(Const.GenesEnum GeneIndex) { this.GeneIndex = GeneIndex; }
    public Const.GenesEnum GetGeneIndex() { return this.GeneIndex; }

    public virtual int GetSpeed(CellBug cellbug) { return 0;}
    public virtual string GetMusic(CellBug cellbug) { return ""; }
    public virtual int GetBirthNum(CellBug cellbug) { return 0; }
    public virtual int GetPowerGetFrom(CellBug cellbug) { return 0; }
    public virtual int GetPoison(CellBug cellbug) { return 0; }
    public virtual int GetAntibiotic(CellBug cellbug) { return 0; }
    public virtual int GetPhotosynthesis(CellBug cellbug) { return 0; }
    public virtual int GetAttackForce(CellBug cellbug) { return 0; }
}

public class Speed : Gene
{
    private int lowSpeed, middleSpeed, highSpeed, nowSpeed;
    public Speed()
    {
        SetGeneIndex(Const.GenesEnum.SpeedEnum);
        lowSpeed = Const.lowSpeed;
        middleSpeed = Const.middleSpeed;
        highSpeed = Const.highSpeed;
        nowSpeed = -1;
    }

    public override int GetSpeed(CellBug cellbug)
    {
        //需要访问dna
        if (nowSpeed <= 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            int speedGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SpeedEnum) 
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SpeedEnum);
            if (speedGeneTotal == 0) nowSpeed = lowSpeed;
            else if (speedGeneTotal == 1) nowSpeed = middleSpeed;
            else if (speedGeneTotal == 2) nowSpeed = highSpeed;
        }
        return nowSpeed;
    }
}

public class Song : Gene
{
    private string oneMusic, twoMusic, ThreeMusic, nowMusic;
    public Song()
    {
        SetGeneIndex(Const.GenesEnum.SongEnum);
        oneMusic = Const.oneMusic;
        twoMusic = Const.twoMusic;
        ThreeMusic = Const.ThreeMusic;
        nowMusic = "";
    }

    public override string GetMusic(CellBug cellbug)
    {
        //需要访问dna
        if (nowMusic == "")
        {
            Dna dna = cellbug.GetAbility().GetDna();
            int songGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SongEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SongEnum);
            if (songGeneTotal == 0) nowMusic = oneMusic;
            else if (songGeneTotal == 1) nowMusic = twoMusic;
            else if (songGeneTotal == 2) nowMusic = ThreeMusic;
        }
        return nowMusic;
    }
}

public class BrithNum : Gene
{
    private int lowBirth, middleBirth, highBrith, nowBirthNum;
    public BrithNum()
    {
        SetGeneIndex(Const.GenesEnum.BrithNumEnum);
        lowBirth = Const.lowBirth;
        middleBirth = Const.middleBirth;
        highBrith = Const.highBrith;
        nowBirthNum = -1;
    }

    public override int GetBirthNum(CellBug cellbug)
    {
        //需要访问dna
        if (nowBirthNum <= 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            int birthNumGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.BrithNumEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.BrithNumEnum);
            if (birthNumGeneTotal == 0) nowBirthNum = lowBirth;
            else if (birthNumGeneTotal == 1) nowBirthNum = middleBirth;
            else if (birthNumGeneTotal == 2) nowBirthNum = highBrith;
        }
        return nowBirthNum;
    }
}


public class PowerGetFrom : Gene
{
    private int nowForm;
    public PowerGetFrom()
    {
        SetGeneIndex(Const.GenesEnum.PowerGetFromEnum);
        nowForm = -1;
    }

    public override int GetPowerGetFrom(CellBug cellbug)
    {
        //需要访问dna
        if (nowForm < 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            nowForm = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PowerGetFromEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PowerGetFromEnum);
        }
        return nowForm;
    }
}


public class Poison : Gene
{
    private int nowPoison;
    public Poison()
    {
        SetGeneIndex(Const.GenesEnum.PoisonEnum);
        nowPoison = -1;
    }

    public override int GetPoison(CellBug cellbug)
    {
        //需要访问dna
        if (nowPoison < 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            nowPoison = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PoisonEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PoisonEnum);
        }
        return nowPoison;
    }
}

public class Antibiotic : Gene
{
    private int nowAntibiotic;
    public Antibiotic()
    {
        SetGeneIndex(Const.GenesEnum.AntibioticEnum);
        nowAntibiotic = -1;
    }

    public override int GetAntibiotic(CellBug cellbug)
    {
        //需要访问dna
        if (nowAntibiotic < 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            nowAntibiotic = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AntibioticEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AntibioticEnum);

        }
        return nowAntibiotic;
    }
}


public class Photosynthesis : Gene
{
    private int lowPhotosynthesis, middlePhotosynthesis, highPhotosynthesis, nowPhotosynthesis;
    public Photosynthesis()
    {
        SetGeneIndex(Const.GenesEnum.PhotosynthesisEnum);
        lowPhotosynthesis = Const.lowPhotosynthesis;
        middlePhotosynthesis = Const.middlePhotosynthesis;
        highPhotosynthesis = Const.highPhotosynthesis;
        nowPhotosynthesis = -1;
    }

    public override int GetPhotosynthesis(CellBug cellbug)
    {
        //需要访问dna
        if (nowPhotosynthesis < 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            int photosynthesisGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PhotosynthesisEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PhotosynthesisEnum);
            if (photosynthesisGeneTotal == 0) nowPhotosynthesis = lowPhotosynthesis;
            else if (photosynthesisGeneTotal == 1) nowPhotosynthesis = middlePhotosynthesis;
            else if (photosynthesisGeneTotal == 2) nowPhotosynthesis = highPhotosynthesis;
        }
        return nowPhotosynthesis;
    }
}

public class AttackForce : Gene
{
    private int lowAttackForce, middleAttackForce, highAttackForce, nowAttackForce;
    public AttackForce()
    {
        SetGeneIndex(Const.GenesEnum.AttackForceEnum);
        lowAttackForce = Const.lowAttackForce;
        middleAttackForce = Const.middleAttackForce;
        highAttackForce = Const.highAttackForce;
        nowAttackForce = -1;
    }

    public override int GetAttackForce(CellBug cellbug)
    {
        //需要访问dna
        if (nowAttackForce < 0)
        {
            Dna dna = cellbug.GetAbility().GetDna();
            int attackGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AttackForceEnum)
                               + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AttackForceEnum);
            if (attackGeneTotal == 0) nowAttackForce = lowAttackForce;
            else if (attackGeneTotal == 1) nowAttackForce = lowAttackForce;
            else if (attackGeneTotal == 2) nowAttackForce = lowAttackForce;
        }
        return nowAttackForce;
    }
}
