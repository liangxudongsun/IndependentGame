(function() {"use strict";var __module = CC_EDITOR ? module : {exports:{}};var __filename = 'preview-scripts/assets/Scripts/MouseTest.js';var __require = CC_EDITOR ? function (request) {return cc.require(request, require);} : function (request) {return cc.require(request, __filename);};function __define (exports, require, module) {"use strict";
cc._RF.push(module, 'e45e8X033BKk4MZqW4KcESP', 'MouseTest', __filename);
// Scripts/MouseTest.ts

// Learn TypeScript:
//  - [Chinese] http://www.cocos.com/docs/creator/scripting/typescript.html
//  - [English] http://www.cocos2d-x.org/docs/editors_and_tools/creator-chapters/scripting/typescript/index.html
// Learn Attribute:
//  - [Chinese] http://www.cocos.com/docs/creator/scripting/reference/attributes.html
//  - [English] http://www.cocos2d-x.org/docs/editors_and_tools/creator-chapters/scripting/reference/attributes/index.html
// Learn life-cycle callbacks:
//  - [Chinese] http://www.cocos.com/docs/creator/scripting/life-cycle-callbacks.html
//  - [English] http://www.cocos2d-x.org/docs/editors_and_tools/creator-chapters/scripting/life-cycle-callbacks/index.html
Object.defineProperty(exports, "__esModule", { value: true });
var _a = cc._decorator, ccclass = _a.ccclass, property = _a.property;
var MouseTest = /** @class */ (function (_super) {
    __extends(MouseTest, _super);
    function MouseTest() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.onMouseDown = function (event) {
            //虽然点到才可以触发,但得到的是屏幕坐标
            var wordPos = event.getLocation();
            //转换成此对象局部坐标
            var nodePos = this.node.convertToNodeSpaceAR(wordPos);
            cc.log(nodePos.x + ":" + nodePos.y);
        }.bind(_this);
        return _this;
    }
    MouseTest.prototype.start = function () {
        // 使用枚举类型来注册
        this.node.on(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
    };
    MouseTest.prototype.onDestroy = function () {
        this.node.off(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
    };
    MouseTest = __decorate([
        ccclass
    ], MouseTest);
    return MouseTest;
}(cc.Component));
exports.default = MouseTest;

cc._RF.pop();
        }
        if (CC_EDITOR) {
            __define(__module.exports, __require, __module);
        }
        else {
            cc.registerModuleFunc(__filename, function () {
                __define(__module.exports, __require, __module);
            });
        }
        })();
        //# sourceMappingURL=MouseTest.js.map
        