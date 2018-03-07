(function() {"use strict";var __module = CC_EDITOR ? module : {exports:{}};var __filename = 'preview-scripts/assets/Scripts/Tool/Common.js';var __require = CC_EDITOR ? function (request) {return cc.require(request, require);} : function (request) {return cc.require(request, __filename);};function __define (exports, require, module) {"use strict";
cc._RF.push(module, '7995613PwxEnq/x52tQxl4y', 'Common', __filename);
// Scripts/Tool/Common.ts

Object.defineProperty(exports, "__esModule", { value: true });
var Common = /** @class */ (function () {
    function Common() {
    }
    Common.GetInstance = function () {
        if (this.Instance == null) {
            this.Instance = new Common();
        }
        return this.Instance;
    };
    //返回一个0~100的数
    Common.prototype.GetRandomNumber = function () {
        return Math.random() * 100;
    };
    Common.Instance = null;
    return Common;
}());
exports.Common = Common;

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
        //# sourceMappingURL=Common.js.map
        