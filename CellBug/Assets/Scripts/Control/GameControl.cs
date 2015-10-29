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
    public UILabel[] geneLabelArray;
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
        if (Input.GetMouseButtonDown(0)
            && UICamera.hoveredObject == null)
        {
            nowCellBug.TapCheck(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        TimeVision(Time.timeSinceLevelLoad);
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
                nowCellBug.SetIsAI(false);
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

        int powerFrom = Const.GeneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);
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
            Food food = foodList[0] as Food;
            for (int m = 1; m < foodList.Count; m++)
            {
                Food tempFood = foodList[m] as Food;
                if (Vector3.Magnitude(cellBug.transform.position - tempFood.transform.position)
                    < Vector3.Magnitude(cellBug.transform.position - food.transform.position))
                    food = tempFood;
            }
            return food;
        }
        return null;
    }

    //在一定距离上寻找敌人
    public CellBug SearchEnemyWithDis(CellBug cellBug, float dis)
    {
        ArrayList enemyBugList = new ArrayList();

        int powerFrom = Const.GeneArray[(int)Const.GenesEnum.PowerGetFromEnum].GetPowerGetFrom(cellBug);
        if (powerFrom == 0) return null;

        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug tempCellBug = cellBugAllList[i] as CellBug;
            if (cellBug.GetAbility().GetGroup() == tempCellBug.GetAbility().GetGroup()) continue;
            if (Vector3.Magnitude(cellBug.transform.position - tempCellBug.transform.position) < dis
                && tempCellBug.GetAbility().GetPower() < cellBug.GetAbility().GetPower())
            {
                enemyBugList.Add(tempCellBug);
            }
        }

        if (enemyBugList.Count != 0)
        {
            CellBug enemy = enemyBugList[0] as CellBug;
            for (int m = 1; m < enemyBugList.Count; m++)
            {
                CellBug tempEnemy = enemyBugList[m] as CellBug;
                if (Vector3.Magnitude(cellBug.transform.position - tempEnemy.transform.position)
                    < Vector3.Magnitude(cellBug.transform.position - enemy.transform.position))
                    enemy = tempEnemy;
            }
            return enemy;
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
            CellBug mate = mateBugList[0] as CellBug;
            for (int m = 1; m < mateBugList.Count; m++)
            {
                CellBug tempMate = mateBugList[m] as CellBug;
                if (Vector3.Magnitude(cellBug.transform.position - tempMate.transform.position)
                    < Vector3.Magnitude(cellBug.transform.position - mate.transform.position))
                    mate = tempMate;
            }
            return mate;
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
                nowCellBug.SetIsAI(true);
                nowCellBug = bug;
                bug.SetIsAI(false);
                break;
            }
            if (i == cellBugAllList.Count - 1) i = -1;
        }
        DnaVision();
    }

    public void AddCellBugAll(CellBug cellBug)
    {
        cellBugAllList.Add(cellBug);
        GeneLiveVision(cellBug);
        GroupNumChangeVision();
    }

    public void DeleteCellBugAll(CellBug cellBug)
    {
        if (cellBug == nowCellBug) nowCellBug = null;
        GeneLiveVision(cellBug);
        cellBugAllList.Remove(cellBug);
        if (GroupNumChangeVision() == 0) Application.LoadLevel("03");
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

    private int GroupNumChangeVision(Const.CellBugGroup group = Const.CellBugGroup.GodChildEnum)
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

        groupNum.text = "GodChild number:" + numGodChild;
        otherGroupLabelArray[0].text = "Orc number:" + numOrc;
        otherGroupLabelArray[1].text = "Human number:" + numHuman;
        otherGroupLabelArray[2].text = "Eidolon number:" + numEidolon;

        return numGodChild;
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

    private void GeneLiveVision(CellBug cellBug)
    {
        if (nowCellBug == null 
            || cellBug.GetAbility().GetGroup() != nowCellBug.GetAbility().GetGroup()) 
            return;

        int groupNum = 0;
        int[] gene = new int[Const.DnaLineLength];
        for (int i = 0; i < cellBugAllList.Count; i++)
        {
            CellBug bug = cellBugAllList[i] as CellBug;
            if (bug.GetAbility().GetGroup() == nowCellBug.GetAbility().GetGroup())
            {
                groupNum++;
                Dna tempDna = bug.GetAbility().GetDna();

                for (int index = 0; index < gene.Length; index++)
                {
                    gene[index] += (tempDna.GetDnaLine(Const.DnaLineEnum.OneEnum)[index] + tempDna.GetDnaLine(Const.DnaLineEnum.TwoEnum)[index]);
                }
            }
        }

        for (int m = 0; m < geneLabelArray.Length; m++ )
        {
            geneLabelArray[m].text = Const.DnaName[m] + ":" + gene[m] / (2.0 * groupNum) * 100 + "%";
        }
    }
}
