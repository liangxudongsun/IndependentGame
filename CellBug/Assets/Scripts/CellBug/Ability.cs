//���Ļ���
public class Ability
{
    //�Ƿ���;
    private bool isLive = true;
    //��ǰ�������������;
    private int power;
    //��ǰʣ����ʱ��;
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