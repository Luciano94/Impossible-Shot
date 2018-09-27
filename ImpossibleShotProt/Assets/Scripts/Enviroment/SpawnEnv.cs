using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnv : MonoBehaviour {

	[SerializeField] GameObject prefab;
	[SerializeField] float timePerObj;

	public void UpdateTime(){
		timePerObj -= 0.02f;
	}
	private void Awake() {
		Invoke("Spawn", timePerObj);
	}

	private void Spawn(){
		Instantiate(prefab, transform.position, transform.rotation);
		Invoke("Spawn", timePerObj);
	}
}
