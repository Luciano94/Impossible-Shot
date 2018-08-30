using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {
	[SerializeField] Factory Factory;
	[SerializeField] float MaxDist;

	private void Update(){
		if (transform.position.z == MaxDist){
			Factory.Return (gameObject);
		}
	}
}