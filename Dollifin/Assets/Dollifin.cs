using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollifin : MonoBehaviour {
	private Rigidbody2D rg2D;

	// Use this for initialization
	void Start () {
		rg2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			rg2D.AddForce(new Vector2(0,100));
		}
	}

}
