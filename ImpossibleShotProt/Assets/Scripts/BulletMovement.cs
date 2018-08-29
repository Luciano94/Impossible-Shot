using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

	[SerializeField] float Speed;
	

	
	// Update is called once per frame
	void Update () {
		Vector3 direc = Vector3.zero;
		direc += Vector3.up * Input.GetAxis ("Vertical") * Speed * Time.deltaTime;
		direc += Vector3.right * Input.GetAxis ("Horizontal") * Speed * Time.deltaTime;
		Vector3 rot = new Vector3(-direc.y, direc.x,0);
		transform.Rotate(rot);	
		transform.Translate (direc);

	}
}
