using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		GameManager.Instance.Aceleration();
		EnemyFactory.Instance.Return(gameObject);
	}
}
