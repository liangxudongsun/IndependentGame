using UnityEngine;
using System.Collections;

public class FoodCreater : MonoBehaviour {

    public Food[] foodArray;

    private Food nowFood = null;
    private float foodCreaterTime = Const.foodCreaterTime;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (nowFood == null)
        {
            foodCreaterTime -= Time.deltaTime;
            if (foodCreaterTime <= 0 && nowFood == null)
            {
                GameObject obj = Instantiate(foodArray[0].gameObject, new Vector3(transform.position.x,transform.position.y,foodArray[0].gameObject.transform.position.z), Quaternion.identity) as GameObject;
                nowFood = obj.GetComponent<Food>();
                foodCreaterTime = Const.foodCreaterTime;
            }
        }
	}
}
