using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveText : MonoBehaviour {

	void Start(){
		gameObject.SetActive(false);
	}
	public void Show(){
		gameObject.SetActive(true);
	}
	public void Hide(float time){
		Invoke("Deactivate",time);
	}
	private void Deactivate(){
		gameObject.SetActive(false);
	}

	public bool GetActive(){
		return gameObject.activeInHierarchy;
	}
}
