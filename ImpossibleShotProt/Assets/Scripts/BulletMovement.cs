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
		transform.Translate (direc);

	}
}
