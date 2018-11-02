using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	private float speed;
	private bool hitOnce;
	private Product product;
	private GameManager gM;

	void Awake(){
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		product = GetComponent<Product> ();
		hitOnce = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(hitOnce){
			hitOnce = false;
			gM.Death();
		}
	}

	void LateUpdate () {
		if (product.IsActive()){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		} else{
			hitOnce = true;
		}
	}
}
