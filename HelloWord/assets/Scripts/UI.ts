// Learn TypeScript:
//  - [Chinese] http://www.cocos.com/docs/creator/scripting/typescript.html
//  - [English] http://www.cocos2d-x.org/docs/editors_and_tools/creator-chapters/scripting/typescript/index.html
// Learn Attribute:
//  - [Chinese] http://www.cocos.com/docs/creator/scripting/reference/attributes.html
//  - [English] http://www.cocos2d-x.org/docs/editors_and_tools/creator-chapters/scripting/reference/attributes/index.html
// Learn life-cycle callbacks:
//  - [Chinese] http://www.cocos.com/docs/creator/scripting/life-cycle-callbacks.html
//  - [English] http://www.cocos2d-x.org/docs/editors_and_tools/creator-chapters/scripting/life-cycle-callbacks/index.html

const { ccclass, property } = cc._decorator;
import { Common } from "./Tool/Common";
import { JsonUtils } from "./Tool/JsonUtils";

@ccclass
export default class UI extends cc.Component
{
    @property(cc.Label)
    label: cc.Label = null;

    @property
    text: string = 'hello';

    @property
    nameLxd: string = "rdy";

    private timer: number = 1;

    onLoad()
    {
        cc.systemEvent.on(cc.SystemEvent.EventType.KEY_DOWN, this.onKeyDown, this);
        cc.systemEvent.on(cc.SystemEvent.EventType.KEY_UP, this.onKeyUp, this);
    }

    start()
    {
        this.label.string = this.nameLxd;

        var node = new cc.Node('Sprite');
        var sprite = node.addComponent(cc.Sprite);
        sprite.spriteFrame = new cc.SpriteFrame(cc.url.raw('resources/Texture/3.png'));
        sprite.node.setPosition(new cc.Vec2(1136 / 2, 640 / 2));
        this.node.addChild(sprite.node);

        var rotate = cc.rotateBy(2.0, 180.0);
        sprite.node.runAction(cc.repeatForever(rotate));

        JsonUtils.GetInstance().JsonParse("Json/floorTextureMap.json");
    }

    update(dt: number)
    {
        this.timer = Common.GetInstance().Add(this.timer);
        this.label.string = this.timer.toString();
    }

    onDestroy()
    {
        cc.systemEvent.off(cc.SystemEvent.EventType.KEY_DOWN, this.onKeyDown, this);
        cc.systemEvent.off(cc.SystemEvent.EventType.KEY_UP, this.onKeyUp, this);
    }

    onKeyDown(event: any)
    {
        switch (event.keyCode)
        {
            case cc.KEY.a:
                console.log('Press a key');
                break;
        }
    }

    onKeyUp(event: any)
    {
        switch (event.keyCode)
        {
            case cc.KEY.a:
                console.log('release a key');
                break;
        }
    }
}
