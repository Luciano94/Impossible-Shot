using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

	//float speed;
	//private GameManager gM;

	Vector3 MatrixCenter;
	[SerializeField] float DistanceToCenter;
	[SerializeField][Range (0f,1f)] float IgnorableDistance;
	[SerializeField][Range (0f,1f)] float TransitionSharpness;
	int x = 0;
	int y = 0;
	int yNext = 0;
	int xNext = 0;
	bool moving = false;
	private void Awake() {
        /*gM=GameManager.Instance;
		speed = gM.BulletSpeed;*/
        MatrixCenter = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//new movement control
		//si no me estoy ya moviento
		if (!moving) {
			//detecto orden de movimiento
			moveDetection();
			if(xNext != x || yNext != y){
				moving = true; //de ser necesario comienzo a moverme;
			}
		} else {
			moveAction ();
		}
		//detecto cambios de movimiento
		/*
		//old movement
		Vector3 direc = Vector3.zero;
		direc += Vector3.up * y * speed * Time.deltaTime;
		direc += Vector3.right * x * speed * Time.deltaTime;
		transform.Translate (direc);
		//Rotate(x,y);
		*/

	}

	private void Rotate(int x, int y){
		if (y == 0)
			transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		else
			transform.rotation = new Quaternion(-(0.1f * y),transform.rotation.y, transform.rotation.z, transform.rotation.w);
		if (x == 0)
			transform.rotation = new Quaternion(transform.rotation.x,0, transform.rotation.z, transform.rotation.w);
		else
			transform.rotation = new Quaternion(transform.rotation.x, (0.1f * x), transform.rotation.z, transform.rotation.w);
	}

	private void moveDetection(){
		DirectionVec dir = InputManager.Instance.GetDirection ();
		//Debug.Log (dir.x + " " + dir.y );
		switch (dir.y) {
		case Direction.Up:
			yNext += 1;
			break;
		case Direction.Down:
			yNext -= 1;
			break;
		default:
			break;
		}

		switch (dir.x) {
		case Direction.Right:
			xNext += 1;
			break;
		case Direction.Left:
			xNext -= 1;
			break;
		default:
			break;
		}
		if (xNext > 1) {xNext = 1;}
		if (xNext < -1) {xNext = -1;}
		if (yNext > 1) {yNext = 1;}
		if (yNext < -1) {yNext = -1;}
	}

	private void moveAction(){
		float yTarget = MatrixCenter.y + (DistanceToCenter * yNext);
		float xTarget = MatrixCenter.x + (DistanceToCenter * xNext);

		/*bool xFix = false;
		bool yFix = false;

		if(((xTarget > transform.position.x) && xTarget - transform.position.x <= IgnorableDistance) || 
			((xTarget < transform.position.x) && transform.position.x - xTarget <= IgnorableDistance)){
			xFix = true;
		}

		if(((yTarget > transform.position.y) && yTarget - transform.position.y <= IgnorableDistance) || 
			((yTarget < transform.position.y) && transform.position.y - yTarget <= IgnorableDistance)){
			yFix = true;
		}

		Debug.Log (xFix + "" + yFix);*/
		Vector3 Target = new Vector3 (xTarget, yTarget, transform.position.z);
		if (Vector3.Distance(transform.position,Target) <= IgnorableDistance) {
			transform.position = Target;
			y = yNext;
			x = xNext;
			moving = false;
		} else {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (xTarget, yTarget, transform.position.z), TransitionSharpness);
		}
	}
}
