using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private GameObject prefab;		//prefab del objeto a spawnear
	private float timePerWave;						//tiempo entre spawns
	[SerializeField] private Vector2 spawnMinValues;//valores minimos de aparicion en x e y
	[SerializeField] private Vector2 spawnMaxValues;//valores maximos de aparicion en x e y

	// Use this for initialization
	void Awake() {
		timePerWave = GameManager.Instance.TimePerEnemy;
		if (prefab.tag == "Enemy")
			Invoke("SpawnWave", timePerWave);
		else
			Invoke("SpawnObs", timePerWave);
	}

	private void Update()
	{
		timePerWave = GameManager.Instance.TimePerEnemy;
	}

	void SpawnWave()
	{
		Vector3 spawnPosition = new Vector3(Random.Range(spawnMinValues.x, spawnMaxValues.x), Random.Range(spawnMinValues.y, spawnMaxValues.y), transform.position.z);
		prefab = EnemyFactory.Instance.Request();
		prefab.transform.position = spawnPosition;
		prefab.GetComponent<EnemyScript>().Active = true;
		Invoke("SpawnWave", timePerWave);
	}

	void SpawnObs()
	{
		Vector3 spawnPosition = new Vector3(Random.Range(spawnMinValues.x, spawnMaxValues.x), Random.Range(spawnMinValues.y, spawnMaxValues.y), transform.position.z);
		prefab = ObstacleFactory.Instance.Request();
		prefab.transform.position = spawnPosition;
		prefab.GetComponent<ObstacleScript>().Active = true;
		Invoke("SpawnObs", timePerWave);
	}
}
