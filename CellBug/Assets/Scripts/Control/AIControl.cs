using System;
using System.Runtime;
using UnityEngine;
using System.Collections;

public class AIControl{

    private CellBug mate = null;
    private Food food = null;
    private CellBug cellBugEnemy = null;
    private Vector3 targetPosition = new Vector3(0,0,-2);
    private float createTargetTime = 0.0f;

    public void UpdateStatus(CellBug cellBug)
    {
        createTargetTime -= Time.deltaTime;
        if (createTargetTime <= 0.0f)
        {
            CreateTargetPosition(cellBug);
            createTargetTime = Const.TimeForCreatePosition;
        }

        Const.StutasEnum status = cellBug.GetAbility().GetStatus();
        switch (status)
        {
            case Const.StutasEnum.IdleEnum:
                CheckPower(cellBug);
                break;
            case Const.StutasEnum.SearchMateEnum:
                SearchMate(cellBug);
                cellBug.SearchMateStatus();
                break;
            case Const.StutasEnum.AttackEnum:
                SerarchEnemy(cellBug);
                cellBug.Attack();
                break;
            case Const.StutasEnum.EatMeatEnum:
                SearchFood(cellBug);
                cellBug.EatMeat();
                break;
            case Const.StutasEnum.EatPlantEnum:
                SearchFood(cellBug);
                cellBug.EatPlant();
                break;
        }
    }

    private void CheckPower(CellBug cellBug)
    {
        //无所事事时
        if (cellBug.GetAbility().isArrive)cellBug.Move(targetPosition);
        if (SearchMate(cellBug)) return;
        if (SearchFood(cellBug)) return;
        if (SerarchEnemy(cellBug)) return;
    }

    private bool SearchMate(CellBug cellBug)
    {
        //寻找配偶
        mate = cellBug.GetGameControl().SearchMate(cellBug);
        if (mate != null
            && cellBug.GetAbility().GetPower() >= Const.PowerForMate
            && cellBug.GetAbility().GetCanMate())
        {
            cellBug.ReadyMate(mate.gameObject);
            return true;
        }
        return false;
    }

    private bool SearchFood(CellBug cellBug)
    {
        //寻找食物
        food = cellBug.GetGameControl().SearchFoodWithDis(cellBug, Const.DisForEatFood);
        if (food && cellBug.GetAbility().GetPower() < Const.PowerForStarve)
        {
            cellBug.ReadyEat(food.gameObject);
            return true;
        }
        return false;
    }

    private bool SerarchEnemy(CellBug cellBug)
    {
        //寻找敌人杀死来食用
        cellBugEnemy = cellBug.GetGameControl().SearchEnemyWithDis(cellBug, Const.DisForEatEnemy);
        if (cellBugEnemy != null && cellBug.GetAbility().GetPower() < Const.PowerForStarve)
        {
            cellBug.ReadyAttack(cellBugEnemy.gameObject);
            return true;
        }
        return false;
    }

    public void UpdatePosition(CellBug cellBug)
    {
        if (!cellBug.GetAbility().isArrive)
        {
            Vector3 dir = cellBug.GetAbility().targetPos - cellBug.transform.position;
            dir = dir.normalized; dir.z = 0;

            float angle = Vector3.Angle(new Vector3(0, -1, 0), dir);
            if (dir.x < 0) angle = 360 - angle;
            cellBug.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(cellBug.transform.eulerAngles.z, angle, Time.deltaTime * 10));

            cellBug.transform.position += dir * cellBug.GetAbility().GetSpeed() * Time.deltaTime;
            if (Vector3.Distance(cellBug.GetAbility().targetPos, cellBug.transform.position) <= 0.1f)
            {
                cellBug.GetAbility().isArrive = true;
            }
        }
    }

    private void CreateTargetPosition(CellBug cellBug)
    {
        int seed = (int)DateTime.Now.Ticks + cellBug.GetAbility().GetId() * 10;
        System.Random random = new System.Random(seed);
        int x = random.Next(-50,50);
        int y = random.Next(-50,50);
        targetPosition = new Vector3(x,y,cellBug.transform.position.z);
    }
}

