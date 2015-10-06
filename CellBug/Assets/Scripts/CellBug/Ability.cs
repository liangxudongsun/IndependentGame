//核心机制
public class Ability
{
    //是否存活;
    private bool isLive = true;
    //当前能量和最大能量;
    private int power;
    //当前剩余存活时间;
    private float remainLiveTime = 300.0f;

    public void TimeDrive(float deltaTime)
    {

    }

    public bool GetIsLive()
    {
        return isLive;
    }

    public void SetPower(int powerModify)
    {
        power += powerModify;
        if (power <= 0)
        {
            isLive = false;
        }
    }

    public void liveTimeModify(float timePass)
    {
        remainLiveTime -= timePass;
        if (remainLiveTime < 0)
        {
            isLive = false;
        }
    }
}