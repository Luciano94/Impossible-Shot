using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
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
}
