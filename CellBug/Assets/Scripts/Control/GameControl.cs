﻿using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    private Camera camera;
    private ArrayList cellBugAllList = new ArrayList();   //所有精灵集合
    private ArrayList foodArrayList = new ArrayList();     //食物集合
 
    private CellBug nowCellBug = null;

    void Awake()
    {
        camera = Camera.main;
    }

	// Use this for initialization
	void Start () {
        //SearchCellBug();
 	}
	
	// Update is called once per frame
	void Update () {
        if (!nowCellBug)
        {
            SearchCellBug();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            nowCellBug.TapCheck(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
	}

    public void ReSet()
    {
        SearchCellBug();
        if (!nowCellBug)
        {
            //游戏结束
        }
    }

    private void SearchCellBug()
    {
        for (int i = 0; i < cellBugAllList.Count;i++)
        {
            CellBug bug = cellBugAllList[i] as CellBug;
            if (bug && bug.GetAbility().cellBugGroup == Const.CellBugGroup.MineEnum)
            {
                nowCellBug = bug; 
                nowCellBug.SetCamera(camera);
                return;
            }
            continue;
        }
    }

    //在一定距离上寻找食物
    public Food SearchFoodWithDis(CellBug cellBug,float dis)
    {
        Food food = null;
        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);

        for (int i = 0; i < foodArrayList.Count; i++)
        {
            Food tempFood = foodArrayList[i] as Food;
            if (powerFrom == 0 && tempFood.GetFoodType() == Const.FoodEnum.MeatEnum) continue;
            if (powerFrom == 2 && tempFood.GetFoodType() == Const.FoodEnum.GrassEnum) continue;

            if (Vector3.Magnitude(cellBug.transform.position - tempFood.transform.position) < dis)
            {
                food = tempFood;
                break;
            }
        }
        return food;
    }

    //在一定距离上寻找敌人
    public CellBug SearchEnemyWithDis(CellBug cellBug,float dis)
    {
        CellBug enemy = null;
        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);
        if (powerFrom == 0) return null;

        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug tempEnemy = cellBugAllList[i] as CellBug;
            if (cellBug.GetAbility().cellBugGroup == tempEnemy.GetAbility().cellBugGroup) continue;
            if (Vector3.Magnitude(cellBug.transform.position - tempEnemy.transform.position) < dis)
            {
                enemy = tempEnemy;
                break;
            }
        }
        return enemy;
    }

    public void AddCellBugAll(CellBug cellBug)
    {
        cellBugAllList.Add(cellBug);
    }

    public void DeleteCellBugAll(CellBug cellBug)
    {
        if (cellBug == nowCellBug) nowCellBug = null;
        cellBugAllList.Remove(cellBug);
    }

    public void AddFood(Food food)
    {
        foodArrayList.Add(food);
    }

    public void DeleteFood(Food food)
    {
        foodArrayList.Remove(food);
    }
}
