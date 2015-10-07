//核心机制

public class Gene
{
    private Const.GenesEnum GeneIndex;
    public void SetGeneIndex(Const.GenesEnum GeneIndex) { this.GeneIndex = GeneIndex; }
    public Const.GenesEnum GetGeneIndex() { return this.GeneIndex; }
}

public class Fluorescent : Gene
{
    private int lowLight, middleLight, highLight;
    private int lowRadius, middleRadius, highRadius;

    private int nowLight,nowRadius;

    void Fluorescent()
    {
        SetGeneIndex(Const.GenesEnum.FluorescentEnum);
        lowLight = Const.lowLightFlor;
        middleLight = Const.middleLightFlor;
        highLight = Const.highLightFlor;

        lowRadius = Const.lowRadiusFlor;
        middleRadius = Const.middleRadiusFlor;
        highRadius = Const.highRadiusFlor;

        nowLight = 0;
        nowRadius = 0;
    }

    public int GetFlorescentLight()
    {
        if (nowLight <= 0)
        {

        }
        return nowLight;
    }

    public int GetFlorescentRadius()
    {
        if (nowRadius <= 0)
        {

        }
        return nowRadius;
    }
}

public class SearchLight : Gene
{
    private int lowLight, middleLight, highLight;
    private int lowRadius, middleRadius, highRadius;

    private int nowLight,nowRadius;

    void SearchLight()
    {
        SetGeneIndex(Const.GenesEnum.SearchLightEnum);
        lowLight = Const.lowLightSearch;
        middleLight = Const.middleLightSearch;
        highLight = Const.highLightSearch;

        lowRadius = Const.lowRadiusSearch;
        middleRadius = Const.middleRadiusSearch;
        highRadius = Const.highRadiusSearch;

        nowLight = 0;
        nowRadius = 0;
    }

    public int GetFlorescentLight()
    {
        if (nowLight <= 0)
        {

        }
        return nowLight;
    }

    public int GetFlorescentRadius()
    {
        if (nowRadius <= 0)
        {

        }
        return nowRadius;
    }
}

public class Speed : Gene
{
    private int lowSpeed, middleSpeed, highSpeed, nowSpeed;
    void Speed()
    {
        SetGeneIndex(Const.GenesEnum.SpeedEnum);
        lowSpeed = 5;
        middleSpeed = 10;
        highSpeed = 15;
        nowSpeed = -1;
    }

    public int GetSpeed()
    {
        //需要访问dna
        if (nowSpeed <= 0)
        {

        }
        return nowSpeed;
    }
}

public class Song : Gene
{
    private string oneMusic, twoMusic, ThreeMusic, nowMusic;
    void Song()
    {
        SetGeneIndex(Const.GenesEnum.SongEnum);
        oneMusic = Const.oneMusic;
        twoMusic = Const.twoMusic;
        ThreeMusic = Const.ThreeMusic;
        nowMusic = "";
    }

    public string GetMusic()
    {
        //需要访问dna
        if (nowMusic == "")
        {

        }
        return nowMusic;
    }
}

public class BrithNum : Gene
{
    private int lowBirth, middleBirth, highBrith, nowBirthNum;
    void BrithNum()
    {
        SetGeneIndex(Const.GenesEnum.BrithNumEnum);
        lowBirth = Const.lowBirth;
        middleBirth = Const.middleBirth;
        highBrith = Const.highBrith;
        nowBirthNum = -1;
    }

    public int GetBirthNum()
    {
        //需要访问dna
        if (nowBirthNum <= 0)
        {

        }
        return nowBirthNum;
    }
}


public class PowerCup : Gene
{
    private int lowCup, middleCup, highCup, nowCup;
    void PowerCup()
    {
        SetGeneIndex(Const.GenesEnum.PowerCupEnum);
        lowCup = Const.lowCup;
        middleCup = Const.middleCup;
        highCup = Const.highCup;
        nowCup = -1;
    }

    public int GetPowerCup()
    {
        //需要访问dna
        if (nowCup <= 0)
        {

        }
        return nowCup;
    }
}

public class PowerGetPecent : Gene
{
    private float lowPecent, middlePecent, highPecent, nowPecent;
    void PowerGetPecent()
    {
        SetGeneIndex(Const.GenesEnum.PowerGetPecentEnum);
        lowPecent = Const.lowPecent;
        middlePecent = Const.middlePecent;
        highPecent = Const.highPecent;
        nowPecent = -1.0f;
    }

    public float GetPowerGetPecent()
    {
        //需要访问dna
        if (nowPecent <= 0)
        {

        }
        return nowPecent;
    }
}

public class PowerGetFrom : Gene
{
    private int nowForm;
    void PowerGetFrom()
    {
        SetGeneIndex(Const.GenesEnum.PowerGetFromEnum);
        nowForm = -1;
    }

    public int GetPowerGetFrom()
    {
        //需要访问dna
        if (nowForm < 0)
        {
        }
        return nowForm;
    }
}


public class Poison : Gene
{
    private int nowPoison;
    void Poison()
    {
        SetGeneIndex(Const.GenesEnum.PoisonEnum);
        nowPoison = -1;
    }

    public int GetPoison()
    {
        //需要访问dna
        if (nowPoison < 0)
        {

        }
        return nowPoison;
    }
}

public class Antibiotic : Gene
{
    private int nowAntibiotic;
    void Antibiotic()
    {
        SetGeneIndex(Const.GenesEnum.AntibioticEnum);
        nowAntibiotic = -1;
    }

    public int GetAntibiotic()
    {
        //需要访问dna
        if (nowAntibiotic < 0)
        {

        }
        return nowAntibiotic;
    }
}


public class Photosynthesis : Gene
{
    private int lowPhotosynthesis, middlePhotosynthesis, highPhotosynthesis, nowPhotosynthesis;
    void Photosynthesis()
    {
        SetGeneIndex(Const.GenesEnum.PhotosynthesisEnum);
        lowPhotosynthesis = Const.lowPhotosynthesis;
        middlePhotosynthesis = Const.middlePhotosynthesis;
        highPhotosynthesis = Const.highPhotosynthesis;
        nowPhotosynthesis = -1;
    }

    public int GetPhotosynthesis()
    {
        //需要访问dna
        if (nowPhotosynthesis < 0)
        {

        }
        return nowPhotosynthesis;
    }
}

public class AttackForce : Gene
{
    private int lowAttackForce, middleAttackForce, highAttackForce, nowAttackForce;
    void AttackForce()
    {
        SetGeneIndex(Const.GenesEnum.AttackForceEnum);
        lowAttackForce = Const.lowAttackForce;
        middleAttackForce = Const.middleAttackForce;
        highAttackForce = Const.highAttackForce;
        nowAttackForce = -1;
    }

    public int GetAttackForce()
    {
        //需要访问dna
        if (nowAttackForce < 0)
        {

        }
        return nowAttackForce;
    }
}
