//���Ļ���
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
        SpeedEnum,          //�ж��ٶ�
        SongEnum,           //ɤ��
        BrithNumEnum,           //���������������
        PowerGetFromEnum,       //������ʳ��
        PoisonEnum,         //�Ƿ��ж�
        AntibioticEnum,     //������
        PhotosynthesisEnum, //�������
        AttackForceEnum,	//������
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

    //������ʽ
    public enum DeadEnum
    {
        TimeRunOutEnum = 1,
        KilledEnum,
        PoisonEnum,
    };

    //����
    public enum DnaLineEnum
    {
        OneEnum = 1,
        TwoEnum,
    }
}
