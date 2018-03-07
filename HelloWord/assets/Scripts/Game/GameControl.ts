import Hero from './Hero';
import { Common } from "./../Tool/Common";
import { ConstData } from '../Tool/ConstData';
import Stick from './Stick';
const { ccclass, property } = cc._decorator;

@ccclass
export default class GameControl extends cc.Component
{
    @property(cc.Prefab)
    private heroProfab: cc.Prefab = null;

    @property(cc.Prefab)
    private stickProfab: cc.Prefab = null;

    @property(cc.Prefab)
    private blockProfab: cc.Prefab = null;

    @property(cc.Node)
    private blockGroup:cc.Node = null;

    //英雄
    private hero: Hero = null;
    //棍子
    private stick: Stick = null;
    
    //方块数组
    @property([cc.Node])
    blockArray:Array<cc.Node> = [];  
    
    onLoad() 
    {
        //添加事件
        this.node.on(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
        this.node.on(cc.Node.EventType.MOUSE_UP, this.onMouseUp);
    }

    start()
    {
        this.createHeroAndStick();
    }

    onDestory()
    {
        this.node.off(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
        this.node.off(cc.Node.EventType.MOUSE_UP, this.onMouseUp);
    }

    private createBlock() 
    {
        let block = cc.instantiate(this.blockProfab);

        this.blockArray.push(block);
        this.blockGroup.addChild(block);

        let position = Common.GetInstance().GetRandomNumber() - 50 + 960;
        let widthPercent = Common.GetInstance().GetRandomNumber()/100.0;
        if(widthPercent > 0.5)
        {
            widthPercent = 0.5;
        }
        else if(widthPercent < 0.3)
        {
            widthPercent = 0.3;
        }

        block.setPosition(new cc.Vec2(position,0));
        block.setScale(widthPercent,1);
    }

    private createHeroAndStick()
    {
        //创建英雄
        this.hero = cc.instantiate(this.heroProfab).getComponent(Hero);
        this.node.addChild(this.hero.node);
        this.hero.node.setPosition(ConstData.HeroStartPos);
        this.hero.setBlockArray(this.blockArray);

        //创建棍子
        this.stick = cc.instantiate(this.stickProfab).getComponent(Stick);
        this.node.addChild(this.stick.node);
        
        let stickPos =  new cc.Vec2(ConstData.HeroStartPos.x,ConstData.HeroStartPos.y);
        stickPos.x += 10;

        this.stick.node.setPosition(stickPos);
        this.hero.setStick(this.stick.node);
    }

    private deleteBlock()
    {
        for (let index = 0; index < this.blockArray.length; index++)
        {
            let element = this.blockArray[index];
            if(element.position.x < 0)
            {
                this.blockArray[index] = null;
                element.removeFromParent(true);
                this.blockArray.shift();
                break;
            }
        }
    }

    onMouseDown = function (event: any)
    {
        this.hero.setHeroState(2);
    }.bind(this)

    onMouseUp = function (event: any)
    {
        let isCanRun = this.hero.setHeroState(3);
        if(isCanRun)
        {
            this.hero.heroRun();
            this.deleteBlock();
            this.createBlock();
        }
    }.bind(this)
}
