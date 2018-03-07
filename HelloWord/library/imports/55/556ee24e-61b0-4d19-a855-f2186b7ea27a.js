"use strict";
cc._RF.push(module, '556eeJOYbBNGahV8hhrfqJ6', 'UI');
// Scripts/UI.ts

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
var Common_1 = require("./Tool/Common");
var JsonUtils_1 = require("./Tool/JsonUtils");
var UI = /** @class */ (function (_super) {
    __extends(UI, _super);
    function UI() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.label = null;
        _this.text = 'hello';
        _this.nameLxd = "rdy";
        _this.timer = 1;
        return _this;
    }
    UI.prototype.onLoad = function () {
        cc.systemEvent.on(cc.SystemEvent.EventType.KEY_DOWN, this.onKeyDown, this);
        cc.systemEvent.on(cc.SystemEvent.EventType.KEY_UP, this.onKeyUp, this);
    };
    UI.prototype.start = function () {
        this.label.string = this.nameLxd;
        var node = new cc.Node('Sprite');
        var sprite = node.addComponent(cc.Sprite);
        sprite.spriteFrame = new cc.SpriteFrame(cc.url.raw('resources/Texture/3.png'));
        sprite.node.setPosition(new cc.Vec2(1136 / 2, 640 / 2));
        this.node.addChild(sprite.node);
        var rotate = cc.rotateBy(2.0, 180.0);
        sprite.node.runAction(cc.repeatForever(rotate));
        JsonUtils_1.JsonUtils.GetInstance().JsonParse("Json/floorTextureMap.json");
    };
    UI.prototype.update = function (dt) {
        this.timer = Common_1.Common.GetInstance().Add(this.timer);
        this.label.string = this.timer.toString();
    };
    UI.prototype.onDestroy = function () {
        cc.systemEvent.off(cc.SystemEvent.EventType.KEY_DOWN, this.onKeyDown, this);
        cc.systemEvent.off(cc.SystemEvent.EventType.KEY_UP, this.onKeyUp, this);
    };
    UI.prototype.onKeyDown = function (event) {
        switch (event.keyCode) {
            case cc.KEY.a:
                console.log('Press a key');
                break;
        }
    };
    UI.prototype.onKeyUp = function (event) {
        switch (event.keyCode) {
            case cc.KEY.a:
                console.log('release a key');
                break;
        }
    };
    __decorate([
        property(cc.Label)
    ], UI.prototype, "label", void 0);
    __decorate([
        property
    ], UI.prototype, "text", void 0);
    __decorate([
        property
    ], UI.prototype, "nameLxd", void 0);
    UI = __decorate([
        ccclass
    ], UI);
    return UI;
}(cc.Component));
exports.default = UI;

cc._RF.pop();