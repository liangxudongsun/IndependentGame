export class HttpUtils {

    private static Instance: HttpUtils = null;
    public GetInstance() {
        if (HttpUtils.Instance == null) {
            HttpUtils.Instance = new HttpUtils();
        }
        return HttpUtils.Instance;
    };

    httpGets(url, callback) {
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

        httpRequest.timeout = 5000;// 5 seconds for timeout  
        httpRequest.send();
    }

    httpPost(url, params, callback) {
        var httpRequest = cc.loader.getXMLHttpRequest();
        httpRequest.onreadystatechange = function () {
            if (httpRequest.readyState === 4 && (httpRequest.status >= 200 && httpRequest.status < 300)) {
                var respone = httpRequest.responseText;
                callback(respone);
            } else {
                callback(-1);
            }
        };

        httpRequest.open("POST", url, true);
        if (cc.sys.isNative) {
            httpRequest.setRequestHeader("Accept-Encoding", "gzip,deflate");
        }

        httpRequest.timeout = 5000;// 5 seconds for timeout  
        httpRequest.send(params);
    }
}
