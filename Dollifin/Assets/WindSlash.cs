using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : MonoBehaviour {
	public float moveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Time.deltaTime*moveSpeed,0, 0);
	}

	void OnCollisionEnter2D(Collision2D col){
		Destroy(gameObject);
	}
}
