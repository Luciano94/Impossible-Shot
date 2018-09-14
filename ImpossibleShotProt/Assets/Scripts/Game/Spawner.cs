using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Factory fabrica;			//la fabrica 
	private float timePerWave = 1f;							//tiempo entre spawns
	private PositionManager pM;
	[SerializeField] private float spawnMinValues;	//valores minimos de aparicion en x e y
	[SerializeField] private float spawnMaxValues;	//valores maximos de aparicion en x e y
	private bool callSpawnOnce;						//si la funcion Go no fué llamada todavía, asegura que solo se llame una vez

	void Awake() {
		//timePerWave = GameManager.Instance.TimePerEnemy;
		pM = PositionManager.Instance;
		callSpawnOnce = true;
	}

	private void Update()
	{
		//timePerWave = GameManager.Instance.TimePerEnemy;
	}

	void SpawnWave()
	{
		GameObject objeto = fabrica.Request ();
		int width = objeto.GetComponent<Product>().Width;
		float posx = pM.getPosition(width);
		objeto.GetComponent<Product>().Index = posx;
		if(posx != -10)
			objeto.transform.position = new Vector3(posx, Random.Range(spawnMinValues, spawnMaxValues), transform.position.z);
		else{
			fabrica.Return(objeto);
		} 
		Invoke ("SpawnWave", timePerWave);
	}

	public void Go(){
		if(callSpawnOnce){
			Invoke ("SpawnWave", timePerWave);
			callSpawnOnce = false;
		}
	}
}
