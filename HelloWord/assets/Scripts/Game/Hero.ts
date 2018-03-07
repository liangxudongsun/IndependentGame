const { ccclass, property } = cc._decorator;

import { ConstData } from "./../Tool/ConstData";
import Stick from './Stick';
import Block from './Block';

//英雄状态
enum HeroState
{
    HeroIdleEnum = 1,
    HeroAddStick,
    HeroRunEnum,
    HeroDropEnum,
}

@ccclass
export default class Hero extends cc.Component
{
    //英雄位置
    private heroPosition: cc.Vec2 = new cc.Vec2(0, 0);
    //英雄状态
    private heroState: HeroState = HeroState.HeroIdleEnum;
    //棍子
    private stick: Stick = null;
    //方块数组
    private blockArray: Array<cc.Node>;
    //英雄移动距离
    private heroMoveDis:number = 0;

    start()
    {
        this.heroState = HeroState.HeroIdleEnum;
    }

    update(dt) 
    {
        switch (this.heroState)
        {
            case HeroState.HeroIdleEnum:
                break;
            case HeroState.HeroAddStick:
                this.motorStickHight(dt);
                break;
            case HeroState.HeroRunEnum:
                break;
            case HeroState.HeroDropEnum:
                this.heroDrop();
                break;
        }
    }

    onDestory()
    {
        this.node.removeFromParent(true);
    }

    /**
     * 设置英雄状态
     */
    public setHeroState(state: number): boolean
    {
        switch (state)
        {
            case 2:
                if (this.heroState == HeroState.HeroIdleEnum)
                {
                    this.stick.resetStick(this.node.getPosition());
                    this.heroState = HeroState.HeroAddStick;
                    return true;
                }
                return false;
            case 3:
                if (this.heroState == HeroState.HeroAddStick)
                {
                    this.heroState = HeroState.HeroRunEnum;
                    return true;
                }
                return false;
        }
        return false;
    }

    /**
     * 设置棍子索引 
     */
    public setStick(stick: cc.Node)
    {
        this.stick = stick.getComponent(Stick);
    }

    /**
     * 设置方块数组索引
     */
    public setBlockArray(blockArray: Array<cc.Node>)
    {
        this.blockArray = blockArray;
    }

    /*
    * 驱动棍子生长
    */
    public motorStickHight(dt: number)
    {
        this.stick.addStickHight(dt);
    }

    /**
     * 英雄行走
     */
    public heroRun(dt)
    {
        this.stick.putStickDown();
        let distance = this.stick.node.getBoundingBox().size.width;
        this.heroMoveDis = distance + 20;

        let tempPos = new cc.Vec2(this.node.position.x + this.heroMoveDis,50);
        let rect = cc.rect(tempPos.x,tempPos.y,this.node.getBoundingBox().width,this.node.getBoundingBox().height);
        for (var index = 0; index < this.blockArray.length; index++) 
        {
            var element = this.blockArray[index];
            if (element.getBoundingBox().containsRect(rect)) 
            {
                let targetX = element.position.x + element.getBoundingBox().width/2;
                let addDis = targetX - tempPos.x;
                this.heroMoveDis += addDis - rect.width/2;
                break;
            }
        }

        let time = this.heroMoveDis / ConstData.HeroRunSpeed;
        let moveAction = cc.moveBy(time, new cc.Vec2(this.heroMoveDis, 0));
        this.node.runAction(moveAction);

        this.scheduleOnce(this.runTimeFun, time);
    }

    /*
    * 英雄掉落
    */
    private heroDrop()
    {
        let moveAction = cc.moveBy(ConstData.HeroTimeDrop, new cc.Vec2(10, -ConstData.HeroDisDrop));
        this.node.runAction(moveAction);
        this.heroState = HeroState.HeroIdleEnum;

        this.scheduleOnce(this.replaceSceneTimeFun,ConstData.HeroTimeDrop);
    }

    //场景跳转
    replaceSceneTimeFun = function (event: any)
    {
        cc.director.loadScene("scene");
    }.bind(this)

    //跑完检测
    runTimeFun = function (event: any)
    {
        this.heroState = HeroState.HeroIdleEnum;
        if (this.checkHeroState()) 
        {
            this.moveBlock();   
        }
    }.bind(this)

    /*
    * 检测是否掉落
    */
    private checkHeroState()
    {
        let live = false;
        for (var index = 0; index < this.blockArray.length; index++) 
        {
            var element = this.blockArray[index];
            let blockSize = element.getBoundingBox();
            let heroSize = this.node.getBoundingBox();

            heroSize.y = 50;

            if (blockSize.containsRect(heroSize))
            {
                live = true;
                return live;
            }
        }

        this.heroState = HeroState.HeroDropEnum;
        return live;
    }

    /*
    * 移动方块
    */
    private moveBlock()
    {
        let heroRunAction = cc.moveBy(0.5,new cc.Vec2(-this.heroMoveDis,0));
        this.node.runAction(heroRunAction);

        let stickRunAction = cc.moveBy(0.5,new cc.Vec2(-this.heroMoveDis,0));
        this.stick.node.runAction(stickRunAction);

        for (var index = 0; index < this.blockArray.length; index++) 
        {
            let element = this.blockArray[index];
            let elementRunAction = cc.moveBy(0.5,new cc.Vec2(-this.heroMoveDis,0));
            element.runAction(elementRunAction);
        }
    }
}
