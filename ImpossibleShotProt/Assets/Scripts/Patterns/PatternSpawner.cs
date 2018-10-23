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

	public void Begin(){
		if(GameManager.Instance.TutorialMode)
			Tutorial();
		else{
			ChargeBattery();
			RandomizeBattery();
		}
		ChargePatterns();
		Invoke("SpawnObstacle", timePerBattery);
	}
	public void Tutorial(){
		actualBattery = 0;
		battery = setOfBattery[actualBattery].GetBattery();
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
		changeTxT.enabled = true;
		changeTxT.text = "BATTERY CHANGE " + actualBattery;
		if(actualBattery != setOfBattery.Length-1)
			actualBattery++;
		ChargeBattery();
		RandomizeBattery();
		actualPattern = 0;
		ChargePatterns();
		if(timePerBattery-timeDown > minTime) timePerBattery -= timeDown;
		else timePerBattery = timeDown;
		Invoke("SpawnObstacle", timePerBattery);
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
		changeTxT.enabled = true;
		changeTxT.text = "PATTERN CHANGE " + actualPattern;
		if(actualPattern != battery.Length-1){
			actualPattern++;
			ChargePatterns();
			if(timePerPattern-timeDown > minTime) timePerPattern -= timeDown;
			else timePerPattern = timeDown;
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
