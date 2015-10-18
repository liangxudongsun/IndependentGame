//核心机制
public static class Const
{
    public static float timeForPowerDelete = 3.0f;//每隔一点时间能量会减1

    public static string CellBugTag = "CellBugTag";
    public static string FoodTag = "FoodTag";
    public static string FloorTag = "FloorTag";

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    //AI
    public static float powerForStarve = 30.0f;//处于饥饿状态,需要寻食
    public static float disForEatFood = 10.0f; //此距离上有食物则直接去吃
    //AI

    public static int startPower = 20;
    public static int plantPower = 100;
    public static int meatPower = 200;

    public static int lowSpeed = 1, middleSpeed = 2, highSpeed = 3;
    public static string oneMusic = "", twoMusic = "", ThreeMusic = "";
    public static int lowBirth = 1, middleBirth = 2, highBrith = 3;

    public static int lowPhotosynthesis = 0, middlePhotosynthesis = 2, highPhotosynthesis = 5;
    public static int lowAttackForce = 1, middleAttackForce = 3, highAttackForce = 5;

    public static Gene[] geneArray = new Gene[] {new Speed(),new Song(),new BrithNum(),new PowerGetFrom(),new Poison(),
                                            new Antibiotic(),new Photosynthesis(),new AttackForce()};

    public enum GenesEnum
    {
        SpeedEnum,          //行动速度
        SongEnum,           //嗓音
        BrithNumEnum,           //单次生育后代数量
        PowerGetFromEnum,       //可消化食物
        PoisonEnum,         //是否有毒
        AntibioticEnum,     //抗毒性
        PhotosynthesisEnum, //光合作用
        AttackForceEnum,	//攻击力
    };

    public enum StutasEnum
    {
        IdleEnum,
        EatPlantEnum,
        EatMeatEnum,
        AttackEnum,
        AttackedEnum,
        SearchMateEnum,
        ReceviceMataEnum,
    }

    public enum CellBugGroup
    {
        MineEnum,
        OtherEnum,
    };

    public enum FoodEnum
    {
        MeatEnum,
        GrassEnum,
    }

    //死亡方式
    public enum DeadEnum
    {
        TimeRunOutEnum,
        StarveEnum, 
        KilledEnum,
        PoisonEnum,
    };

    //链条
    public enum DnaLineEnum
    {
        OneEnum,
        TwoEnum,
    }
}
