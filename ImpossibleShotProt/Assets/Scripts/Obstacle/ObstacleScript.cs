using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	private float speed;
	private Product product;
	private GameManager gM;

	void Awake(){
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		product = GetComponent<Product> ();
	}

	private void OnTriggerEnter(Collider other)
	{
		gM.Death();
	}

	void LateUpdate () {
		if (product.IsActive()){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
	}
}
