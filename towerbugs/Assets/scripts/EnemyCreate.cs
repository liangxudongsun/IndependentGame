using UnityEngine;
using System.Collections;

public class EnemyCreate : MonoBehaviour {

    public float purEnemyTime = 1.0f;
    public float purGroupTime = 5.0f;
    public int[,] enemyArray = new int[10,10]{
        { 1,2,2,3,4,1,2,2,3,3},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
    };

    public GameObject enemy;

    private float enemyTime = 1f;
    private float groupTime = 5f;

    private int enemyIndex = 0;
    private int groupIndex = 0;

	// Use this for initialization
	void Start () {

	}	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(enemyTime) <= 0.01f
            && groupIndex < 10 && enemyIndex < 10)
        {
            GameObject enemyNow = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
            enemyNow.GetComponent<Enemy>().SetEnemy(enemyArray[groupIndex, enemyIndex]);
            enemyIndex++;

            if (enemyIndex == 9)
            {
                groupIndex++;
                enemyIndex = 0;
                enemyTime = groupTime;
            }
            else
            {
                enemyTime = purEnemyTime;
            }
        }
        else
        {
            enemyTime -= Time.deltaTime;
        }
	}
}
