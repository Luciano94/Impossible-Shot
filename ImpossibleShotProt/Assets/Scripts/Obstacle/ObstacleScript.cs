using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	private float speed;
	private bool active;
	private GameManager gM;
	private ObstacleFactory oF;

	void Start () {
		active = false;
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		oF = ObstacleFactory.Instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		gM.Death();
	}

	void LateUpdate () {
		if (active){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
			if (transform.position.z < -100){
				oF.Return (gameObject);
			}
		}
	}

	public bool Active{
		get{ return active;}
		set{ active = value;}
	}
}
