using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
private static TutorialManager instance;

    public static TutorialManager Instance {
        get {
            instance = FindObjectOfType<TutorialManager>();
            if(instance == null) {
                GameObject go = new GameObject("TutorialManager");
                instance = go.AddComponent<TutorialManager>();
            }
            return instance;
        }
    }
	private static Vector3 colPos = new Vector3(0f,0f,50f);
	private static Vector3 colCenter = new Vector3(0f,5f,0f);
	private static Vector3 colSize = new Vector3(12f,10f,1f);
	private static float normalWaitTime = 5f;

	private GameObject tutorialCollider;
	private TutorialEnemy tutorialEnemy;
	private bool[,] positionChecks;

	private BulletMovement playerMov;
    private DPadShine DPadShiner;

	private bool firstTutorialEnemy = true;
	private bool firstPhaseEnded = false;



	void Awake(){
		CreateTutorialCollider();

		positionChecks = new bool[3,3];
		for(int i = 0; i < 3; i++){
			for (int j = 0; j < 3; j++){
				positionChecks[i,j] = false;
			}
		}
	}

	void Start () {
        playerMov = FindObjectOfType<BulletMovement>();
        DPadShiner = FindObjectOfType<DPadShine>();

        FirstPhase();
	}

	void LateUpdate(){
		var loc = playerMov.getPositionInts();
		if(!firstPhaseEnded){
			if(positionChecks[loc.x +1 ,loc.y +1] == false){ //from -1->1 scale to 0->2 scale
				positionChecks[loc.x +1,loc.y+1] = true;
			}
			CheckAllPositions();
		}
	}

	//Control utilities
	private void Wait(float waitTime, string next){
		Time.timeScale = 0;
		if(next != "Wait"){
			Invoke(next, waitTime);
		} else {
			Debug.LogError("'Wait' function cannot invoke itself");
			Invoke("DoNothing", waitTime);
		}
	}

	private void Continue(){
		Time.timeScale = 1;
	}

	private void DoNothing(){}

	public void TutorialSelected(){
		GameManager.Instance.PlayTutorial();
		MenuManager.Instance.StartGame();
	}

	
	//Set up utilities
	private void CreateTutorialCollider(){
		tutorialCollider = new GameObject("TutorialCollider");
		tutorialCollider.tag = "TutorialCollider";
		tutorialCollider.layer = 13; //Utility layer
		tutorialCollider.transform.position.Set(colPos.x,colPos.y,colPos.z);
		tutorialCollider.transform.parent = gameObject.transform;

		var col = tutorialCollider.AddComponent<BoxCollider>();
		col.center.Set(colCenter.x,colCenter.y,colCenter.z);
		col.size.Set(colSize.x,colSize.y,colSize.z);

	}

	//tutorial phase control
	private void FirstPhase(){
        //DPad tutorial
        DPadShiner.Shine(0.25f);
		MenuManager.Instance.ShowDPadTuto();
	}
	
	private void SecondPhase(){
		//Enemy tutorial
		MenuManager.Instance.ShowEnemyTuto();
		if(firstTutorialEnemy){
			firstTutorialEnemy = false;
			Wait(normalWaitTime, "Continue");
		} else{
			if (tutorialEnemy.WasHit()){
				ThirdPhase();
			}
		}
	}

	private void ThirdPhase(){
		//Obsacle tutorial
		TutorialEnd();

	}

	private void TutorialEnd(){
		Destroy(tutorialCollider);
		Destroy(gameObject);
	}

	//phase utilities
	public void TutorialEnemyEnter(TutorialEnemy enemy){
		tutorialEnemy = enemy;
		SecondPhase();
	}
	private void CheckAllPositions(){
		for(int i = 0; i < 3; i++){
			for (int j = 0; j < 3; j++){
				if (positionChecks[i,j] == false){
					return;
				}
			}
		}
		GameManager.Instance.TutorialSpawnBegin();
		MenuManager.Instance.DonePadTuto();
		firstPhaseEnded = true;
	}
}
