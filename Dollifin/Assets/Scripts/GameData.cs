using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
	public string playerName;
	public int totalNumberOfPlayableLevels;
	public List<float> scoreList;
	public List<string> availableMaps;
	public List<string> completedMaps;
}
