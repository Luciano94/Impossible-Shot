using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnv : MonoBehaviour {

	[SerializeField] float distZ;
	private float speed;

	private void LateUpdate() {
		speed = GameManager.Instance.TerrainSpeed;
		transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		if(transform.position.z < distZ)
			Destroy(gameObject);
	}
}
