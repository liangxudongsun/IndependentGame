using System;
using UnityEngine;
using System.Collections;

public class CellBugCreater : MonoBehaviour {

    public CellBug cellBug;
    public GameObject cellBugArray;
    public Const.CellBugGroup group = Const.CellBugGroup.MineEnum;
    public int cellBugNum = 0;
    public float createTimePur = 5.0f;

    private bool isCreate = true;
    private float timeForCreate;
	// Use this for initialization
	void Start () {
        timeForCreate = createTimePur;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isCreate) return;
        timeForCreate -= Time.deltaTime;
        if (timeForCreate <= 0)
        {
            CreateCellBug();
            timeForCreate = createTimePur;
            cellBugNum--;
            if (cellBugNum <= 0) isCreate = false;
        }        
	}

    private void CreateCellBug()
    {
        int seed = (int)DateTime.Now.Ticks;
        System.Random random = new System.Random(seed + 500 * cellBugNum);
        int dis = random.Next(1, 5);
        Vector3 position = transform.position + new Vector3(dis,dis,-2);
        CellBug bug = Instantiate(cellBug,position,Quaternion.identity) as CellBug;
        bug.GetAbility().cellBugGroup = group;
        bug.transform.parent = cellBugArray.transform;
    }
}
