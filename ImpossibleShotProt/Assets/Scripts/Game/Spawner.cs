using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Factory fabrica;			//la fabrica 
	private float timePerWave;							//tiempo entre spawns
	private PositionManager pM;
	[SerializeField] private Vector2 spawnMinValues;	//valores minimos de aparicion en x e y
	[SerializeField] private Vector2 spawnMaxValues;	//valores maximos de aparicion en x e y
	// Use this for initialization
	void Awake() {
		timePerWave = GameManager.Instance.TimePerEnemy;
		pM = PositionManager.Instance;
		Invoke ("SpawnWave", timePerWave);
	}

	private void Update()
	{
		timePerWave = GameManager.Instance.TimePerEnemy;
	}

	void SpawnWave()
	{
		GameObject objeto = fabrica.Request ();
		objeto.transform.position = getSpawnPosition(objeto.tag);
		if (objeto.tag == "Enemy") {
			objeto.GetComponent<EnemyScript> ().Active = true;
		} else {
			objeto.GetComponent<ObstacleScript> ().Active = true;
		}
		Invoke ("SpawnWave", timePerWave);
	}

	private Vector3 getSpawnPosition(string tag){
		int posX;
		if(tag == "Enemy")
			posX = pM.getPosition(1);
		else
			posX = pM.getPosition(2);
		if(posX < 5)
			return new Vector3(posX-4,1,transform.position.z);
		else
			return new Vector3(posX,1,transform.position.z);

	}
}
