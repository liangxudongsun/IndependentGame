//核心机制
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

    //移动
    public void Move()
    {
        //涉及到寻路,移动速度将设置为寻路的速度
    }

    //攻击
    public void Attack(CellBug cellBug)
    {
        //如果死亡则停止攻击
    }

    //睡眠
    public void Sleep()
    {
        //会关闭灯光,减少能量消耗
    }

    //隐身
    public void Shadow()
    {
        //不会关闭灯光,但是其他人看不见
    }

    //吃草
    public void EatPlant()
    {
        //需要调用基因查看
    }

    //吃肉
    public void EatMeat(CellBug cellBug)
    {
        //需要调用基因查看,看能否吃肉,肉有没有毒,自己有没有抗毒性
    }

    //开启荧光
    public void OpenFluorescent()
    {
        //需要调用基因查看
    }

    //关闭荧光
    public void CloseFluorescent()
    {
        
    }

    //开启探照灯
    public void OpenSearchLight()
    {
        //需要调用基因查看
    }

    //关闭探照灯
    public void CloseSearchLight()
    {
        
    }

    //召唤配偶
    public void CallMate()
    {
        //需要调用基因查看
    }

    //配对成功,召唤者没有权利反对
    public void IWillYouMate(CellBug cellBug)
    {
        //停止召唤,记录配偶,需要有一个清单记录,并且适时清理
    }

    //接收到召唤
    public void CallProCast()
    {
        //禁止接受其他召唤
    }

    //产生后代
    public void RaiseUpSeed()
    {

    }

    public Ability GetAbility()
    {
        return ability;
    }

    //出生的才会使用,系统产生的不使用
    public void SetDNA(int[] DNALineOne, int[] DNALineTwo)
    {
        dna.SetDna(DNALineOne, DNALineTwo);
    }

    //死亡
    public void Dead(Const.DeadEnum deadEnum)
    {

    }
}