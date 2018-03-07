"use strict";
cc._RF.push(module, '0d7a6IZ02RK1JGtW4lnoCJ/', 'JsonUtils');
// Scripts/Tool/JsonUtils.ts

Object.defineProperty(exports, "__esModule", { value: true });
var JsonUtils = /** @class */ (function () {
    function JsonUtils() {
        this.parseMath = function (err, res) {
            //输出错误
            cc.log('err[' + err + ']');
            //JSON.stringify(res)是使用json库中的方法将json文件转换为字符串。
            var jsonStr = JSON.stringify(res);
            cc.log(jsonStr);
            //将字符串转换为json对象
            var jsonObject = JSON.parse(JSON.stringify(res));
            //从json对象中取数据
            var name = jsonObject[0].FloorTexture[0].name;
            cc.log(name);
        };
    }
    JsonUtils.GetInstance = function () {
        if (this.Instance == null) {
            this.Instance = new JsonUtils();
        }
        return this.Instance;
    };
    JsonUtils.prototype.JsonParse = function (jsonPath, parseMath) {
        if (jsonPath === void 0) { jsonPath = ""; }
        if (parseMath === void 0) { parseMath = this.parseMath; }
        cc.loader.loadRes(jsonPath, parseMath);
    };
    JsonUtils.Instance = null;
    return JsonUtils;
}());
exports.JsonUtils = JsonUtils;

cc._RF.pop();