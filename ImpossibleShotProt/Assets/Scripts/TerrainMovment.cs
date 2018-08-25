using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovment : MonoBehaviour {

	private float distance = 0;
	[SerializeField] private GameObject terrain;
	[SerializeField] private float speed;

	void Update () {
		distance = terrain.transform.position.z;
		if(distance > -100)
		{
			terrain.transform.Translate((Vector3.back * speed * Time.deltaTime));
		}else
		{
			distance = 199;
			terrain.transform.Translate(Vector3.forward * (distance));
		}
	}
}
