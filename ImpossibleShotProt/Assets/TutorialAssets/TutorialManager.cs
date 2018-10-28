using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private enum TutorialStage{
		SetUp = 0,
		FirstPhase,
		SetupMarkers,
		WaitForMakers,
		ShowArrows,
		ArrowsGone,
		MarkersCheck,
		SecondPhase,
		EnemyHit,
		ThirdPhase,
		NoObstacleHit,
		PatternEnd,
		TutorialEnd
	}
	private static Vector3 colPos = new Vector3(0f,0f,49f);
	private static Vector3 colCenter = new Vector3(0f,5f,0f);
	private static Vector3 colSize = new Vector3(12f,10f,5f);

	private GameObject tutorialCollider;

	private BulletMovement playerMov;
    private DPadShine DPadShiner;
	private PatternSpawner spawner;

	private TutorialArrows arrows;

    private TutorialMarker[] markers;

	private TutorialStage stage = 0;

	void Awake(){
	}

	void Start () {
        playerMov = FindObjectOfType<BulletMovement>();
        DPadShiner = FindObjectOfType<DPadShine>();
		spawner = FindObjectOfType<PatternSpawner>();
		arrows = FindObjectOfType<TutorialArrows>();
	}

	
	void Update(){
		switch(stage){
			case TutorialStage.SetUp:
				StageDebug();
				CreateTutorialCollider();
				playerMov.enabled = false;
				stage++;
				StageDebug();
			break;
			case TutorialStage.FirstPhase:
				FirstPhase();
				stage++;
				StageDebug();
			break;
			case TutorialStage.SetupMarkers:
				if(DPadShiner.DoneShining()){
				setUpMarkers();
				stage++;
				StageDebug();
				}
			break;
			case TutorialStage.WaitForMakers:
				if(CheckMarkersDoneMoving()){stage++; StageDebug();}
			break;
			case TutorialStage.ShowArrows:
				ShowArrows();
				stage++;
				StageDebug();
			break;
			case TutorialStage.ArrowsGone:
				if(CheckArrowsGone()){
					playerMov.enabled = true;
					stage++;
					StageDebug();
				}
			break;
			case TutorialStage.MarkersCheck:
				if(AllMarkersTouched()){
					DestroyMarkers();
					spawner.Begin();
					stage++;
					StageDebug();
				}
			break;
			case TutorialStage.SecondPhase:
				//wait for TutorialEnemyEnter()
			break;
			case TutorialStage.EnemyHit:
				if(TutorialEnemyHit()){
					stage++;
					StageDebug();
				}
			break;
			case TutorialStage.ThirdPhase:
				spawner.ObstacleTutorial();
				stage++;
				StageDebug();
			break;
			case TutorialStage.NoObstacleHit:
				if(true/*no obstacle hit*/){
					GameManager.Instance.EndTutorial();
					stage++;
					StageDebug();
				}
			break;
			case TutorialStage.PatternEnd:
				//spawn normal patterns
				stage++;
				StageDebug();
			break;
			case TutorialStage.TutorialEnd:
				CleanUp();
			break;
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

	public void TutorialSelected(){
		GameManager.Instance.PlayTutorial();
		MenuManager.Instance.StartGame();
	}

	private void StageDebug(){
		Debug.Log(stage);
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
		MenuManager.Instance.ShowDPadTuto();
	}
	
	private void SecondPhase(){
		//Enemy tutorial
		MenuManager.Instance.ShowEnemyTuto();
		stage++;
		StageDebug();
	}

	private void ThirdPhase(){
		//Obsacle tutorial
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
        for (int i = 0; i < markers.Length; i++)
        {
            GameObject go = Instantiate(baseObject) as GameObject;
			markers[i] = go.GetComponent<TutorialMarker>();
            markers[i].gameObject.transform.position = new Vector3(positionVectors[i].x, positionVectors[i].y, positionVectors[i].z + 15);
			markers[i].setTargetZ(positionVectors[i].z);

        }
		Invoke("MoveMarkers",1.0f);
    }

	private void MoveMarkers(){
		for(int i = 0; i < markers.Length; i++){
			markers[i].Move(1);
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
		arrows.Hide(1.0f);
	}
	private bool CheckArrowsGone(){  return !arrows.GetActive();}

	public void TutorialEnemyEnter(){
		SecondPhase();
	}
	private bool TutorialEnemyHit(){
		return TutorialEnemy.WasHit();
	}

	public void TutorialObstacleEnter(){}
	public void PlayerHitTutorialObstacle(){}

}
