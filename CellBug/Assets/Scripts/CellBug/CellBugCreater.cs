using System;
using UnityEngine;
using System.Collections;

public class CellBugCreater : MonoBehaviour {

    public CellBug cellBug;
    public Const.CellBugGroup group = Const.CellBugGroup.GodChildEnum;

    private int cellBugNum = Const.CellBugCreaterNum;
    private bool isCreate = true;
    private float timeForCreate = 0.0f;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!isCreate) return;
        timeForCreate -= Time.deltaTime;
        if (timeForCreate <= 0)
        {
            CreateCellBug();
            timeForCreate = Const.CellBugCreaterTime;
            cellBugNum--;
            if (cellBugNum <= 0) isCreate = false;
        }        
	}

    private void CreateCellBug()
    {
        int seed = (int)DateTime.Now.Ticks + 100 * (int)group;
        System.Random random = new System.Random(seed);
        int dis = random.Next(1, 5);
        float z = cellBug.transform.position.z;
        Vector3 position = transform.position + new Vector3(dis,dis,z);
        CellBug bug = Instantiate(cellBug,position,Quaternion.identity) as CellBug;
        bug.GetAbility().SetGroup(group);
    }
}
