using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeOption : MonoBehaviour {
	//Este script pertenece a un grupo de 2 mutualmente exclusivos, no funciona para otros casos
	public void ValueChange(){
		GetComponentInParent<InputSelect> ().SetAlternative (!GetComponent<UnityEngine.UI.Toggle>().isOn);
	}
}
