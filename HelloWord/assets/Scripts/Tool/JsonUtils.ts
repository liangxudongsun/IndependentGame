export class JsonUtils {
    private static Instance: JsonUtils = null;
    public static GetInstance() {
        if (this.Instance == null) {
            this.Instance = new JsonUtils();
        }
        return this.Instance;
    }

    public JsonParse(jsonPath: string = "", parseMath: any = this.parseMath) {
        cc.loader.loadRes(jsonPath, parseMath);
    }

    parseMath = function (err, res) {
        //输出错误
        cc.log('err[' + err + ']');
        //JSON.stringify(res)是使用json库中的方法将json文件转换为字符串。
        let jsonStr: string = JSON.stringify(res);
        cc.log(jsonStr);
        //将字符串转换为json对象
        var jsonObject = JSON.parse(JSON.stringify(res));
        //从json对象中取数据
        var name = jsonObject[0].FloorTexture[0].name;
        cc.log(name);
    }
}
