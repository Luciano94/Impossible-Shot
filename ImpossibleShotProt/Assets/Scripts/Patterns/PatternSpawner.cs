using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternSpawner : MonoBehaviour {

	[SerializeField]private  Battery[] setOfBattery;
	private int actualBattery=1;
	private GameObject[] battery;
	private int actualPattern=0;
	private GameObject pattern;
	[SerializeField]private Text changeTxT;
	[SerializeField]private float timePerBattery;
	[SerializeField]private float timePerPattern;
	[SerializeField]private float timePerObstacle;
	[SerializeField]private float timeDown;
	[SerializeField]private float minTime;
	
	/*TUTORIAL ATTRIBUTES */
	private GameObject patternTutorial;
	private int actualPatternTutorial=0;
	private GameObject[] tutobattery;
	private TutorialStage stage;

	/*TUTORIAL METHODS */
	private void Start() {
		tutobattery = setOfBattery[0].GetBattery();
	}

	private void EnemyTutorial(){//le cambié el nombre
		ChargePatternsTutorial();
		Invoke("SpawnTutorial", timePerBattery);
	}

	private void ChargePatternsTutorial(){
		if(actualPatternTutorial > tutobattery.Length-1)
			actualPatternTutorial = tutobattery.Length-1;
		patternTutorial=tutobattery[actualPatternTutorial];
	}

	private void SpawnTutorial(){
		changeTxT.enabled = false;
		GenerateObstacleTuto();
        if(patternTutorial.GetComponent<Pattern>().Count() > 0)
            Invoke("SpawnTutorial", timePerObstacle);
        else changePhase();
	}

	private void changePhase(){
		switch (stage)
		{
			case TutorialStage.EnemyHit:
				ChargePatternsTutorial();
				Invoke("SpawnTutorial", timePerBattery);
			break;
			case TutorialStage.NoObstacleHit:
				ChargePatternsTutorial();
				Invoke("SpawnTutorial", timePerBattery);	
			break;
			case TutorialStage.ThirdPhase:
				actualPatternTutorial++;
				stage = TutorialManager.Instance.GetStage();
				EnemyTutorial();
			break;
		}
	}

	private void GenerateObstacleTuto(){
		GameObject go = patternTutorial.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
	}

	public void EndTutorial(){
		CancelInvoke("SpawnTutorial");
		ChargeBattery();
		RandomizeBattery();
		ChargePatterns();
		Invoke("SpawnObstacle", timePerBattery);
	}

	public void UpdateStage(){
		stage = TutorialManager.Instance.GetStage();
	}
	/*END TUTORIAL FUNCTIONS */

	public void Begin(){
		if(GameManager.Instance.TutorialMode)
			EnemyTutorial();
		else{
			ChargeBattery();
			RandomizeBattery();
			ChargePatterns();
			Invoke("SpawnObstacle", timePerBattery);
		}
	}

	public void ObstacleTutorial(){ //cree esta funcion
		Debug.LogError("Lucho, checkea este codigo");
		if(actualBattery == 0 && actualPattern == 0){
			PatternChange();
		}
	}
	private void ChargeBattery(){
		battery = setOfBattery[actualBattery].GetBattery();
	}
	private void ChargePatterns(){
		pattern = battery[actualPattern];
	}

	private void GenerateObstacle(){
		GameObject go = pattern.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
	}

	private void BatteryChange(){
		if(actualBattery != setOfBattery.Length-1)
			actualBattery++;
		ChargeBattery();
		RandomizeBattery();
		actualPattern = 0;
		ChargePatterns();
		if(timePerObstacle-timeDown > minTime) 
			timePerObstacle -= timeDown;
		else 
			timePerObstacle = minTime;
		Invoke("SpawnObstacle", timePerPattern);
	}

	private void RandomizeBattery(){
        int i = battery.Length;
        while(i >1){
            i--;
            int j = Random.Range(0, i);
            GameObject go = battery[j];
            battery[j]= battery[i];
            battery[i]=go;
        }
    }

	private void PatternChange(){
		if(actualPattern != battery.Length-1){
			actualPattern++;
			ChargePatterns();

			Invoke("SpawnObstacle", timePerPattern);
		} else BatteryChange();
	}

	private void SpawnObstacle(){
		changeTxT.enabled = false;
		GenerateObstacle();
        if(pattern.GetComponent<Pattern>().Count() > 0)
            Invoke("SpawnObstacle", timePerObstacle);
        else PatternChange();
	}

}
