using System;
using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    private ArrayList cellBugAllList = new ArrayList();   //所有精灵集合
    private ArrayList foodArrayList = new ArrayList();     //食物集合
    private CellBug nowCellBug = null;
    private int nowCellBugIndex = 0;
    void Awake()
    {

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

    public CellBug GetNowCellBug()
    {
        return nowCellBug;
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
            if (bug && bug.GetAbility().cellBugGroup == Const.CellBugGroup.GodChildEnum)
            {
                nowCellBug = bug; 
                nowCellBug.SetCamera(Camera.main);
                nowCellBugIndex = i;
                return;
            }
            continue;
        }
    }

    //在一定距离上寻找食物
    public Food SearchFoodWithDis(CellBug cellBug,float dis)
    {
        ArrayList foodList = new ArrayList();

        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);
        for (int i = 0; i < foodArrayList.Count; i++)
        {
            Food tempFood = foodArrayList[i] as Food;
            if (!tempFood.GetCanEat()) continue;
            if (powerFrom == 0 && tempFood.GetFoodType() == Const.FoodEnum.MeatEnum) continue;
            if (powerFrom == 2 && tempFood.GetFoodType() == Const.FoodEnum.GrassEnum) continue;
            if (Vector3.Magnitude(cellBug.transform.position - tempFood.transform.position) < dis)
            {
                foodList.Add(tempFood);
            }
        }

        if (foodList.Count != 0)
        {
            int seed = (int)DateTime.Now.Ticks + cellBug.GetAbility().id * 10;
            
            System.Random ranWhatIndex = new System.Random(seed);
            int num = ranWhatIndex.Next(1,foodList.Count + 1);
            Food food = foodList[num - 1] as Food;
            return food;
        }
        return null;
    }

    //在一定距离上寻找敌人
    public CellBug SearchEnemyWithDis(CellBug cellBug,float dis)
    {
        ArrayList enemyBugList = new ArrayList();

        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);
        if (powerFrom == 0) return null;

        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug tempEnemy = cellBugAllList[i] as CellBug;
            if (cellBug.GetAbility().cellBugGroup == tempEnemy.GetAbility().cellBugGroup) continue;
            if (Vector3.Magnitude(cellBug.transform.position - tempEnemy.transform.position) < dis)
            {
                enemyBugList.Add(tempEnemy);
            }
        }

        if (enemyBugList.Count != 0)
        {
            int seed = (int)DateTime.Now.Ticks + cellBug.GetAbility().id * 10;
            //那条链返回
            System.Random ranWhatIndex = new System.Random(seed);
            int num = ranWhatIndex.Next(1, enemyBugList.Count + 1);
            CellBug bug = enemyBugList[num - 1] as CellBug;
            return bug;
        }
        return null;
    }

    //寻找配偶
    public CellBug SearchMate(CellBug cellBug)
    {
        Const.CellBugGroup group = cellBug.GetAbility().cellBugGroup;
        
        ArrayList mateBugList = new ArrayList();
        for (int i = 0; i < cellBugAllList.Count;i++)
        {
            CellBug tempBug = cellBugAllList[i] as CellBug;
            if (tempBug.GetAbility().status != Const.StutasEnum.ReceviceMataEnum 
                && tempBug.GetAbility().status != Const.StutasEnum.SearchMateEnum
                && tempBug.GetAbility().cellBugGroup == group
                && tempBug != cellBug)
            {
                mateBugList.Add(tempBug);
            }
        }

        if (mateBugList.Count != 0)
        {
            int seed = (int)DateTime.Now.Ticks + 10 * cellBug.GetAbility().id;
            //那条链返回
            System.Random ranWhatIndex = new System.Random(seed);
            int num = ranWhatIndex.Next(1, mateBugList.Count + 1);
            CellBug bug = mateBugList[num - 1] as CellBug;
            return bug;
        }

        return null;
    }

    public void Change()
    {
        for (int i = (nowCellBugIndex+1)%cellBugAllList.Count; i < cellBugAllList.Count; i++)
        {
            if (i == nowCellBugIndex) return;
            CellBug bug = cellBugAllList[i] as CellBug;
            if (bug.GetAbility().cellBugGroup == Const.CellBugGroup.GodChildEnum
                && bug != nowCellBug)
            {
                    nowCellBugIndex = i;
                    nowCellBug.SetCamera(null);
                    nowCellBug = bug;
                    bug.SetCamera(Camera.main);
                    return;
            }
            if (i == cellBugAllList.Count - 1)i = -1;
        }
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
