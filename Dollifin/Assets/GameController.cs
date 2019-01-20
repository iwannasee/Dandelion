using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Text distance;
	public GameObject resultPanel;
    public GameObject guidePanel;
	public GameObject desPref;
	private Transform dandelion;
	Destination[] destinations;
    DandyFellow[] dandyFellows;
	public Button retryBtn;
	public Button continueBtn;
	public Button mainMenuBtn;
    public DestinationSpawner desSpawner;
	Transform destinationTransform;
	public bool isStageEnd = false;

    private bool bIsDestinationAppeared = false;
	// Use this for initialization
	void Start () {
		resultPanel.SetActive(false);
        guidePanel.SetActive(true);
        dandelion = FindObjectOfType<LittleDandy>().transform;
        dandyFellows = FindObjectsOfType<DandyFellow>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bIsDestinationAppeared)
        {
            float distanceToChosen = Vector2.Distance(dandelion.position, destinationTransform.position);
            float twoDigitRounded = Mathf.Round(distanceToChosen * 100f) / 100f;
            distance.text = string.Format("{0:F2}", twoDigitRounded);
        }
		
	}

	public void WinBehavior(){
		resultPanel.transform.GetChild(0).GetComponent<Text>().text = "CLEAR";
		resultPanel.SetActive(true);
		retryBtn.gameObject.SetActive(false);
		continueBtn.gameObject.SetActive(true);
		isStageEnd = true;

		//Get score
		float scoreEarned = dandelion.GetComponent<LittleDandy>().GetRemainingHealth()*100;
		//Calculate the current scene's index
		//TODO Warning, here is assumed that the level 1 build index is 2. If the build index changes, the magic number must be changed as well
		int levelIndexOfThisScene = SceneManager.GetActiveScene().buildIndex - 2;
		print("levelIndexOfThisScene " + levelIndexOfThisScene);

		if((levelIndexOfThisScene == (PlayerProgress.playerData.totalNumberOfPlayableLevels - 1) ) && 
			(PlayerProgress.playerData.totalNumberOfPlayableLevels < 9)){
			PlayerProgress.playerData.totalNumberOfPlayableLevels++;
			PlayerProgress.playerData.scoreList.Add(scoreEarned);
			SaveLoadSystem.SaveGame(PlayerProgress.playerData);
			return;
		}

		if(PlayerProgress.playerData.scoreList[levelIndexOfThisScene] < scoreEarned){
			PlayerProgress.playerData.scoreList[levelIndexOfThisScene] = scoreEarned;
		}

		SaveLoadSystem.SaveGame(PlayerProgress.playerData);
	}

    public void SetUpDestinationForStage()
    {
        desSpawner.SpawnDestination();
        destinationTransform = desSpawner.transform.GetChild(0);
        bIsDestinationAppeared = true;
        /*Destination chosenOne = destinations[Random.Range(0, destinations.Length)];
		chosenOne.SetChosenOne();
		destinationTransform = chosenOne.transform;*/
    }

	public void LoseBehavior(){
		resultPanel.transform.GetChild(0).GetComponent<Text>().text = "FAILED";
		resultPanel.SetActive(true);
		retryBtn.gameObject.SetActive(true);
		continueBtn.gameObject.SetActive(false);
		isStageEnd = true;
	}

	public bool GetIsStageEnd(){
		return isStageEnd;
	}

	public void RetryLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene("02 LevelSelect");
	}

    public bool IsGuidePanelDisplaying()
    {
        return guidePanel.activeInHierarchy;
    }

    public void HideGuidePanel()
    {
        guidePanel.SetActive(false);
    }

    public bool TouchedAllRequiredFellows()
    {
        bool touchedAll = true;
        for(int i = 0; i<dandyFellows.Length; i++)
        {
            if (!dandyFellows[i].GetIsTouched())
            {
                touchedAll = false;
                break;
            }
        }

        return touchedAll;
    }
}
