using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float speed;
	private bool active;
	private GameManager gM;
	private Product pScript;

	void Start () {
		active = false;
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		pScript = GetComponent<Product> ();
	}

	private void OnTriggerEnter(Collider other)
	{
		gM.Aceleration();
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
