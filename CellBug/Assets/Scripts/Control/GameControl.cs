using System;
using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{

    private ArrayList cellBugAllList = new ArrayList();   //所有精灵集合
    private ArrayList foodArrayList = new ArrayList();     //食物集合
    private CellBug nowCellBug = null;
    private int nowCellBugIndex = 0;

    public UILabel groupNum;
    public UILabel gameTime;
    public UISprite gameStatus;
    public UILabel alertLabel;
    public UILabel[] dnaLabelArray;
    public UILabel[] otherGroupLabelArray;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!nowCellBug)
        {
            SearchCellBug();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            nowCellBug.TapCheck(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        TimeVision(Time.time);
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
        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug bug = cellBugAllList[i] as CellBug;
            if (bug && bug.GetAbility().GetGroup() == Const.CellBugGroup.GodChildEnum)
            {
                nowCellBug = bug;
                nowCellBug.SetCamera(Camera.main);
                nowCellBugIndex = i;
                break;
            }
            continue;
        }
        DnaVision();
    }

    //在一定距离上寻找食物
    public Food SearchFoodWithDis(CellBug cellBug, float dis)
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
            int seed = (int)DateTime.Now.Ticks + cellBug.GetAbility().GetId() * 10;

            System.Random ranWhatIndex = new System.Random(seed);
            int num = ranWhatIndex.Next(1, foodList.Count + 1);
            Food food = foodList[num - 1] as Food;
            return food;
        }
        return null;
    }

    //在一定距离上寻找敌人
    public CellBug SearchEnemyWithDis(CellBug cellBug, float dis)
    {
        ArrayList enemyBugList = new ArrayList();

        int powerFrom = Const.geneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);
        if (powerFrom == 0) return null;

        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug tempEnemy = cellBugAllList[i] as CellBug;
            if (cellBug.GetAbility().GetGroup() == tempEnemy.GetAbility().GetGroup()) continue;
            if (Vector3.Magnitude(cellBug.transform.position - tempEnemy.transform.position) < dis)
            {
                enemyBugList.Add(tempEnemy);
            }
        }

        if (enemyBugList.Count != 0)
        {
            int seed = (int)DateTime.Now.Ticks + cellBug.GetAbility().GetId() * 10;
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
        Const.CellBugGroup group = cellBug.GetAbility().GetGroup();

        ArrayList mateBugList = new ArrayList();
        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug tempBug = cellBugAllList[i] as CellBug;
            if (tempBug.GetAbility().GetStatus() != Const.StutasEnum.SearchMateEnum
                && tempBug.GetAbility().GetGroup() == group
                && tempBug != cellBug)
            {
                mateBugList.Add(tempBug);
            }
        }

        if (mateBugList.Count != 0)
        {
            int seed = (int)DateTime.Now.Ticks + 10 * cellBug.GetAbility().GetId();
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
        for (int i = (nowCellBugIndex + 1) % cellBugAllList.Count; i < cellBugAllList.Count; i++)
        {
            if (i == nowCellBugIndex) return;
            CellBug bug = cellBugAllList[i] as CellBug;
            if (bug.GetAbility().GetGroup() == Const.CellBugGroup.GodChildEnum
                && bug != nowCellBug)
            {
                nowCellBugIndex = i;
                nowCellBug.SetCamera(null);
                nowCellBug = bug;
                bug.SetCamera(Camera.main);
                break;
            }
            if (i == cellBugAllList.Count - 1) i = -1;
        }

        DnaVision();
    }

    public void AddCellBugAll(CellBug cellBug)
    {
        cellBugAllList.Add(cellBug);
        GroupNumChangeVision();
    }

    public void DeleteCellBugAll(CellBug cellBug)
    {
        if (cellBug == nowCellBug) nowCellBug = null;
        cellBugAllList.Remove(cellBug);
        GroupNumChangeVision();
    }

    public void AddFood(Food food)
    {
        foodArrayList.Add(food);
    }

    public void DeleteFood(Food food)
    {
        foodArrayList.Remove(food);
    }

    private void DnaVision()
    {
        if (nowCellBug)
        {
            for (int m = 0; m < dnaLabelArray.Length; m++)
            {
                dnaLabelArray[m].text = Const.DnaName[m] + "(" + nowCellBug.GetAbility().GetDna().GetDnaIndex(Const.DnaLineEnum.OneEnum, (Const.GenesEnum)m) + "-"
                    + nowCellBug.GetAbility().GetDna().GetDnaIndex(Const.DnaLineEnum.TwoEnum, (Const.GenesEnum)m) + ")";
            }
        }
    }

    private void TimeVision(float time)
    {
        int hour = (int)(time / 3600);
        int minte = (int)((time - 3600 * hour) / 60);
        int second = (int)(time % 60);

        gameTime.text = "" + hour + ":" + minte + ":" + second;
    }

    private void GroupNumChangeVision(Const.CellBugGroup group = Const.CellBugGroup.GodChildEnum)
    {
        int numGodChild = 0;
        int numOrc = 0;
        int numHuman = 0;
        int numEidolon = 0;

        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug bug = cellBugAllList[i] as CellBug;
            if (bug.GetAbility().GetGroup() == group) numGodChild++;
            if (bug.GetAbility().GetGroup() == Const.CellBugGroup.OrcEnum) numOrc++;
            if (bug.GetAbility().GetGroup() == Const.CellBugGroup.HumanEnum) numHuman++;
            if (bug.GetAbility().GetGroup() == Const.CellBugGroup.EidolonEnum) numEidolon++;
        }

        groupNum.text = "group number:" + numGodChild;
        otherGroupLabelArray[0].text = "Orc number:" + numOrc;
        otherGroupLabelArray[1].text = "Human number:" + numHuman;
        otherGroupLabelArray[2].text = "Eidolon number:" + numEidolon;
    }

    public void AlertVision(CellBug cellBug,string message)
    {
        if (cellBug != nowCellBug) return;

        alertLabel.text = message;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        alertLabel.text = "";
    }

    public void StatusVision(CellBug cellBug)
    {
        if (cellBug != nowCellBug) return;
        switch (cellBug.GetAbility().GetStatus())
        {
            case Const.StutasEnum.AttackEnum:
                gameStatus.spriteName = "attack";
                break;
            case Const.StutasEnum.EatMeatEnum:
                gameStatus.spriteName = "meat";
                break;
            case Const.StutasEnum.EatPlantEnum:
                gameStatus.spriteName = "plant";
                break;
            case Const.StutasEnum.IdleEnum:
                gameStatus.spriteName = "idle";
                break;
            case Const.StutasEnum.SearchMateEnum:
                gameStatus.spriteName = "search";
                break;
        }
    }
}
