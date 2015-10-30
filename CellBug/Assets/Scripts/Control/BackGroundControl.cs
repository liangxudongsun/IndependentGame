using UnityEngine;
using System.Collections;

public class BackGroundControl : MonoBehaviour {

    //public GameObject[] backGround;

    private CellBug cellBug;
    private int posIndex = 0;
    private int upOrDownIndex = 1;
    private int leftOrRightIndex = 2;

    private Vector3 position;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBackGroundPosition();
	}

    public void SetCellBug(CellBug cellBug)
    {
        this.cellBug = cellBug;
        position = cellBug.transform.position;
        //backGround.transform.position = new Vector3(cellBug.transform.position.x,cellBug.transform.position.y,backGround.transform.position.z);
    }

    private void UpdateBackGroundPosition()
    {
        if (!cellBug) return;
        Vector3 positionChange = cellBug.transform.position - position;
        position = cellBug.transform.position;
        //backGround[0].transform.position -= positionChange; 
    }

    private void upOrDownUpdate()
    {

    }

    private void leftOrRightUpdate()
    {

    }
}
