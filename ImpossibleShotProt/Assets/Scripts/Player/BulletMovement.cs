using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
	private Vector3 MatrixCenter;
	[SerializeField] private float DistanceToCenter;
	[SerializeField][Range (0f,1f)] private float IgnorableDistance;
	[SerializeField][Range (0f,1f)] private float TransitionSharpness;
	private int x = 0;
	private int y = 0;
	private int yNext = 0;
	private int xNext = 0;
	private bool moving = false;
	private void Awake() {
        MatrixCenter = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
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
