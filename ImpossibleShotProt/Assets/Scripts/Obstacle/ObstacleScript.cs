using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	private float speed;
	private bool active;
	private GameManager gM;

	void Start () {
		active = false;
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
	}

	private void OnTriggerEnter(Collider other)
	{
		gM.Death();
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
