using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvScript : MonoBehaviour {
	[SerializeField] float distZ;
	private GameManager gameManager;
	private float speed;

	private void Awake() {
		gameManager = GameManager.Instance;
	}
	private void LateUpdate() {
		speed = gameManager.TerrainSpeed;
		transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		if(transform.position.z < distZ)
			Destroy(gameObject);
	}
}
