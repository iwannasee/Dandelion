using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSpawner : MonoBehaviour {
    public GameObject DestinationPref;
    public float MaxLeftX;
    public float MaxRightX;
    public float[] YPosToSpawns;
    
    public int spawnedNumber;
    // Use this for initialization
    void Start () {
	}

    public void SpawnDestination()
    {
        Vector2 spawnPos = new Vector2(Random.Range(MaxLeftX, MaxRightX),
                                            YPosToSpawns[Random.Range(0, YPosToSpawns.Length)]);
        GameObject des = Instantiate(DestinationPref, spawnPos, Quaternion.identity) as GameObject;
        des.transform.SetParent(transform);
    }
}
