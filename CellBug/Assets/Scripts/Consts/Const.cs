//���Ļ���
public static class Const
{
    public static string CellBugTag = "CellBugTag";
    public static string FoodTag = "FoodTag";

    public static float AttackDis = 2.0f;
    public static float FoodDis = 2.0f;
    public static float MateDis = 1.0f;

    public static int DnaLineLength = 8;
	public static float MaleTime = 30.0f;

    public static int VariationProbability = 10;//���켸��,Ϊ���ٷ�֮һ 
    public static float TimeForPowerDelete = 2.0f;//ÿ��һ��ʱ���������1
    public static float MaxPower = 200.0f;//�������ֵ
    public static float FoodCreaterTime = 20.0f;//ʳ�����ɼ��
    public static float CellBugCreaterTime = 5.0f;//�������ɼ��
    public static int CellBugCreaterNum = 2;//�״ξ�����������
    public static int StartPower = 60;
    public static int PlantPower = 20;
    public static int MeatPower = 50;

    //AI
    public static float TimeForCreatePosition = 4.0f;//�Զ�����Ŀ���ʱ����
    public static float PowerForStarve = 60.0f;//���ڼ���״̬,��ҪѰʳ
    public static float DisForEatFood = 100.0f; //�˾�������ʳ����ֱ��ȥ��
    public static float DisForEatEnemy = 100.0f; //�ٴξ�����Ѱ�ҵ���
    public static int PowerForMate = 60;        //Ѱ����ż������
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
