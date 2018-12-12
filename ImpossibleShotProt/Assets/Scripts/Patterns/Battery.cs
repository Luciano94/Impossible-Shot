using UnityEngine;

public class Battery : MonoBehaviour {
	[SerializeField]private GameObject[] battery;

	public GameObject[] GetBattery(){
		return battery;
	}
}
