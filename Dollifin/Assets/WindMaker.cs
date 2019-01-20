using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMaker : MonoBehaviour {
	public GameObject windSlashPref;
	private Camera cam;
	Transform targetTransform ;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		targetTransform = FindObjectOfType<LittleDandy>().transform;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			print("now release windslash");
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			Vector2 spawnPoint =cam.ScreenToWorldPoint( new Vector2(x, y));
			GameObject slash = Instantiate(windSlashPref, spawnPoint, Quaternion.identity);
			Vector2 dir = new Vector2(targetTransform.position.x - spawnPoint.x,
							targetTransform.position.y - spawnPoint.y);

			Vector3 vectorToTarget = targetTransform.position - slash.transform.position;
 			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
 			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			slash.transform.rotation = q;
			//targetTransform.GetComponent<Rigidbody2D>().gravityScale = 0;
			targetTransform.GetComponent<Rigidbody2D>().AddForce(dir);
		}
	}
}
