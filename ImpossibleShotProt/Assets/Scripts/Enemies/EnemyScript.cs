using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float speed;
	private GameManager gM;
	private Product product;
	private PositionManager pM;
	private int width;


	void Awake () {
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		product = GetComponent<Product> ();
		width = product.Width;
		pM = PositionManager.Instance;
	}

	private void OnTriggerExit(Collider other)
	{
		//gM.Aceleration();
		pM.freePosition(product.Index, width);
		product.ReturnToFactory ();
	}

	void LateUpdate () {
		if (product.IsActive()){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
	}
}
