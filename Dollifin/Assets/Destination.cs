using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){

		if(col.GetComponent<LittleDandy>()){
			//If the dandelion is still alive, player wins
			LittleDandy littleDandy = col.GetComponent<LittleDandy>();

			if(littleDandy.GetIsAlive()){
                littleDandy.VictoryRotate(transform.position);

                FindObjectOfType<GameController>().WinBehavior();
			}
		}
	}
}
