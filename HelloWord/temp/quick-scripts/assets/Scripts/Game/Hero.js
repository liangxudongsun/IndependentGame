(function() {"use strict";var __module = CC_EDITOR ? module : {exports:{}};var __filename = 'preview-scripts/assets/Scripts/Game/Hero.js';var __require = CC_EDITOR ? function (request) {return cc.require(request, require);} : function (request) {return cc.require(request, __filename);};function __define (exports, require, module) {"use strict";
cc._RF.push(module, '54807Xilg1P8p7DRIaniblv', 'Hero', __filename);
// Scripts/Game/Hero.ts

Object.defineProperty(exports, "__esModule", { value: true });
var _a = cc._decorator, ccclass = _a.ccclass, property = _a.property;
var ConstData_1 = require("./../Tool/ConstData");
var Stick_1 = require("./Stick");
//英雄状态
var HeroState;
(function (HeroState) {
    HeroState[HeroState["HeroIdleEnum"] = 1] = "HeroIdleEnum";
    HeroState[HeroState["HeroAddStick"] = 2] = "HeroAddStick";
    HeroState[HeroState["HeroRunEnum"] = 3] = "HeroRunEnum";
    HeroState[HeroState["HeroDropEnum"] = 4] = "HeroDropEnum";
})(HeroState || (HeroState = {}));
var Hero = /** @class */ (function (_super) {
    __extends(Hero, _super);
    function Hero() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        //英雄位置
        _this.heroPosition = new cc.Vec2(0, 0);
        //英雄状态
        _this.heroState = HeroState.HeroIdleEnum;
        //棍子
        _this.stick = null;
        //英雄移动距离
        _this.heroMoveDis = 0;
        //场景跳转
        _this.replaceSceneTimeFun = function (event) {
            cc.director.loadScene("scene");
        }.bind(_this);
        //跑完检测
        _this.runTimeFun = function (event) {
            this.heroState = HeroState.HeroIdleEnum;
            if (this.checkHeroState()) {
                this.moveBlock();
            }
        }.bind(_this);
        return _this;
    }
    Hero.prototype.start = function () {
        this.heroState = HeroState.HeroIdleEnum;
    };
    Hero.prototype.update = function (dt) {
        switch (this.heroState) {
            case HeroState.HeroIdleEnum:
                break;
            case HeroState.HeroAddStick:
                this.motorStickHight(dt);
                break;
            case HeroState.HeroRunEnum:
                break;
            case HeroState.HeroDropEnum:
                this.heroDrop();
                break;
        }
    };
    Hero.prototype.onDestory = function () {
        this.node.removeFromParent(true);
    };
    /**
     * 设置英雄状态
     */
    Hero.prototype.setHeroState = function (state) {
        switch (state) {
            case 2:
                if (this.heroState == HeroState.HeroIdleEnum) {
                    this.stick.resetStick(this.node.getPosition());
                    this.heroState = HeroState.HeroAddStick;
                    return true;
                }
                return false;
            case 3:
                if (this.heroState == HeroState.HeroAddStick) {
                    this.heroState = HeroState.HeroRunEnum;
                    return true;
                }
                return false;
        }
        return false;
    };
    /**
     * 设置棍子索引
     */
    Hero.prototype.setStick = function (stick) {
        this.stick = stick.getComponent(Stick_1.default);
    };
    /**
     * 设置方块数组索引
     */
    Hero.prototype.setBlockArray = function (blockArray) {
        this.blockArray = blockArray;
    };
    /*
    * 驱动棍子生长
    */
    Hero.prototype.motorStickHight = function (dt) {
        this.stick.addStickHight(dt);
    };
    /**
     * 英雄行走
     */
    Hero.prototype.heroRun = function (dt) {
        this.stick.putStickDown();
        var distance = this.stick.node.getBoundingBox().size.width;
        this.heroMoveDis = distance + 20;
        var tempPos = new cc.Vec2(this.node.position.x + this.heroMoveDis, 50);
        var rect = cc.rect(tempPos.x, tempPos.y, this.node.getBoundingBox().width, this.node.getBoundingBox().height);
        for (var index = 0; index < this.blockArray.length; index++) {
            var element = this.blockArray[index];
            if (element.getBoundingBox().containsRect(rect)) {
                var targetX = element.position.x + element.getBoundingBox().width / 2;
                var addDis = targetX - tempPos.x;
                this.heroMoveDis += addDis - rect.width / 2;
                break;
            }
        }
        var time = this.heroMoveDis / ConstData_1.ConstData.HeroRunSpeed;
        var moveAction = cc.moveBy(time, new cc.Vec2(this.heroMoveDis, 0));
        this.node.runAction(moveAction);
        this.scheduleOnce(this.runTimeFun, time);
    };
    /*
    * 英雄掉落
    */
    Hero.prototype.heroDrop = function () {
        var moveAction = cc.moveBy(ConstData_1.ConstData.HeroTimeDrop, new cc.Vec2(10, -ConstData_1.ConstData.HeroDisDrop));
        this.node.runAction(moveAction);
        this.heroState = HeroState.HeroIdleEnum;
        this.scheduleOnce(this.replaceSceneTimeFun, ConstData_1.ConstData.HeroTimeDrop);
    };
    /*
    * 检测是否掉落
    */
    Hero.prototype.checkHeroState = function () {
        var live = false;
        for (var index = 0; index < this.blockArray.length; index++) {
            var element = this.blockArray[index];
            var blockSize = element.getBoundingBox();
            var heroSize = this.node.getBoundingBox();
            heroSize.y = 50;
            if (blockSize.containsRect(heroSize)) {
                live = true;
                return live;
            }
        }
        this.heroState = HeroState.HeroDropEnum;
        return live;
    };
    /*
    * 移动方块
    */
    Hero.prototype.moveBlock = function () {
        var heroRunAction = cc.moveBy(0.5, new cc.Vec2(-this.heroMoveDis, 0));
        this.node.runAction(heroRunAction);
        var stickRunAction = cc.moveBy(0.5, new cc.Vec2(-this.heroMoveDis, 0));
        this.stick.node.runAction(stickRunAction);
        for (var index = 0; index < this.blockArray.length; index++) {
            var element = this.blockArray[index];
            var elementRunAction = cc.moveBy(0.5, new cc.Vec2(-this.heroMoveDis, 0));
            element.runAction(elementRunAction);
        }
    };
    Hero = __decorate([
        ccclass
    ], Hero);
    return Hero;
}(cc.Component));
exports.default = Hero;

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
        //# sourceMappingURL=Hero.js.map
        