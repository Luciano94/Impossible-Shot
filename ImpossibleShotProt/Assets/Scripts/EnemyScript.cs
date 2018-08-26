using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float speed;
	private bool active;
	private GameManager gM;
	private EnemyFactory eF;

	void Start () {
		active = false;
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		eF = EnemyFactory.Instance;
	}

	void Update () {
		if (active){
			transform.Translate (Vector3.back * speed * Time.deltaTime);
			if (transform.position.z > -100){
				eF.Return (gameObject);
			}
		}
	}

	public bool Active{
		get{ return active;}
		set{ active = value;}
	}
}
