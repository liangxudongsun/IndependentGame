using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

    protected int power;
    protected GameControl gameControl;
    protected Const.FoodEnum foodType;
    private bool eated = false;

    public FoodCreater foodCreater;
    public int GetPower()
    {
        return power;
    }

    public Const.FoodEnum GetFoodType()
    {
        return foodType;
    }

    public bool GetCanEat()
    {
        return !eated;
    }

    public void Eated()
    {
        eated = true;
        
        gameControl.DeleteFood(this);
        Destroy(this.gameObject);
    }
}
