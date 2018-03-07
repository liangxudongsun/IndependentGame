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

@ccclass
export default class MouseTest extends cc.Component 
{
    start() 
    {
        // 使用枚举类型来注册
        this.node.on(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
    }

    onDestroy()
    {
        this.node.off(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
    }

    onMouseDown = function (event: any)
    {
        //虽然点到才可以触发,但得到的是屏幕坐标
        var wordPos = event.getLocation();
        //转换成此对象局部坐标
        var nodePos = this.node.convertToNodeSpaceAR(wordPos);
        cc.log(nodePos.x + ":" + nodePos.y);
    }.bind(this)
}
