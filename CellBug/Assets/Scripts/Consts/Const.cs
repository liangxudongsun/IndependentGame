//���Ļ���
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
        FluorescentEnum,//ӫ��
        SearchLightEnum,    //̽�յ�
        SpeedEnum,          //�ж��ٶ�
        SongEnum,           //ɤ��
        BrithNumEnum,           //���������������
        PowerCupEnum,			//�������ֵ
        PowerGetPecentEnum,     //ʳ����������
        PowerGetFromEnum,       //������ʳ��
        PoisonEnum,         //�Ƿ��ж�
        AntibioticEnum,     //������
        PhotosynthesisEnum, //�������
        AttackForceEnum,	//������
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
