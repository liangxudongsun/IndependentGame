using UnityEngine;
using System.Collections;

public class Love : MonoBehaviour {

    public float speed = 3;
    private float liveTime = 2.0f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        transform.localScale -= new Vector3(Time.deltaTime / liveTime /2,Time.deltaTime/ liveTime / 2,0);
	}
}
