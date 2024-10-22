﻿using UnityEngine;

public enum TutorialStage{
	Waiting = 0,
	SetUp,
	FirstPhase,
	SetupMarkers,
	WaitForMakers,
	ShowArrows,
	ArrowsGone,
	TextGone,
	MarkersCheck,
	SecondPhase,
	EnemyHit,
	ThirdPhase,
	NoObstacleHit,
	PatternEnd,
	TutorialEnd,
	TotalStages
}

public class TutorialManager : MonoBehaviour {
private static TutorialManager instance;

    public static TutorialManager Instance {
        get {
            instance = FindObjectOfType<TutorialManager>();
            if(instance == null) {
                GameObject go = new GameObject("Tutorial Manager");
                instance = go.AddComponent<TutorialManager>();
            }
            return instance;
        }
    }

	
	private static Vector3 colPos = new Vector3(0f,0f,39f);
	private static Vector3 colCenter = new Vector3(0f,5f,0f);
	private static Vector3 colSize = new Vector3(12f,10f,5f);

	private GameObject tutorialCollider;

	private BulletMovement playerMov;
    private DPadShine DPadShiner;
	private PatternSpawner spawner;

	private TutorialArrows arrows;
	private TutorialCross cross;

	private TutorialMoveText text;

    private TutorialMarker[] markers;

	private TutorialStage stage = 0;

	private bool obstacleHitThisRound;
	private bool noObstaclesHit;

	void Start () {
        playerMov = FindObjectOfType<BulletMovement>();
        DPadShiner = FindObjectOfType<DPadShine>();
		spawner = FindObjectOfType<PatternSpawner>();
		arrows = FindObjectOfType<TutorialArrows>();
		cross = FindObjectOfType<TutorialCross>();
		text = FindObjectOfType<TutorialMoveText>();

		noObstaclesHit = false;
		obstacleHitThisRound = false;
	}

	
	void Update(){
		switch(stage){
			case TutorialStage.Waiting:
			break;
			case TutorialStage.SetUp:
				CreateTutorialCollider();
				playerMov.enabled = false;
				Time.timeScale = 0.1f;
				StageChange();
			break;
			case TutorialStage.FirstPhase:
				FirstPhase();
				StageChange();
			break;
			case TutorialStage.SetupMarkers:
				if(DPadShiner.DoneShining()){
				Time.timeScale = 1.0f;
				setUpMarkers();
				StageChange();
				}
			break;
			case TutorialStage.WaitForMakers:
				if(CheckMarkersDoneMoving()){
					StageChange();
				}
			break;
			case TutorialStage.ShowArrows:
				ShowArrows();
				StageChange();
			break;
			case TutorialStage.ArrowsGone:
				if(CheckArrowsGone()){
					ShowText();
					StageChange();
				}
			break;
			case TutorialStage.TextGone:
				if(CheckTextGone()){
					playerMov.enabled = true;
					StageChange();
				}
			break;
			case TutorialStage.MarkersCheck:
				if(AllMarkersTouched()){
					SoundManager.Instance.Tutorial2();
					DestroyMarkers();
					StageChange();
					spawner.Begin();
				}
			break;
			case TutorialStage.SecondPhase:
			break;
			case TutorialStage.EnemyHit:
				if(TutorialEnemyHit()){
					SoundManager.Instance.Tutorial3();
					StageChange();
				}
			break;
			case TutorialStage.ThirdPhase:
			break;
			case TutorialStage.NoObstacleHit:
				if(noObstaclesHit){
					SoundManager.Instance.Tutorial4();
					StageChange();
				}
			break;
			case TutorialStage.PatternEnd:
				MenuManager.Instance.FinalCountdown();
				GameManager.Instance.EndTutorial();
				StageChange();
			break;
			case TutorialStage.TutorialEnd:
				spawner.EndTutorial();
				CleanUp();
			break;
		}
	}

	//Control utilities
	private void Wait(float waitTime, string next){
		Time.timeScale = 0.1f;
		if(next != "Wait"){
			Invoke(next, waitTime);
		} else {
			Debug.LogError("'Wait' function cannot invoke itself");
			Invoke("DoNothing", waitTime);
		}
	}
	private void Continue(){
		Time.timeScale = 1.0f;
		StageChange();
	}

	private void DoNothing(){
		Time.timeScale = 1.0f;
	}

	public void TutorialSelected(){
		GameManager.Instance.PlayTutorial();
		MenuManager.Instance.StartGame();
		StageChange();
	}

	private void StageChange(){
		if(stage < TutorialStage.TotalStages -1){
			stage++;
			
			if(spawner){
				spawner.UpdateStage();
			}else{
				spawner = FindObjectOfType<PatternSpawner>();
				spawner.UpdateStage();
			}
		}
	}

	public TutorialStage GetStage(){
		return stage;
	}

	
	//Set up utilities
	private void CreateTutorialCollider(){
		tutorialCollider = new GameObject("Tutorial Collider");
		tutorialCollider.tag = "TutorialCollider";
		tutorialCollider.layer = 13; //Utility layer
		tutorialCollider.transform.position = colPos;
		tutorialCollider.transform.parent = gameObject.transform;

		var col = tutorialCollider.AddComponent<BoxCollider>();
		col.isTrigger = true;
		col.center = colCenter;
		col.size = colSize;

		var rig = tutorialCollider.AddComponent<Rigidbody>();
		rig.useGravity = false;
		rig.isKinematic = true;

	}

	//tutorial phases
	private void FirstPhase(){
        //DPad tutorial
        DPadShiner.Shine(0.25f);
	}
	
	private void SecondPhase(){
		//Enemy tutorial
		Wait(0.5f,"Continue");
	}

	private void ThirdPhase(){
		//Obsacle tutorial
		Wait(0.5f,"Continue");
	}

	private void CleanUp(){
		Destroy(tutorialCollider);
		Destroy(gameObject);
	}

	//phase utilities
    private void setUpMarkers()
    {
        Vector3[] positionVectors = FindObjectOfType<BulletMovement>().getCorners();
       	markers = new TutorialMarker[positionVectors.Length];
        Object baseObject = Resources.Load("Models/TutorialBulletPositionMarker");
        for (int i = 0; i < markers.Length; i++){
            GameObject go = Instantiate(baseObject) as GameObject;
			markers[i] = go.GetComponent<TutorialMarker>();
            markers[i].gameObject.transform.position = new Vector3(positionVectors[i].x, positionVectors[i].y, positionVectors[i].z + 15);
			markers[i].SetTargetZ(positionVectors[i].z);

        }
		Invoke("MoveMarkers",1.0f);
    }

	private void MoveMarkers(){
		for(int i = 0; i < markers.Length; i++){
			markers[i].Move(0.5f);
		}
	}

	private bool CheckMarkersDoneMoving(){
		for (int i = 0; i < markers.Length; i++){
			if (!markers[i].DoneMoving()){ return false;}
		}
		return true;
	}

	private bool AllMarkersTouched(){
		for(int i = 0; i < markers.Length; i++){
			if (!markers[i].GetPlayerTouched()){ return false;}
		}
		return true;
	}
	private void DestroyMarkers(){
		for (int i = 0; i < markers.Length; i++){
			Destroy (markers[i].gameObject);
		}
	}

	private void ShowArrows(){
		arrows.Show();
		arrows.Hide(1.5f);
	}
	private bool CheckArrowsGone(){  return !arrows.GetActive();}

	private void ShowText(){
		text.Show();
		text.Hide(1.5f);
	}
	private bool CheckTextGone(){return !text.GetActive();}

	public void TutorialEnemyEnter(){
		SecondPhase();
	}
	private bool TutorialEnemyHit(){
		return TutorialEnemy.WasHit();
	}

	public void TutorialObstacleEnter(){
		ThirdPhase();
	}
	public void PlayerHitTutorialObstacle(){
		obstacleHitThisRound = true;
		cross.Show();
		cross.Hide(0.5f);
		Wait(0.5f,"DoNothing");
	}

	public void UpdateObstaclesHit(){
		noObstaclesHit = !obstacleHitThisRound;
		obstacleHitThisRound = false;
	}
}
