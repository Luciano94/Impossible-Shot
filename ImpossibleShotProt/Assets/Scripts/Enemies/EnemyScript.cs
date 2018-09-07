using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float speed;
	private GameManager gM;
	private Product product;
	private PositionManager pM;

	void Awake () {
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		product = GetComponent<Product> ();
		pM = PositionManager.Instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		gM.Aceleration();
		int posX;
		if(transform.position.x < 0)
			posX = (int)transform.position.x + 4;
		else
			posX = (int)transform.position.x;
		pM.freePosition(posX,1);
		product.ReturnToFactory ();
	}

	void LateUpdate () {
		if (product.IsActive()){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
	}
}
