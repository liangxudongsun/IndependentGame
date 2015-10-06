//核心机制
public static class Const
{
    public static int DnaLineLength = 15;
	public static float MaleTime = 30.0f;
    public static int plantPower = 100;
    public static int meatPower = 200;

    //基因
    public static enum GenesEnum
    {
        FluorescentEnum = 1,//荧光
        SearchLightEnum,    //探照灯
        SpeedEnum,          //行动速度
        SongEnum,           //嗓音
        BirthInterval,      //生育间隔
        BrithNum,           //单次生育后代数量
        PowerCupEnum,       //能量最大值
        PowerGetPecent,     //食物消化能力
        PowerGetFrom,       //可消化食物
        PoisonEnum,         //是否有毒
        AntibioticEnum,     //抗毒性
        PhotosynthesisEnum, //光合作用
        ShadowEnum,         //是否隐形
        ShadowFindEnum,     //反隐形
        SleepEnum,          //休眠
    };

    //死亡方式
    public static enum DeadEnum
    {
        TimeRunOutEnum = 1,
        KilledEnum,
    };

    //链条
    public static enum DnaLineEnum
    {
        OneEnum = 1,
        TwoEnum,
    }
}
