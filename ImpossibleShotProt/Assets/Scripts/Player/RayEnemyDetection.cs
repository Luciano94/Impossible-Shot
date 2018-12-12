using UnityEngine;

public class RayEnemyDetection : MonoBehaviour {

	private Ray detectionRay;
	private RaycastHit raycastHit;

	private void Start() {
		detectionRay = new Ray(transform.position, transform.forward * 10);
	}

	private void LateUpdate() {
		detectionRay.origin = transform.position;
		if(Physics.Raycast(detectionRay, out raycastHit)){
			if(raycastHit.distance < 10)
				if(raycastHit.transform.gameObject.tag == "Enemy"){
					ChangeSprite(raycastHit.transform.gameObject.GetComponent<SpriteChange>());
				}
		}
	}

	private void ChangeSprite(SpriteChange spr){
		if(!spr.IsScreaming){
			SoundManager.Instance.EnemyScream();
			spr.ChangeSpriteScream();
		}
	}
	
}
