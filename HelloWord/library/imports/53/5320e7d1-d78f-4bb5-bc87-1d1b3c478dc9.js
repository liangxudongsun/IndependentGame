"use strict";
cc._RF.push(module, '5320efR149LtbyHHRs8R43J', 'GameControl');
// Scripts/Game/GameControl.ts

Object.defineProperty(exports, "__esModule", { value: true });
var Hero_1 = require("./Hero");
var Common_1 = require("./../Tool/Common");
var ConstData_1 = require("../Tool/ConstData");
var Stick_1 = require("./Stick");
var _a = cc._decorator, ccclass = _a.ccclass, property = _a.property;
var GameControl = /** @class */ (function (_super) {
    __extends(GameControl, _super);
    function GameControl() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.heroProfab = null;
        _this.stickProfab = null;
        _this.blockProfab = null;
        _this.blockGroup = null;
        //英雄
        _this.hero = null;
        //棍子
        _this.stick = null;
        //方块数组
        _this.blockArray = [];
        _this.onMouseDown = function (event) {
            this.hero.setHeroState(2);
        }.bind(_this);
        _this.onMouseUp = function (event) {
            var isCanRun = this.hero.setHeroState(3);
            if (isCanRun) {
                this.hero.heroRun();
                this.deleteBlock();
                this.createBlock();
            }
        }.bind(_this);
        return _this;
    }
    GameControl.prototype.onLoad = function () {
        //添加事件
        this.node.on(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
        this.node.on(cc.Node.EventType.MOUSE_UP, this.onMouseUp);
    };
    GameControl.prototype.start = function () {
        this.createHeroAndStick();
    };
    GameControl.prototype.onDestory = function () {
        this.node.off(cc.Node.EventType.MOUSE_DOWN, this.onMouseDown);
        this.node.off(cc.Node.EventType.MOUSE_UP, this.onMouseUp);
    };
    GameControl.prototype.createBlock = function () {
        var block = cc.instantiate(this.blockProfab);
        this.blockArray.push(block);
        this.blockGroup.addChild(block);
        var position = Common_1.Common.GetInstance().GetRandomNumber() - 50 + 960;
        var widthPercent = Common_1.Common.GetInstance().GetRandomNumber() / 100.0;
        if (widthPercent > 0.5) {
            widthPercent = 0.5;
        }
        else if (widthPercent < 0.3) {
            widthPercent = 0.3;
        }
        block.setPosition(new cc.Vec2(position, 0));
        block.setScale(widthPercent, 1);
    };
    GameControl.prototype.createHeroAndStick = function () {
        //创建英雄
        this.hero = cc.instantiate(this.heroProfab).getComponent(Hero_1.default);
        this.node.addChild(this.hero.node);
        this.hero.node.setPosition(ConstData_1.ConstData.HeroStartPos);
        this.hero.setBlockArray(this.blockArray);
        //创建棍子
        this.stick = cc.instantiate(this.stickProfab).getComponent(Stick_1.default);
        this.node.addChild(this.stick.node);
        var stickPos = new cc.Vec2(ConstData_1.ConstData.HeroStartPos.x, ConstData_1.ConstData.HeroStartPos.y);
        stickPos.x += 10;
        this.stick.node.setPosition(stickPos);
        this.hero.setStick(this.stick.node);
    };
    GameControl.prototype.deleteBlock = function () {
        for (var index = 0; index < this.blockArray.length; index++) {
            var element = this.blockArray[index];
            if (element.position.x < 0) {
                this.blockArray[index] = null;
                element.removeFromParent(true);
                this.blockArray.shift();
                break;
            }
        }
    };
    __decorate([
        property(cc.Prefab)
    ], GameControl.prototype, "heroProfab", void 0);
    __decorate([
        property(cc.Prefab)
    ], GameControl.prototype, "stickProfab", void 0);
    __decorate([
        property(cc.Prefab)
    ], GameControl.prototype, "blockProfab", void 0);
    __decorate([
        property(cc.Node)
    ], GameControl.prototype, "blockGroup", void 0);
    __decorate([
        property([cc.Node])
    ], GameControl.prototype, "blockArray", void 0);
    GameControl = __decorate([
        ccclass
    ], GameControl);
    return GameControl;
}(cc.Component));
exports.default = GameControl;

cc._RF.pop();