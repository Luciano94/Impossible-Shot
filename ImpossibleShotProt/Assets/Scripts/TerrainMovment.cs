using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovment : MonoBehaviour {

	[SerializeField] private Transform back;
	private float speed;

	private void Update()
	{
		speed = GameManager.Instance.TerrainSpeed;
		if(transform.position.z > -100)
		{
			transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		}else{
			if (transform.position.z != back.position.z)
			{
				transform.position = back.TransformDirection(back.position);
			}
		}
	}
}
