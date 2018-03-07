"use strict";
cc._RF.push(module, 'f599arGRDRBUZPK6F+O3ol7', 'HttpUtils');
// Scripts/Tool/HttpUtils.ts

Object.defineProperty(exports, "__esModule", { value: true });
var HttpUtils = /** @class */ (function () {
    function HttpUtils() {
    }
    HttpUtils.prototype.GetInstance = function () {
        if (HttpUtils.Instance == null) {
            HttpUtils.Instance = new HttpUtils();
        }
        return HttpUtils.Instance;
    };
    ;
    HttpUtils.prototype.httpGets = function (url, callback) {
        var httpRequest = cc.loader.getXMLHttpRequest();
        httpRequest.onreadystatechange = function () {
            if (httpRequest.readyState === 4 && (httpRequest.status >= 200 && httpRequest.status < 300)) {
                var respone = httpRequest.responseText;
                callback(respone);
            }
        };
        httpRequest.open("GET", url, true);
        if (cc.sys.isNative) {
            httpRequest.setRequestHeader("Accept-Encoding", "gzip,deflate");
        }
        httpRequest.timeout = 5000; // 5 seconds for timeout  
        httpRequest.send();
    };
    HttpUtils.prototype.httpPost = function (url, params, callback) {
        var httpRequest = cc.loader.getXMLHttpRequest();
        httpRequest.onreadystatechange = function () {
            if (httpRequest.readyState === 4 && (httpRequest.status >= 200 && httpRequest.status < 300)) {
                var respone = httpRequest.responseText;
                callback(respone);
            }
            else {
                callback(-1);
            }
        };
        httpRequest.open("POST", url, true);
        if (cc.sys.isNative) {
            httpRequest.setRequestHeader("Accept-Encoding", "gzip,deflate");
        }
        httpRequest.timeout = 5000; // 5 seconds for timeout  
        httpRequest.send(params);
    };
    HttpUtils.Instance = null;
    return HttpUtils;
}());
exports.HttpUtils = HttpUtils;

cc._RF.pop();