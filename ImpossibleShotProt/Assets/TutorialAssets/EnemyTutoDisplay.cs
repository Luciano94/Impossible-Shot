using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutoDisplay : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		MenuManager.Instance.ShowEnemyTuto();
		Destroy(gameObject);
	}
}
