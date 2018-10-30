using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPattern : MonoBehaviour {
	private static bool noneHit = false;

	private bool[] hitTracker;
	private float[] iDTracker;
	private Pattern pattern;
	private TutorialObstacle[] tutorialObstacles;
	void Start () {
		noneHit = false;
		pattern = GetComponent<Pattern>();
		GameObject[] obstacles = pattern.GetList();
		int total = 0;
		for(int i = 0; i < obstacles.Length; i++){
			if(obstacles[i].GetComponent<TutorialObstacle>()){
				total++;
			}
		}
		tutorialObstacles = new TutorialObstacle[total];
		hitTracker = new bool[total];
		iDTracker = new float[total];
		total = 0;
		for(int i = 0; i < obstacles.Length; i++){
			if(obstacles[i].GetComponent<TutorialObstacle>()){
				tutorialObstacles[total] = obstacles[i].GetComponent<TutorialObstacle>();
				hitTracker[total] = true;
				iDTracker[total] = tutorialObstacles[total].GetInstanceID();
				total++;
			}
		}
	}
	public static bool NoneHit(){
		return noneHit;
	}
	public void Return(GameObject go){
		TutorialObstacle to = go.GetComponent<TutorialObstacle>();
		if(to){
			for(int i = 0; i <tutorialObstacles.Length; i++){
				if(to.GetInstanceID() == iDTracker[i]){
					Debug.Log(to.GetInstanceID() +" "+to.WasHit());
					hitTracker[i] = to.WasHit();
					to.ResetHit();
				}
			}

			bool nohit = true;
			for(int i = 0; i < hitTracker.Length;i++){
				if(hitTracker[i] == true){
					nohit = false;
				}
			}
			noneHit = nohit;
		}
	}
}
