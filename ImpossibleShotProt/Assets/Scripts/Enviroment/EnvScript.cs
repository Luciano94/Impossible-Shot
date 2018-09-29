using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvScript : MonoBehaviour {
	[SerializeField] float distZ;
	private GameManager gameManager;
	private SpawnEnv spawnEnv;
	private float speed;

	private void Awake() {
		gameManager = GameManager.Instance;
		spawnEnv = SpawnEnv.Instance;
	}
	private void LateUpdate() {
		speed = gameManager.TerrainSpeed;
		transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		if(transform.position.z < distZ)
			spawnEnv.Despawn(gameObject);
	}
}
