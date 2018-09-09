using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Factory fabrica;			//la fabrica 
	private float timePerWave;							//tiempo entre spawns
	private PositionManager pM;
	[SerializeField] private float spawnMinValues;	//valores minimos de aparicion en x e y
	[SerializeField] private float spawnMaxValues;	//valores maximos de aparicion en x e y
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
		int width = objeto.GetComponent<Product>().Width;
		float posx = pM.getPosition(width);
		objeto.GetComponent<Product>().Index = posx;
		if(posx != -10)
			objeto.transform.position = new Vector3(posx ,Random.Range(spawnMinValues,spawnMaxValues),
													transform.position.z);
		else{
			fabrica.Return(objeto);
		} 
		Invoke ("SpawnWave", timePerWave);
	}

}
