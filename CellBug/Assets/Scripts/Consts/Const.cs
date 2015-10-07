//核心机制
public static class Const
{
    public static int DnaLineLength = 12;
	public static float MaleTime = 30.0f;
    
    public static int plantPower = 100;
    public static int meatPower = 200;
    public static int lowCup = 10, middleCup = 20, highCup = 30;
    public static float lowPecent = 0.1f, middlePecent = 0.2f, highPecent = 0.3f;

    public static int lowLightFlor = 1, middleLightFlor = 2, highLightFlor = 3;
    public static int lowRadiusFlor = 1, middleRadiusFlor= 2, highRadiusFlor = 3;

    public static int lowLightSearch = 1, middleLightSearch = 2, highLightSearch = 3;
    public static int lowRadiusSearch = 1, middleRadiusSearch = 2, highRadiusSearch = 3;

    public static int lowSpeed = 1, middleSpeed = 2, highSpeed = 3;
    public static string oneMusic = "", twoMusic = "", ThreeMusic = "";
    public static int lowBirth = 1, middleBirth = 2, highBrith = 3;

    public static int lowPhotosynthesis = 0, middlePhotosynthesis = 2, highPhotosynthesis = 5;
    public static int lowAttackForce = 1, middleAttackForce = 3, highAttackForce = 5;

    public enum GenesEnum
    {
        FluorescentEnum,//荧光
        SearchLightEnum,    //探照灯
        SpeedEnum,          //行动速度
        SongEnum,           //嗓音
        BrithNumEnum,           //单次生育后代数量
        PowerCupEnum,			//能量最大值
        PowerGetPecentEnum,     //食物消化能力
        PowerGetFromEnum,       //可消化食物
        PoisonEnum,         //是否有毒
        AntibioticEnum,     //抗毒性
        PhotosynthesisEnum, //光合作用
        AttackForceEnum,	//攻击力
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
