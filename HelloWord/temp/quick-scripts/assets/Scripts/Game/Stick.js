(function() {"use strict";var __module = CC_EDITOR ? module : {exports:{}};var __filename = 'preview-scripts/assets/Scripts/Game/Stick.js';var __require = CC_EDITOR ? function (request) {return cc.require(request, require);} : function (request) {return cc.require(request, __filename);};function __define (exports, require, module) {"use strict";
cc._RF.push(module, '60b36oY/25ODJU239Ko+Ngs', 'Stick', __filename);
// Scripts/Game/Stick.ts

Object.defineProperty(exports, "__esModule", { value: true });
var _a = cc._decorator, ccclass = _a.ccclass, property = _a.property;
var ConstData_1 = require("./../Tool/ConstData");
var Stick = /** @class */ (function (_super) {
    __extends(Stick, _super);
    function Stick() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        //棍子的位置
        _this.stickPosition = new cc.Vec2(0, 0);
        //棍子的高度
        _this.hightPercent = 0.0;
        return _this;
    }
    // onLoad () {},
    Stick.prototype.start = function () {
        //this.node.setPosition(this.stickPosition);
        this.node.setScale(1.0, this.hightPercent);
    };
    // update (dt) {},
    Stick.prototype.onDestory = function () {
        this.node.removeFromParent(true);
    };
    /**
     * 增加筷子长度
     */
    Stick.prototype.addStickHight = function (dt) {
        this.hightPercent += ConstData_1.ConstData.StickAddHightSpeed * dt;
        if (this.hightPercent > 1) {
            this.hightPercent = 1;
        }
        this.node.setScale(1.0, this.hightPercent);
    };
    /**
     * 放倒筷子
     */
    Stick.prototype.putStickDown = function () {
        this.node.rotation = 90;
    };
    /**
     * 重置棍子
     */
    Stick.prototype.resetStick = function (position) {
        var stickPos = position;
        stickPos.x += 10;
        this.node.setPosition(stickPos);
        this.hightPercent = 0;
        this.node.setScale(1.0, this.hightPercent);
        this.node.rotation = 0;
    };
    Stick = __decorate([
        ccclass
    ], Stick);
    return Stick;
}(cc.Component));
exports.default = Stick;

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
        //# sourceMappingURL=Stick.js.map
        