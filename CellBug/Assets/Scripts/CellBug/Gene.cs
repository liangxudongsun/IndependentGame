//核心机制

public class Gene
{
    protected float lowPowerCustom, middlePowerCustom, highPowerCustom;

    private Const.GenesEnum GeneIndex;
    public void SetGeneIndex(Const.GenesEnum GeneIndex) { this.GeneIndex = GeneIndex; }
    public Const.GenesEnum GetGeneIndex() { return this.GeneIndex; }

    public virtual int GetSpeed(CellBug cellbug) { return 0; }
    public virtual string GetMusic(CellBug cellbug) { return ""; }
    public virtual int GetBirthNum(CellBug cellbug) { return 0; }
    public virtual int GetPowerGetFrom(CellBug cellbug) { return 0; }
    public virtual int GetPoison(CellBug cellbug) { return 0; }
    public virtual int GetAntibiotic(CellBug cellbug) { return 0; }
    public virtual float GetPhotosynthesis(CellBug cellbug) { return 0; }
    public virtual int GetAttackForce(CellBug cellbug) { return 0; }
    public virtual float GetPowerCustom(CellBug cellBug) { return 0; }
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

        lowPowerCustom = Const.lowPowerCustomSpeed;
        middlePowerCustom = Const.middlePowerCustomSpeed;
        highPowerCustom = Const.highPowerCustomSpeed;
    }

    public override int GetSpeed(CellBug cellbug)
    {
        //需要访问dna
        Dna dna = cellbug.GetAbility().GetDna();
        int speedGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SpeedEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SpeedEnum);
        if (speedGeneTotal == 0) nowSpeed = lowSpeed;
        else if (speedGeneTotal == 1) nowSpeed = middleSpeed;
        else if (speedGeneTotal == 2) nowSpeed = highSpeed;

        return nowSpeed;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int speedGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SpeedEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SpeedEnum);
        if (speedGeneTotal == 0) return lowPowerCustom;
        else if (speedGeneTotal == 1) return middlePowerCustom;
        else if (speedGeneTotal == 2) return highPowerCustom;

        return lowPowerCustom;
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

        lowPowerCustom = Const.lowPowerCustomSong;
        middlePowerCustom = Const.middlePowerCustomSong;
        highPowerCustom = Const.highPowerCustomSong;
    }

    public override string GetMusic(CellBug cellbug)
    {
        //需要访问dna

        Dna dna = cellbug.GetAbility().GetDna();
        int songGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SongEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SongEnum);
        if (songGeneTotal == 0) nowMusic = oneMusic;
        else if (songGeneTotal == 1) nowMusic = twoMusic;
        else if (songGeneTotal == 2) nowMusic = ThreeMusic;

        return nowMusic;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int songGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.SongEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.SongEnum);
        if (songGeneTotal == 0) return lowPowerCustom;
        else if (songGeneTotal == 1) return middlePowerCustom;
        else if (songGeneTotal == 2) return highPowerCustom;

        return lowPowerCustom;
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

        lowPowerCustom = Const.lowPowerCustomBrithNum;
        middlePowerCustom = Const.middlePowerCustomBrithNum;
        highPowerCustom = Const.highPowerCustomBrithNum;
    }

    public override int GetBirthNum(CellBug cellbug)
    {
        //需要访问dna
        Dna dna = cellbug.GetAbility().GetDna();
        int birthNumGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.BrithNumEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.BrithNumEnum);
        if (birthNumGeneTotal == 0) nowBirthNum = lowBirth;
        else if (birthNumGeneTotal == 1) nowBirthNum = middleBirth;
        else if (birthNumGeneTotal == 2) nowBirthNum = highBrith;

        return nowBirthNum;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int birthNumGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.BrithNumEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.BrithNumEnum);
        if (birthNumGeneTotal == 0) return lowPowerCustom;
        else if (birthNumGeneTotal == 1) return middlePowerCustom;
        else if (birthNumGeneTotal == 2) return highPowerCustom;

        return lowPowerCustom;
    }
}


public class PowerGetFrom : Gene
{
    private int nowForm;
    public PowerGetFrom()
    {
        SetGeneIndex(Const.GenesEnum.PowerGetFromEnum);
        nowForm = -1;

        lowPowerCustom = Const.lowPowerCustomPowerGetFrom;
        middlePowerCustom = Const.middlePowerCustomPowerGetFrom;
        highPowerCustom = Const.highPowerCustomPowerGetFrom;
    }

    public override int GetPowerGetFrom(CellBug cellbug)
    {
        //需要访问dna
        Dna dna = cellbug.GetAbility().GetDna();
        nowForm = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PowerGetFromEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PowerGetFromEnum);
        return nowForm;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int powerGetFromGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PowerGetFromEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PowerGetFromEnum);
        if (powerGetFromGeneTotal == 0) return lowPowerCustom;
        else if (powerGetFromGeneTotal == 1) return middlePowerCustom;
        else if (powerGetFromGeneTotal == 2) return highPowerCustom;

        return lowPowerCustom;
    }
}


public class Poison : Gene
{
    private int nowPoison;
    public Poison()
    {
        SetGeneIndex(Const.GenesEnum.PoisonEnum);
        nowPoison = -1;

        lowPowerCustom = Const.lowPowerCustomPoison;
        middlePowerCustom = Const.middlePowerCustomPoison;
        highPowerCustom = Const.highPowerCustomPoison;
    }

    public override int GetPoison(CellBug cellbug)
    {
        //需要访问dna
        Dna dna = cellbug.GetAbility().GetDna();
        nowPoison = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PoisonEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PoisonEnum);
        return nowPoison;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int poisonTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PoisonEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PoisonEnum);
        if (poisonTotal == 0) return lowPowerCustom;
        else if (poisonTotal == 1) return middlePowerCustom;
        else if (poisonTotal == 2) return highPowerCustom;

        return lowPowerCustom;
    }
}

public class Antibiotic : Gene
{
    private int nowAntibiotic;
    public Antibiotic()
    {
        SetGeneIndex(Const.GenesEnum.AntibioticEnum);
        nowAntibiotic = -1;

        lowPowerCustom = Const.lowPowerCustomAntibiotic;
        middlePowerCustom = Const.middlePowerCustomAntibiotic;
        highPowerCustom = Const.highPowerCustomAntibiotic;
    }

    public override int GetAntibiotic(CellBug cellbug)
    {
        //需要访问dna

        Dna dna = cellbug.GetAbility().GetDna();
        nowAntibiotic = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AntibioticEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AntibioticEnum);

        return nowAntibiotic;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int antibioticTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AntibioticEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AntibioticEnum);
        if (antibioticTotal == 0) return lowPowerCustom;
        else if (antibioticTotal == 1) return middlePowerCustom;
        else if (antibioticTotal == 2) return highPowerCustom;

        return lowPowerCustom;
    }
}


public class Photosynthesis : Gene
{
    private float lowPhotosynthesis, middlePhotosynthesis, highPhotosynthesis, nowPhotosynthesis;
    public Photosynthesis()
    {
        SetGeneIndex(Const.GenesEnum.PhotosynthesisEnum);
        lowPhotosynthesis = Const.lowPhotosynthesis;
        middlePhotosynthesis = Const.middlePhotosynthesis;
        highPhotosynthesis = Const.highPhotosynthesis;
        nowPhotosynthesis = -1.0f;

        lowPowerCustom = Const.lowPowerCustomPhotosynthesis;
        middlePowerCustom = Const.middlePowerCustomPhotosynthesis;
        highPowerCustom = Const.highPowerCustomPhotosynthesis;
    }

    public override float GetPhotosynthesis(CellBug cellbug)
    {
        //需要访问dna
        Dna dna = cellbug.GetAbility().GetDna();
        int photosynthesisGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PhotosynthesisEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PhotosynthesisEnum);
        if (photosynthesisGeneTotal == 0) nowPhotosynthesis = lowPhotosynthesis;
        else if (photosynthesisGeneTotal == 1) nowPhotosynthesis = middlePhotosynthesis;
        else if (photosynthesisGeneTotal == 2) nowPhotosynthesis = highPhotosynthesis;
        return nowPhotosynthesis;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int photosynthesisGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.PhotosynthesisEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.PhotosynthesisEnum);
        if (photosynthesisGeneTotal == 0) return lowPowerCustom;
        else if (photosynthesisGeneTotal == 1) return middlePowerCustom;
        else if (photosynthesisGeneTotal == 2) return highPowerCustom;
        return lowPowerCustom;
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

        lowPowerCustom = Const.lowPowerCustomAttackForce;
        middlePowerCustom = Const.middlePowerCustomAttackForce;
        highPowerCustom = Const.highPowerCustomAttackForce;
    }

    public override int GetAttackForce(CellBug cellbug)
    {
        //需要访问dna
        Dna dna = cellbug.GetAbility().GetDna();
        int attackGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AttackForceEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AttackForceEnum);
        if (attackGeneTotal == 0) nowAttackForce = lowAttackForce;
        else if (attackGeneTotal == 1) nowAttackForce = middleAttackForce;
        else if (attackGeneTotal == 2) nowAttackForce = highAttackForce;

        return nowAttackForce;
    }

    public override float GetPowerCustom(CellBug cellBug)
    {
        Dna dna = cellBug.GetAbility().GetDna();
        int attackGeneTotal = dna.GetDnaIndex(Const.DnaLineEnum.OneEnum, Const.GenesEnum.AttackForceEnum)
                           + dna.GetDnaIndex(Const.DnaLineEnum.TwoEnum, Const.GenesEnum.AttackForceEnum);
        if (attackGeneTotal == 0) return lowPowerCustom;
        else if (attackGeneTotal == 1) return middlePowerCustom;
        else if (attackGeneTotal == 2) return highPowerCustom;

        return lowPowerCustom;
    }
}
