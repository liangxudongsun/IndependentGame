using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

    protected int power;
    protected GameControl gameControl;
    protected Const.FoodEnum foodType;

    public int GetPower()
    {
        return power;
    }

    public Const.FoodEnum GetFoodType()
    {
        return foodType;
    }

    public void Eated()
    {
        gameControl.DeleteFood(this);
        Destroy(this.gameObject, 0.1f);
    }
}
