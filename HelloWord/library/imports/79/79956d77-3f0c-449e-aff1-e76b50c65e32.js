"use strict";
cc._RF.push(module, '7995613PwxEnq/x52tQxl4y', 'Common');
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