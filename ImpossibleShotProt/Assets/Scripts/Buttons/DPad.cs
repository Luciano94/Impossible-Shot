using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPad : MonoBehaviour {
	void Start(){
		if(!InputManager.Instance.IsInputAndroidAlternative()){
			gameObject.SetActive (false);
		}
	}
}
