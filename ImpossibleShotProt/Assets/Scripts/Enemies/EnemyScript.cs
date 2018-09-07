using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float speed;
	private bool active;
	private GameManager gM;
	private Product pScript;
	private PositionManager pM;

	void Start () {
		active = false;
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		pScript = GetComponent<Product> ();
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
		pScript.ReturnToFactory ();
	}

	void LateUpdate () {
		if (active){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
	}

	public bool Active{
		get{ return active;}
		set{ active = value;}
	}
}
