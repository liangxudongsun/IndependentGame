//核心机制
public static class Const
{
    public static string CellBugTag = "CellBugTag";
    public static string FoodTag = "FoodTag";

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    public static float timeForPowerDelete = 2.0f;//每隔一点时间能量会减1
    public static float maxPower = 200.0f;//最大能量值
    public static float foodCreaterTime = 20.0f;//食物生成间隔
    public static int startPower = 50;
    public static int plantPower = 20;
    public static int meatPower = 50;

    //AI
    public static float powerForStarve = 60.0f;//处于饥饿状态,需要寻食
    public static float disForEatFood = 100.0f; //此距离上有食物则直接去吃
    public static float disForEatEnemy = 100.0f; //再次距离内寻找敌人
    public static int powerForMate = 60;        //寻找配偶的条件
    //AI

    public static int lowSpeed = 5, middleSpeed = 8, highSpeed = 10;
    public static string oneMusic = "", twoMusic = "", ThreeMusic = "";
    public static int lowBirth = 1, middleBirth = 2, highBrith = 3;

    public static float lowPhotosynthesis = 0, middlePhotosynthesis = 0.2f, highPhotosynthesis = 0.4f;
    public static int lowAttackForce = 1, middleAttackForce = 3, highAttackForce = 5;

    public static Gene[] geneArray = new Gene[] {new Speed(),new Song(),new BrithNum(),new PowerGetFrom(),new Poison(),
                                            new Antibiotic(),new Photosynthesis(),new AttackForce()};

    public enum GenesEnum
    {
        SpeedEnum,          //行动速度
        SongEnum,           //嗓音
        BrithNumEnum,       //单次生育后代数量
        PowerGetFromEnum,   //可消化食物
        PoisonEnum,         //是否有毒
        AntibioticEnum,     //抗毒性
        PhotosynthesisEnum, //光合作用
        AttackForceEnum,	//攻击力
    };

    public static string[] DnaName = new string[] { "肌肉", "嗓音", "生育", "消化","毒素","抗毒","光合","攻击" };

    public enum StutasEnum
    {
        IdleEnum,
        EatPlantEnum,
        EatMeatEnum,
        AttackEnum,
        SearchMateEnum,
    }

    public enum CellBugGroup
    {
        GodChildEnum,
        OrcEnum,
        HumanEnum,
        EidolonEnum,
    };

    public static string[] GroupName = new string[]{"GodChild","Orc","Human","Eidolon"};
 
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
