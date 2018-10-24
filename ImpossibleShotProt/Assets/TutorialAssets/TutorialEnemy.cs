using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour {

	private static bool wasHit = false;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "TutorialCollider"){
			TutorialManager.Instance.TutorialEnemyEnter(this);
		}

		if(other.gameObject.tag == "Bullet"){
			wasHit = true;
		}
	}

	public bool WasHit(){
		return wasHit;
	}
}
