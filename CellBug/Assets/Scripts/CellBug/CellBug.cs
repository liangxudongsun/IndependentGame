//���Ļ���
using UnityEngine;
using System.Collections;

public class CellBug:MonoBehaviour
{
    private Dna dna;
    private Ability ability;

    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

    public Dna GetDna()
    {
        return dna;
    }

    //�ƶ�
    public void Move()
    {
        //�漰��Ѱ·,�ƶ��ٶȽ�����ΪѰ·���ٶ�
    }

    //����
    public void Attack(CellBug cellBug)
    {
        //���������ֹͣ����
    }

    //˯��
    public void Sleep()
    {
        //��رյƹ�,������������
    }

    //����
    public void Shadow()
    {
        //����رյƹ�,���������˿�����
    }

    //�Բ�
    public void EatPlant()
    {
        //��Ҫ���û���鿴
    }

    //����
    public void EatMeat(CellBug cellBug)
    {
        //��Ҫ���û���鿴,���ܷ����,����û�ж�,�Լ���û�п�����
    }

    //����ӫ��
    public void OpenFluorescent()
    {
        //��Ҫ���û���鿴
    }

    //�ر�ӫ��
    public void CloseFluorescent()
    {
        
    }

    //����̽�յ�
    public void OpenSearchLight()
    {
        //��Ҫ���û���鿴
    }

    //�ر�̽�յ�
    public void CloseSearchLight()
    {
        
    }

    //�ٻ���ż
    public void CallMate()
    {
        //��Ҫ���û���鿴
    }

    //��Գɹ�,�ٻ���û��Ȩ������
    public void IWillYouMate(CellBug cellBug)
    {
        //ֹͣ�ٻ�,��¼��ż,��Ҫ��һ���嵥��¼,������ʱ����
    }

    //���յ��ٻ�
    public void CallProCast()
    {
        //��ֹ���������ٻ�
    }

    //�������
    public void RaiseUpSeed()
    {

    }

    public Ability GetAbility()
    {
        return ability;
    }

    //�����ĲŻ�ʹ��,ϵͳ�����Ĳ�ʹ��
    public void SetDNA(int[] DNALineOne, int[] DNALineTwo)
    {
        dna.SetDna(DNALineOne, DNALineTwo);
    }

    //����
    public void Dead(Const.DeadEnum deadEnum)
    {

    }
}