using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

	float speed;
	private GameManager gM;
	
	private void Awake() {
		gM=GameManager.Instance;
		speed = gM.BulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//new movement control
		DirectionVec dir =  InputManager.Instance.GetDirection();
		int x = 0;
		int y = 0;
		switch (dir.y) {
		case Direction.Up:
			y = 1;
			break;
		case Direction.Down:
			y = -1;
			break;
		default:
			break;
		}

		switch (dir.x){
		case Direction.Right:
			x = 1;
			break;
		case Direction.Left:
			x = -1;
			break;
		default:
			break;
		}
		//old movement
		Vector3 direc = Vector3.zero;
		direc += Vector3.up * y * speed * Time.deltaTime;
		direc += Vector3.right * x * speed * Time.deltaTime;
		transform.Translate (direc);
		//Rotate(x,y);
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
}
