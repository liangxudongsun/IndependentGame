//���Ļ���
public static class Const
{
    public static int DnaLineLength = 15;
	public static float MaleTime = 30.0f;
    public static int plantPower = 100;
    public static int meatPower = 200;

    //����
    public static enum GenesEnum
    {
        FluorescentEnum = 1,//ӫ��
        SearchLightEnum,    //̽�յ�
        SpeedEnum,          //�ж��ٶ�
        SongEnum,           //ɤ��
        BirthInterval,      //�������
        BrithNum,           //���������������
        PowerCupEnum,       //�������ֵ
        PowerGetPecent,     //ʳ����������
        PowerGetFrom,       //������ʳ��
        PoisonEnum,         //�Ƿ��ж�
        AntibioticEnum,     //������
        PhotosynthesisEnum, //�������
        ShadowEnum,         //�Ƿ�����
        ShadowFindEnum,     //������
        SleepEnum,          //����
    };

    //������ʽ
    public static enum DeadEnum
    {
        TimeRunOutEnum = 1,
        KilledEnum,
    };

    //����
    public static enum DnaLineEnum
    {
        OneEnum = 1,
        TwoEnum,
    }
}
