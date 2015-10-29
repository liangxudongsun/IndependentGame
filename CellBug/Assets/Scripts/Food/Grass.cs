using UnityEngine;
using System.Collections;

public class Grass : Food {

    void Awake()
    {
        gameControl = (GameObject.Find("gameControl") as GameObject).GetComponent<GameControl>();
    }

	// Use this for initialization
	void Start () {
        gameControl.AddFood(this);
        foodType = Const.FoodEnum.GrassEnum;
        power = Const.PlantPower;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
