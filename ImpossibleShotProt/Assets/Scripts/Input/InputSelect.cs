using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSelect : MonoBehaviour {

	private bool AlternativeSelected = false; //false = swipe, true = D-pad

	public void SetAlternative(bool alt){
		AlternativeSelected = alt;
	}
	public bool GetAlternative (){
		return AlternativeSelected;
	}
}
