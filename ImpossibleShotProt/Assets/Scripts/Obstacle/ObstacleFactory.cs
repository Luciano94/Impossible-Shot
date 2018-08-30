using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {

	private static ObstacleFactory instance;
	public static ObstacleFactory Instance{
		get{ 
			if (instance == null) {
				instance = FindObjectOfType<ObstacleFactory> ();
				if(instance == null){
					GameObject go = new GameObject ("ObstacleFactory");
					instance = go.AddComponent<ObstacleFactory> ();
				}
			}
			return instance;
		}
	}
		
	void Awake(){
		if (Instance != this){ Destroy (gameObject);}
	}

	private GameObject[] cargador;

	void Start(){
		cargador = new GameObject[10];
		for(int i = 0; i < 10; i++){
			GameObject go = Instantiate (Resources.Load("Prefabs/ObstaclePrefab", typeof (GameObject)) as GameObject);
			go.transform.position = Vector3.one * 60;
			cargador [i] = go;
		}
	}

	public GameObject Request(){
		//funcion para que llamen los spawners

		for(int i = 0; i < 10; i++){
			if (!cargador[i].GetComponent<ObstacleScript>().Active){
				return cargador [i];
			}
		}
		Return (cargador[0]);
		return cargador [0];
	}

	public void Return(GameObject go){
		//funcion para que llamen los obstaculos al quedar fuera de pantalla
		bool done = false;
		for (int i = 0; i < 10; i++){
			if(cargador[i] == go){
				done = true;
				go.GetComponent<ObstacleScript> ().Active = false;
				go.transform.position = Vector3.one * 60;
				break;
			}
		}
		if(!done){
			Destroy (go);
		}
	}

	private void OnDestroy(){
		for(int i = 0; i < 10; i++){
			Destroy (cargador[i]);
		}
	}
}
