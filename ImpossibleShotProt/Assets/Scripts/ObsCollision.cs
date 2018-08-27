using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsCollision : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Entre wachin");
		GameManager.Instance.Death();
	}
}
