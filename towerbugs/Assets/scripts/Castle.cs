using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.collider.gameObject;
        if (collision.collider.gameObject.name== "enemy")
        {
            gameObject.GetComponent<Enemy>().Dead();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.collider.gameObject;
        if (collision.collider.gameObject.name == "enemy")
        {
            gameObject.GetComponent<Enemy>().Dead();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.collider.gameObject;
        if (collision.collider.gameObject.name == "enemy")
        {
            gameObject.GetComponent<Enemy>().Dead();
        }
    }
}
