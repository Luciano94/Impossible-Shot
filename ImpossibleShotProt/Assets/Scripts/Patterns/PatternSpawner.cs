using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternSpawner : MonoBehaviour {

	[SerializeField]private  Battery[] setOfBattery;
	[SerializeField]private Text changeTxT;
	[SerializeField]private float timePerBattery;
	[SerializeField]private float timePerPattern;
	[SerializeField]private float timePerObstacle;
	[SerializeField]private float timeDown;
	[SerializeField]private float minTime;	
	private int actualBattery=1;
	private GameObject[] battery;
	private int actualPattern=0;
	private GameObject pattern;

	
	/*TUTORIAL ATTRIBUTES */
	private GameObject patternTutorial;
	private int actualPatternTutorial=0;
	private GameObject[] tutobattery;
	private TutorialStage stage;

	/*Events Atributes */
	[SerializeField] private Battery batteryOfBulletTime;
	[SerializeField] private Battery batteryOfEnemyStream;
	[SerializeField] private float timePerObsInEvent = 0.4f;
	private float actualTimePerObs;
	private Queue<Product> arrayOfProducts;
	private bool bulletTime = false;

#region Tutorial	/*TUTORIAL METHODS */
	private void Start() {
		arrayOfProducts = new Queue<Product>();
		tutobattery = setOfBattery[0].GetBattery();
	}

	private void EnemyTutorial(){//le cambié el nombre
		ChargePatternsTutorial();
		Invoke("SpawnTutorial", timePerBattery);
	}

	private void ChargePatternsTutorial(){
		if(actualPatternTutorial > tutobattery.Length-1){
			actualPatternTutorial = tutobattery.Length-1;
		}
		patternTutorial=tutobattery[actualPatternTutorial];
	}

	private void SpawnTutorial(){
		changeTxT.enabled = false;
		GenerateObstacleTuto();
        if(patternTutorial.GetComponent<Pattern>().Count() > 0){
            Invoke("SpawnTutorial", timePerObstacle);
		}else{ 
			changePhase();
		}
	}

	private void changePhase(){
		switch (stage){
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
#endregion	/*End Tutorial Methods */
#region Events	/*Events Methods */
	public void InitEvent(){
		CancelInvoke("SpawnObstacle");
		actualTimePerObs = timePerObstacle;
		timePerObstacle = timePerObsInEvent;
	}

	private void SpawnObstacleEvent(){
		GenerateObstacle();
        if(pattern.GetComponent<Pattern>().Count() > 0){
            Invoke("SpawnObstacleEvent", timePerObstacle);
		}else{ 
			EndEvent();
		}
	}

	private void EndEvent(){
		if(bulletTime){ 
			bulletTime = false;
		}	
		EventsManager.Instance.DesactiveEvent();
		actualPattern = 0;
		timePerObstacle = actualTimePerObs;
		ChargeBattery();
		RandomizeBattery();
		ChargePatterns();
		Invoke("SpawnObstacle", timePerBattery);
		arrayOfProducts.Clear();
	}

	public void BeginEnemyEvent(){
		battery = batteryOfEnemyStream.GetBattery();
		actualPattern = 0;
		RandomizeBattery();
		ChargePatterns();
		SpawnObstacleEvent();
	}

	public void BeginBulletEvent(){
		bulletTime = true;
		battery = batteryOfBulletTime.GetBattery();
		actualPattern = 0;
		RandomizeBattery();
		ChargePatterns();
		SpawnObstacleEvent();
	}
#endregion	/*End events Methods */
#region Spawn
	public void PauseSpawn(){
		CancelInvoke("SpawnObstacleEvent");
		CancelInvoke("SpawnObstacle");
	}

	public void Death(){
		if(EventsManager.Instance.ActiveEvent){
			DesactiveEvent();
			CancelInvoke("SpawnObstacleEvent");
		}
		CancelInvoke("SpawnObstacle");
	}

	private void DesactiveEvent(){
		foreach (Product item in arrayOfProducts){
			item.ReturnToFactory();
		}
		EndEvent();
	}

	public void Begin(){
		if(EventsManager.Instance.ActiveEvent){
			Invoke("SpawnObstacleEvent", timePerObstacle);
		}else {
			if(GameManager.Instance.TutorialMode){
				EnemyTutorial();
			}else{
				ChargeBattery();
				RandomizeBattery();
				ChargePatterns();
				Invoke("SpawnObstacle", timePerBattery);
			}
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
		if(bulletTime)
			arrayOfProducts.Enqueue(go.GetComponent<Product>());
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
	}

	private void BatteryChange(){
		if(actualBattery != setOfBattery.Length-1){
			actualBattery++;
		}
		if(actualBattery == 3){
			EventsManager.Instance.ActiveEvents();
		}
		ChargeBattery();
		RandomizeBattery();
		actualPattern = 0;
		ChargePatterns();
		if(timePerObstacle-timeDown > minTime){
			timePerObstacle -= timeDown;
		}else{ 
			timePerObstacle = minTime;
		}
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
		}else{ 
			BatteryChange();
		}
	}

	private void SpawnObstacle(){
		GenerateObstacle();
        if(pattern.GetComponent<Pattern>().Count() > 0){
            Invoke("SpawnObstacle", timePerObstacle);
		}else {
			PatternChange();
		}
	}

}
#endregion