using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	[SerializeField] int points = 100;
	private GameManager gM;


	void Awake () {
		gM = GameManager.Instance;
	}

	private void OnTriggerExit(Collider other)
	{
		gM.EnemyDeath(points);
	}
}
