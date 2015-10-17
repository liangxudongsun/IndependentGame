//核心机制
public static class Const
{
    public static string CellBugTag = "CellBugTag";
    public static string GrassTag = "GrassTag";
    public static string MeatTag = "MeatTag";
    public static string FloorTag = "FloorTag";

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    public static int startPower = 100;
    public static int plantPower = 100;
    public static int meatPower = 200;

    public static int lowSpeed = 1, middleSpeed = 2, highSpeed = 3;
    public static string oneMusic = "", twoMusic = "", ThreeMusic = "";
    public static int lowBirth = 1, middleBirth = 2, highBrith = 3;

    public static int lowPhotosynthesis = 0, middlePhotosynthesis = 2, highPhotosynthesis = 5;
    public static int lowAttackForce = 1, middleAttackForce = 3, highAttackForce = 5;

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
        SearchMateEnum,
        ReceviceMataEnum,
    }

    public enum CellBugGroup
    {
        MineEnum,
        OtherEnum,
    };

    //死亡方式
    public enum DeadEnum
    {
        TimeRunOutEnum = 1,
        KilledEnum,
        PoisonEnum,
    };

    //链条
    public enum DnaLineEnum
    {
        OneEnum = 1,
        TwoEnum,
    }
}
