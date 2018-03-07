const { ccclass, property } = cc._decorator;

@ccclass
export default class Block extends cc.Component
{
    //方块位置
    private blockPos: cc.Vec2 = new cc.Vec2(0,0);
    //宽度比例
    private widthPercent : number = 1.0;
    
    //onLoad() {}

    start()
    {

    }

    update(dt) 
    {

    }

    onDestory()
    {
        this.node.removeFromParent(true);
    }

    public setPosition(position:cc.Vec2)
    {
        this.blockPos = position;
    }

    public setWidthPercent(widthPercent:number)
    {
        this.widthPercent = widthPercent;
    }
}
