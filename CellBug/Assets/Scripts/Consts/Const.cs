//���Ļ���
public static class Const
{
    public static string CellBugTag = "CellBugTag";
    public static string FoodTag = "FoodTag";

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    public static float timeForPowerDelete = 2.0f;//ÿ��һ��ʱ���������1
    public static float maxPower = 200.0f;//�������ֵ
    public static float foodCreaterTime = 20.0f;//ʳ�����ɼ��
    public static int startPower = 50;
    public static int plantPower = 20;
    public static int meatPower = 50;

    //AI
    public static float powerForStarve = 60.0f;//���ڼ���״̬,��ҪѰʳ
    public static float disForEatFood = 100.0f; //�˾�������ʳ����ֱ��ȥ��
    public static float disForEatEnemy = 100.0f; //�ٴξ�����Ѱ�ҵ���
    public static int powerForMate = 60;        //Ѱ����ż������
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
        SpeedEnum,          //�ж��ٶ�
        SongEnum,           //ɤ��
        BrithNumEnum,       //���������������
        PowerGetFromEnum,   //������ʳ��
        PoisonEnum,         //�Ƿ��ж�
        AntibioticEnum,     //������
        PhotosynthesisEnum, //�������
        AttackForceEnum,	//������
    };

    public static string[] DnaName = new string[] { "����", "ɤ��", "����", "����","����","����","���","����" };

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
