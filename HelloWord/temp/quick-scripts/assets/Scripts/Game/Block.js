(function() {"use strict";var __module = CC_EDITOR ? module : {exports:{}};var __filename = 'preview-scripts/assets/Scripts/Game/Block.js';var __require = CC_EDITOR ? function (request) {return cc.require(request, require);} : function (request) {return cc.require(request, __filename);};function __define (exports, require, module) {"use strict";
cc._RF.push(module, 'ab0b5CnvJpA54lTe7wtZdLV', 'Block', __filename);
// Scripts/Game/Block.ts

Object.defineProperty(exports, "__esModule", { value: true });
var _a = cc._decorator, ccclass = _a.ccclass, property = _a.property;
var Block = /** @class */ (function (_super) {
    __extends(Block, _super);
    function Block() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        //方块位置
        _this.blockPos = new cc.Vec2(0, 0);
        //宽度比例
        _this.widthPercent = 1.0;
        return _this;
    }
    //onLoad() {}
    Block.prototype.start = function () {
    };
    Block.prototype.update = function (dt) {
    };
    Block.prototype.onDestory = function () {
        this.node.removeFromParent(true);
    };
    Block.prototype.setPosition = function (position) {
        this.blockPos = position;
    };
    Block.prototype.setWidthPercent = function (widthPercent) {
        this.widthPercent = widthPercent;
    };
    Block = __decorate([
        ccclass
    ], Block);
    return Block;
}(cc.Component));
exports.default = Block;

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
        //# sourceMappingURL=Block.js.map
        