using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	/*
	[SerializeField] Transform Bullet;
	[SerializeField] float Speed;
	[SerializeField] float DifferenceWindow;
	[SerializeField] Transform Up;
	[SerializeField] Transform Left;
	[SerializeField] Transform Right;

	Vector3 NormalVector;

	private void Start(){
		//set the initial camera-to-bullet distance and angle as the standard
		NormalVector = transform.position - Bullet.position; 
	}

	private void LateUpdate () {
		Vector3 BulletPos = Bullet.position;
		Vector3 MyPos = transform.position;
		Vector3 direc = Vector3.zero;
		//Left-Right axis
		//Left
		if(MyPos.x > BulletPos.x && MyPos.x - BulletPos.x > DifferenceWindow){
			if (MyPos.x >= Left.position.x) {
				direc += Vector3.left * Speed * Time.deltaTime;
			} else {
				transform.LookAt (Bullet);
			}
		//Right
		} else if((MyPos.x < BulletPos.x && BulletPos.x - MyPos.x > DifferenceWindow)){
			if (MyPos.x <= Right.position.x) {
				direc += Vector3.right * Speed * Time.deltaTime;
			} else {
				transform.LookAt (Bullet);
			}
		}
		//Up-Down axis
		MyPos.y += NormalVector.y;
		//Down
		if(MyPos.y < BulletPos.y && BulletPos.y - MyPos.y > DifferenceWindow){
			direc += Vector3.down * Speed * Time.deltaTime;
		//Up
		} else if(MyPos.y > BulletPos.y && MyPos.y - BulletPos.y > DifferenceWindow){
			
			if (MyPos.y <= Up.position.y) {
				direc += Vector3.up * Speed * Time.deltaTime;
			} else {
				transform.LookAt (Bullet);
			}
		}
		//FinalMovement
		transform.Translate(direc);
	}
	*/
	[SerializeField] Transform Bullet;
	[SerializeField] Transform Up;
	[SerializeField] Transform Left;
	[SerializeField] Transform Right;
	[SerializeField] Transform cameraPos;
	[SerializeField] [Range (1,25)] private int TransitionSharpness;

	private Vector3 NormalVector;
	private Quaternion NormalRotation;
	private bool canMove;
	private float LerpState = 0;
	private Vector3 InitialPosition;
	void Start(){
		NormalVector = transform.position - Bullet.position;
		NormalRotation = transform.rotation;
		canMove = false;
	}

	private void getNomrals(){
		NormalVector = transform.position - Bullet.position;
		NormalRotation = transform.rotation;
	}
	void LateUpdate (){
		if(canMove){ 
			MoveCamera();
		}
		else CameraLerp();
		//transform.LookAt (Bullet);
		/*Quaternion rot = transform.rotation;
		rot.x = NormalRotation.x;
		transform.rotation = rot;*/
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
		if(LerpState >= 1.0f) {
			canMove = true;
			getNomrals();
			Bullet.gameObject.GetComponent<BulletMovement>().enabled = true;
		}
	}
}
