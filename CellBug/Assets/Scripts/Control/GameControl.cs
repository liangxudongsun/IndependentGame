using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    private Camera camera;
    private ArrayList cellBugNativeList = new ArrayList();//本族谱精灵集合
    private ArrayList cellBugAllList = new ArrayList();   //所有精灵集合
    private ArrayList foodArrayList = new ArrayList();     //食物集合
 
    private CellBug nowCellBug = null;

    void Awake()
    {
        camera = Camera.main;
    }

	// Use this for initialization
	void Start () {
        SearchCellBug();
 	}
	
	// Update is called once per frame
	void Update () {

        if (!nowCellBug) return;
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
        for (int i = 0; i < cellBugNativeList.Count;i++)
        {
            CellBug bug = cellBugNativeList[i] as CellBug;
            if (bug)
            {
                nowCellBug = bug; 
                nowCellBug.SetCamera(camera);
                return;
            } 
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

    public void AddCellBugNative(CellBug cellBug)
    {
        cellBugNativeList.Add(cellBug);
    }

    public void DeleteCellBugNative(CellBug cellBug)
    {
        cellBugNativeList.Remove(cellBug);
    }

    public void AddCellBugAll(CellBug cellBug)
    {
        cellBugAllList.Add(cellBug);
    }

    public void DeleteCellBugAll(CellBug cellBug)
    {
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
