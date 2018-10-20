using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {
	[SerializeField] GameObject[] battery;

	public GameObject[] GetBattery(){
		return battery;
	}
}
