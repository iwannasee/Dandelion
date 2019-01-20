using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PlayerProgress : MonoBehaviour {
				
	public static GameData playerData = new GameData();
	//TODO remove this bool after
	private static bool playerDataLoaded = false; 
	void Start(){
		 
		if(!playerDataLoaded){
			playerData = SaveLoadSystem.LoadGame();
			print(playerData.playerName);
			playerDataLoaded  = true;
			//DontDestroyOnLoad(gameObject);
		}


	}


}

