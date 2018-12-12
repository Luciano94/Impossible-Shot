using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	private float speed;
	private bool hitOnce;
	private Product product;

	void Awake(){
		speed = GameManager.Instance.TerrainSpeed;
		product = GetComponent<Product> ();
		hitOnce = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(hitOnce){
			hitOnce = false;
			SoundManager.Instance.WoodImpact();
			GameManager.Instance.Death();
			Invoke("Return", 1.0f);
		}
	}

	public void Return(){
		product.ReturnToFactory();
	}

	void LateUpdate () {
		if (product.IsActive()){
			speed = GameManager.Instance.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		} else{
			hitOnce = true;
		}
	}
}
