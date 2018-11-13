using UnityEngine;

public class EnemyScript : MonoBehaviour {
	
	[SerializeField] int points = 100;
	private bool death = false;

	private void OnTriggerEnter(Collider other){
		if (!death && other.gameObject.tag != "TutorialCollider" ){
			death = true;
			GameManager.Instance.EnemyDeath(points);
		}
	}
}
