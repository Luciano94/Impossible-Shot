using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPattern : MonoBehaviour {
	private static bool noneHit = false;
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

		 total = 0;
		for(int i = 0; i < obstacles.Length; i++){
			TutorialObstacle component = obstacles[i].GetComponent<TutorialObstacle>();
			if(component){
				tutorialObstacles[total] = component;
				total++;
			}
		}
	}
	
	public void OnPatternEnd(){
		if(pattern.Count() == pattern.TamLista()){
			bool noHit = true;
			for(int i = 0; i < tutorialObstacles.Length; i++){
				if(tutorialObstacles[i].WasHit()){
					resetHits();
					noHit = false;
				}
			}
			noneHit = noHit;
		}
	}

	public static bool NoneHit(){
		return noneHit;
	}

	public void resetHits(){
		for(int i = 0; i < tutorialObstacles.Length; i++){
			tutorialObstacles[i].ResetHit();
		}
	}
}
