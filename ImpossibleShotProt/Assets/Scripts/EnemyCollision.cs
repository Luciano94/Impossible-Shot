using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Entre wachin");
		GameManager.Instance.Aceleration();
		//gameObject.GetComponent<EnemyScript>().Active = false;
	}
}
