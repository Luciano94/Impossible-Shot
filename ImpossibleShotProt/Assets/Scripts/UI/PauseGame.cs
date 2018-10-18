using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	// Use this for initialization
	public void Pause(){
		MenuManager.Instance.PauseGame();
	}
}
