const { ccclass, property } = cc._decorator;
import { ConstData } from "./../Tool/ConstData";

@ccclass
export default class Stick extends cc.Component
{
    //棍子的位置
    private stickPosition: cc.Vec2 = new cc.Vec2(0, 0);

    //棍子的高度
    private hightPercent: number = 0.0;

    // onLoad () {},

    start()
    {
        //this.node.setPosition(this.stickPosition);
        this.node.setScale(1.0, this.hightPercent);
    }

    // update (dt) {},

    onDestory()
    {
        this.node.removeFromParent(true);
    }

    /**
     * 增加筷子长度
     */
    public addStickHight(dt:number)
    {
        this.hightPercent += ConstData.StickAddHightSpeed*dt;
        if (this.hightPercent > 1) 
        {
            this.hightPercent = 1;
        }
        this.node.setScale(1.0, this.hightPercent);
    }

    /**
     * 放倒筷子
     */
    public putStickDown() 
    {
        this.node.rotation = 90;    
    }

    /**
     * 重置棍子
     */
    public resetStick(position:cc.Vec2)
    {
        let stickPos = position;
        stickPos.x += 10;
        this.node.setPosition(stickPos);    
        this.hightPercent = 0;
        this.node.setScale(1.0, this.hightPercent);
        this.node.rotation = 0;
    }
}
