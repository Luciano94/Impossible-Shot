using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentLimit : MonoBehaviour {

	[SerializeField]private Vector2 limits; 

	void Update () {
		if (transform.position.x > limits.x)
			transform.position = new Vector3 (limits.x, transform.position.y, transform.position.z);
		else if (transform.position.x < (-limits.x))
			transform.position = new Vector3 ((-limits.x), transform.position.y, transform.position.z);
		if (transform.position.y > limits.y)
			transform.position = new Vector3 (transform.position.x, limits.y, transform.position.z);
		else if (transform.position.y < 0.5f)
			transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
	}
}
