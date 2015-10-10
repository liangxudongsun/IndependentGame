using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int[,] enemyAbility = new int[,]
    {
        {1/*life*/,5/*speed*/,3/*coin*/},
        {1/*life*/,1/*speed*/,3/*coin*/},
        {1/*life*/,3/*speed*/,3/*coin*/},
        {1/*life*/,5/*speed*/,3/*coin*/},
        {1/*life*/,5/*speed*/,3/*coin*/},
        {1/*life*/,2/*speed*/,3/*coin*/},
        {1/*life*/,2/*speed*/,3/*coin*/},
    };

    private int life;
    private int speed;
    private int coin;

    private Control control;
    private int pathIndex;
	// Use this for initialization
	void Start () {
        control = GameObject.Find("control").GetComponent<Control>();
        pathIndex = 0;

        //life = enemyAbility[0,0];
       // speed = enemyAbility[0,1];
       // coin = enemyAbility[0,2];
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = control.path[pathIndex].position - transform.position;
        float dis = dir.magnitude;

        if (dis <= 0.1f && pathIndex < 11/*control.path.Length*/)
        {
            pathIndex++;
        }
        else
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
	}

    public void SetEnemy(int enemyType)
    {
        life = enemyAbility[enemyType, 0];
        speed = enemyAbility[enemyType, 1];
        coin = enemyAbility[enemyType, 2];
    }

    public void Dead()
    {
        Destroy(this);
    }
}
