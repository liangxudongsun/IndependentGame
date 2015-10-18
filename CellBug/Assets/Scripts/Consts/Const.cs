//���Ļ���
public static class Const
{
    public static float timeForPowerDelete = 3.0f;//ÿ��һ��ʱ���������1

    public static string CellBugTag = "CellBugTag";
    public static string FoodTag = "FoodTag";
    public static string FloorTag = "FloorTag";

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    //AI
    public static float powerForStarve = 30.0f;//���ڼ���״̬,��ҪѰʳ
    public static float disForEatFood = 10.0f; //�˾�������ʳ����ֱ��ȥ��
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

    //������ʽ
    public enum DeadEnum
    {
        TimeRunOutEnum,
        StarveEnum, 
        KilledEnum,
        PoisonEnum,
    };

    //����
    public enum DnaLineEnum
    {
        OneEnum,
        TwoEnum,
    }
}
