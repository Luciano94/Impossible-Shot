using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float speed;
	private GameManager gM;
	private Product product;
	private int width;


	void Awake () {
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		product = GetComponent<Product> ();
	}

	private void OnTriggerExit(Collider other)
	{
		gM.EnemyDeath();
		product.ReturnToFactory ();
	}

	void LateUpdate () {
		if (product.IsActive()){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
	}
}
