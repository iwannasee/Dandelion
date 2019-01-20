using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public GameObject levelPanel;
    public GameObject animatingDandelions;
    public GameObject animationCanvas;
    // Use this for initialization
    void Start () {
        if(SceneManager.GetActiveScene().buildIndex == 0) { return; }

		int playableLevels = PlayerProgress.playerData.totalNumberOfPlayableLevels;
		int totalAvailableLevels = levelPanel.transform.childCount;
        print("playableLevels " + playableLevels + " in " + totalAvailableLevels);
        for (int i = 0; i< totalAvailableLevels; i++){
			PlayLevelButton playLevelBtn = levelPanel.transform.GetChild(i).GetComponent<PlayLevelButton>();
			if (i < playableLevels){
				//PlayerProgress.playerData.scoreList[i]
				playLevelBtn.SetLock(false);
				if(i >= PlayerProgress.playerData.scoreList.Count){continue;}
				float score = PlayerProgress.playerData.scoreList[i];
				print("score of level " + i + " is " + score);
				playLevelBtn.SetRank(score);
			}else{
				playLevelBtn.SetLock(true);

			}
		}
	}

	public void LoadLevel(string sceneName){
		SceneManager.LoadScene(sceneName);
	}

	public static int GetIndexOfFirstPLayScene(){
		int indexOfFirstLevel = SceneManager.GetSceneByName("Level 1").buildIndex;
		print("indexOfFirstLevel " + indexOfFirstLevel);
		return indexOfFirstLevel;
	}

    public void LoadLevelSelect()
    {
        animatingDandelions.GetComponent<Animator>().SetTrigger("windblowTrigger");
        animationCanvas.GetComponent<Animator>().SetBool("StartPressed", true);
        Invoke("LevelSelectLoad", 1.30f);
    }
    
    private void LevelSelectLoad()
    {
        SceneManager.LoadScene("02 LevelSelect");
    }
}
