using UnityEngine;

public class CameraMovement : MonoBehaviour {
	[SerializeField] private  Transform Bullet;
	[SerializeField] private Transform Up;
	[SerializeField] private Transform Left;
	[SerializeField] private Transform Right;
	[SerializeField] private Transform cameraPos;
	[SerializeField] [Range (1,25)] private int TransitionSharpness;

	private Vector3 NormalVector;
	private bool canMove;
	private float LerpState = 0;
	private Vector3 InitialPosition;
	
	void Start(){
		NormalVector = transform.position - Bullet.position;
		canMove = false;
	}

	private void getNomrals(){
		NormalVector = transform.position - Bullet.position;
	}

	void LateUpdate (){
        if (canMove)
        {
            MoveCamera();
        } else {
            CameraLerp();
        }
	}

	private void MoveCamera(){
		Vector3 pos = Bullet.position + NormalVector;
		if(pos.x > Right.position.x ){ pos.x = Right.position.x;}
		if(pos.x < Left.position.x){ pos.x = Left.position.x;}
		if(pos.y > Up.position.y){pos.y = Up.position.y;}
		transform.position = pos;
	}

	private void CameraLerp(){
		LerpState += TransitionSharpness * Time.deltaTime;
		if (LerpState > 1.0f) {LerpState = 1.0f;}

		transform.position = Vector3.Lerp (transform.position, cameraPos.position, LerpState);
		transform.rotation = Quaternion.Lerp(transform.rotation,  cameraPos.rotation, LerpState);
		if(LerpState >= 1.0f) {
			canMove = true;
			getNomrals();
			if(!GameManager.Instance.InTutorial()){
				Bullet.gameObject.GetComponent<BulletMovement>().enabled = true;
			}
		}
	}
}
