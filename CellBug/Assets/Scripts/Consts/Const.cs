//核心机制
public static class Const
{
    public static string CellBugTag = "CellBugTag";
    public static string FoodTag = "FoodTag";

    public static float AttackDis = 2.0f;
    public static float FoodDis = 2.0f;
    public static float MateDis = 1.0f;

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    public static int VariationProbability = 10;//变异几率,为多少分之一 
    public static float TimeForPowerDelete = 2.0f;//每隔一点时间能量会减1
    public static float MaxPower = 200.0f;//最大能量值
    public static float FoodCreaterTime = 20.0f;//食物生成间隔
    public static float CellBugCreaterTime = 5.0f;//精灵生成间隔
    public static int CellBugCreaterNum = 2;//首次精灵生成数量
    public static int StartPower = 60;
    public static int PlantPower = 20;
    public static int MeatPower = 50;

    //AI
    public static float TimeForCreatePosition = 4.0f;//自动生成目标点时间间隔
    public static float PowerForStarve = 60.0f;//处于饥饿状态,需要寻食
    public static float DisForEatFood = 100.0f; //此距离上有食物则直接去吃
    public static float DisForEatEnemy = 100.0f; //再次距离内寻找敌人
    public static int PowerForMate = 60;        //寻找配偶的条件
    //AI

    public static int lowSpeed = 5, middleSpeed = 8, highSpeed = 10;
    public static string oneMusic = "", twoMusic = "", ThreeMusic = "";
    public static int lowBirth = 1, middleBirth = 2, highBrith = 3;
    public static float lowPhotosynthesis = 0, middlePhotosynthesis = 0.2f, highPhotosynthesis = 0.4f;
    public static int lowAttackForce = 1, middleAttackForce = 3, highAttackForce = 5;

    public static float GeneForPowerUpdate = 1.0f;
    public static float lowPowerCustomSpeed = 0, middlePowerCustomSpeed = 0.3f, highPowerCustomSpeed = 0.3f;
    public static float lowPowerCustomSong = 0, middlePowerCustomSong = 0.05f, highPowerCustomSong = 0.05f;
    public static float lowPowerCustomBrithNum = 0, middlePowerCustomBrithNum = 0.05f, highPowerCustomBrithNum = 0.1f;
    public static float lowPowerCustomPowerGetFrom = 0, middlePowerCustomPowerGetFrom = 0.2f, highPowerCustomPowerGetFrom = 0.4f;
    public static float lowPowerCustomPoison = 0, middlePowerCustomPoison = 0.05f, highPowerCustomPoison = 0.1f;
    public static float lowPowerCustomAntibiotic = 0, middlePowerCustomAntibiotic = 0.05f, highPowerCustomAntibiotic = 0.1f;
    public static float lowPowerCustomPhotosynthesis = 0, middlePowerCustomPhotosynthesis = 0.0f, highPowerCustomPhotosynthesis = 0.0f;
    public static float lowPowerCustomAttackForce = 0, middlePowerCustomAttackForce = 0.05f, highPowerCustomAttackForce = 0.1f;

    public static Gene[] GeneArray = new Gene[] {new Speed(),new Song(),new BrithNum(),new PowerGetFrom(),new Poison(),
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
