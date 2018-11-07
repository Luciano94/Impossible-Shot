using UnityEngine;

public class EnemyScript : MonoBehaviour {
	
	[SerializeField] int points = 100;

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.tag != "TutorialCollider" )
			GameManager.Instance.EnemyDeath(points);
	}
}
