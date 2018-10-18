using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPadButton : MonoBehaviour {
	private InputManager inputMG;

	void Awake(){
		inputMG = InputManager.Instance;
	}
	public void GoUp(){
		inputMG.GoUp ();
	}
	public void GoDown(){
		inputMG.GoDown ();
	}
	public void GoLeft(){
		inputMG.GoLeft ();
	}
	public void GoRight(){
		inputMG.GoRight ();
	}
}
