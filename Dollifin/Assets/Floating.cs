using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
    public float speed;
    public bool canFloatAlong = false;
    public float maxTimeToReverse = 2f;
    private float timeToReverse;
	// Use this for initialization
	void Start () {
        timeToReverse = maxTimeToReverse;

    }
	
	// Update is called once per frame
	void Update () {
        if (canFloatAlong)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            timeToReverse -= Time.deltaTime;
            return;
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timeToReverse -= Time.deltaTime;
        if(timeToReverse <= 0)
        {
            speed *= -1;
            timeToReverse = maxTimeToReverse;
        }
	}
}
