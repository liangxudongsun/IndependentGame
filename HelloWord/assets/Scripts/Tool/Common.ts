export class Common 
{
    private static Instance: Common = null;
    constructor() 
    {

    }

    public static GetInstance() 
    {
        if (this.Instance == null) 
        {
            this.Instance = new Common();
        }
        return this.Instance;
    }

    //返回一个0~100的数
    public GetRandomNumber():number
    {
        return Math.random()*100;
    }
}
