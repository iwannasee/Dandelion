using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
	public float destroyAfter = 2f;
	// Use this for initialization
	void Start () {
		Invoke("DestroyEffect", destroyAfter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DestroyEffect(){
		Destroy(gameObject);
	}
}
