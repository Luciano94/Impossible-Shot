using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Factory EnemyFactory; 		//fabrica de enemigos
	[SerializeField] private Factory ObstacleFactory; 	//fabrica de obstaculos
	private float timePerWave;							//tiempo entre spawns
	[SerializeField] private Vector2 spawnMinValues;	//valores minimos de aparicion en x e y
	[SerializeField] private Vector2 spawnMaxValues;	//valores maximos de aparicion en x e y
	// Use this for initialization
	void Awake() {
		timePerWave = GameManager.Instance.TimePerEnemy;
		Invoke ("SpawnWave", timePerWave);
		Invoke ("SpawnObs", timePerWave);
	}

	private void Update()
	{
		timePerWave = GameManager.Instance.TimePerEnemy;
	}

	void SpawnWave()
	{
		Vector3 spawnPosition = new Vector3(Random.Range(spawnMinValues.x, spawnMaxValues.x), Random.Range(spawnMinValues.y, spawnMaxValues.y), transform.position.z);
		GameObject enem = EnemyFactory.Request ();
		enem.transform.position = spawnPosition;
		enem.GetComponent<EnemyScript> ().Active = true;
		Invoke ("SpawnWave", timePerWave);
	}

	void SpawnObs()
	{
		Vector3 spawnPosition = new Vector3(Random.Range(spawnMinValues.x, spawnMaxValues.x), Random.Range(spawnMinValues.y, spawnMaxValues.y), transform.position.z);
		GameObject obs = ObstacleFactory.Request();
		obs.transform.position = spawnPosition;
		obs.GetComponent<ObstacleScript> ().Active = true;
		Invoke ("SpawnObs", timePerWave);
	}
}
