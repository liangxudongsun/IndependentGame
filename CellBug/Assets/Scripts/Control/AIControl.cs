using UnityEngine;
using System.Collections;

public class AIControl{
    
    public void UpdateStatus(CellBug cellBug)
    {
        Const.StutasEnum status = cellBug.GetAbility().status;
        switch (status)
        {
            case Const.StutasEnum.IdleEnum:
                CheckPower(cellBug);
                break;
            case Const.StutasEnum.ReceviceMataEnum:
                break;
            case Const.StutasEnum.SearchMateEnum:
                cellBug.SearchMateStatus();
                break;
            case Const.StutasEnum.AttackEnum:
                cellBug.Attack();
                break;
            case Const.StutasEnum.EatMeatEnum:
                cellBug.EatMeat();
                break;
            case Const.StutasEnum.EatPlantEnum:
                cellBug.EatPlant();
                break;
        }
    }

    private void CheckPower(CellBug cellBug)
    {
        //寻找配偶
        CellBug mate = cellBug.GetGameControl().SearchMate(cellBug);
        if (cellBug.GetAbility().GetPower() >= Const.powerForMate && cellBug.GetAbility().GetCanMate())
        {
            cellBug.ReadyMate(mate.gameObject);
            return;
        }

        //寻找食物
        Food food = cellBug.GetGameControl().SearchFoodWithDis(cellBug, Const.disForEatFood);
        if (food && cellBug.GetAbility().GetPower() < Const.powerForStarve)
        {
            cellBug.ReadyEat(food.gameObject);
            return;
        }

        //寻找敌人杀死来食用
        CellBug cellBugEnemy = cellBug.GetGameControl().SearchEnemyWithDis(cellBug, Const.disForEatEnemy);
        if (cellBugEnemy && cellBug.GetAbility().GetPower() < Const.powerForStarve)
        {
            cellBug.ReadyAttack(cellBugEnemy.gameObject);
            return;
        }

        //无所事事时

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

            cellBug.transform.position += dir * cellBug.GetAbility().speed * Time.deltaTime;
            if (Vector3.Distance(cellBug.GetAbility().targetPos, cellBug.transform.position) <= 0.1f)
            {
                cellBug.GetAbility().isArrive = true;
            }
        }
    }
}

