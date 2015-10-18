using UnityEngine;
using System.Collections;

public class Meat : Food {

    private int poison;

    void Awake()
    {
        gameControl = (GameObject.Find("gameControl") as GameObject).GetComponent<GameControl>();
    }

	// Use this for initialization
	void Start () {
        gameControl.AddFood(this);
        foodType = Const.FoodEnum.MeatEnum;
        power = Const.meatPower;
        poison = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPoison(int poison)
    {
        this.poison = poison;
    }

    public int GetPoison()
    {
        return poison;
    }
}
