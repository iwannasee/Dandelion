using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLevelButton : MonoBehaviour {
	public Sprite goldRankImg;
	public Sprite unRankImg;

	// Use this for initialization
	void Start () {
		//Firstly make disable-effect to the rank icon
		for(int i = 0; i<transform.childCount; i++){
			if(transform.GetChild(i).CompareTag("Locked Stage Icon")){continue;}

			Image rankImage = transform.GetChild(i).GetComponent<Image>();
			rankImage.sprite = unRankImg;
			rankImage.color = new Color(1,1,1,0.3f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetRank(float score){
		Image rankStar1 = transform.GetChild(0).GetComponent<Image>();
		Image rankStar2 = transform.GetChild(1).GetComponent<Image>();
		Image rankStar3 = transform.GetChild(2).GetComponent<Image>();

		if(score > 0 && score <45f){
			rankStar1.sprite = goldRankImg;
			rankStar1.color = new Color(1,1,1,1);
		}else if(score >= 45f && score < 98f){
			rankStar1.sprite = goldRankImg;
			rankStar1.color = new Color(1,1,1,1);
			rankStar2.sprite = goldRankImg;
			rankStar2.color = new Color(1,1,1,1);
		} else if(score >=98f){
			rankStar1.sprite = goldRankImg;
			rankStar1.color = new Color(1,1,1,1);
			rankStar2.sprite = goldRankImg;
			rankStar2.color = new Color(1,1,1,1);
			rankStar3.sprite = goldRankImg;
			rankStar3.color = new Color(1,1,1,1);
		}
	}

	public void SetLock(bool setKey){
		transform.GetChild(3).gameObject.SetActive(setKey);

		if(setKey == true){
            print("this "+ gameObject.name+  " is locked");
			GetComponent<Button>().interactable = false;
		}else{
			GetComponent<Button>().interactable = true;
		}
	}


}
