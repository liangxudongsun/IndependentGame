(function() {"use strict";var __module = CC_EDITOR ? module : {exports:{}};var __filename = 'preview-scripts/assets/Scripts/Tool/ConstData.js';var __require = CC_EDITOR ? function (request) {return cc.require(request, require);} : function (request) {return cc.require(request, __filename);};function __define (exports, require, module) {"use strict";
cc._RF.push(module, '65ef2v6xw5CqoPUdH1HuG3R', 'ConstData', __filename);
// Scripts/Tool/ConstData.ts

Object.defineProperty(exports, "__esModule", { value: true });
var ConstData = /** @class */ (function () {
    function ConstData() {
    }
    ConstData.StickAddHightSpeed = 0.3;
    ConstData.HeroRunSpeed = 100;
    ConstData.HeroDisDrop = 500;
    ConstData.HeroTimeDrop = 1.0;
    ConstData.HeroStartPos = new cc.Vec2(145, 300);
    return ConstData;
}());
exports.ConstData = ConstData;

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
        //# sourceMappingURL=ConstData.js.map
        