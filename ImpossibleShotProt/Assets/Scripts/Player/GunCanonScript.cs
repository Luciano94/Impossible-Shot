using UnityEngine;

public class GunCanonScript : MonoBehaviour {

	[SerializeField] private GameObject explosion;
	[SerializeField] private Transform targetPos;
	[SerializeField] [Range (1,25)] private int TransitionSharpness;

	private float LerpState = 0;
	private bool gameStart = false;

	private void Awake(){
		explosion.SetActive(false);
	}

	public void GameStart(){
		gameStart = true;
		explosion.SetActive(true);
	}

	private void Lerp(){
		LerpState += TransitionSharpness * Time.deltaTime;
		if (LerpState > 1.0f) {LerpState = 1.0f;}

		transform.position = Vector3.Lerp (transform.position, targetPos.position, LerpState);
		if(LerpState >= 1.0f){
			Destroy(gameObject, 0.5f);
		}
	}

	private void LateUpdate(){
        if (gameStart) { Lerp(); }
	}
	
}
