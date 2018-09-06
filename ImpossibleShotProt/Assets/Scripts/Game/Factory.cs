using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
	[SerializeField] GameObject Product;
	[SerializeField] int Capacidad;
	private GameObject[] cargador;
	private bool[] tracker;

	void Start () {
		cargador = new GameObject[Capacidad];
		tracker = new bool[Capacidad];
		for(int i = 0; i< Capacidad; i++){
			GameObject go = Instantiate (Product);
			go.transform.position = Vector3.one * 60;
			if(go.tag == "Enemy"){
				go.GetComponent<EnemyScript> ().Active = false;
			} else if (go.tag == "Obstacle"){
				go.GetComponent<ObstacleScript> ().Active = false;
			}
			cargador [i] = go;
			tracker [i] = true;
		}
	}
	
	public GameObject Request(){
		for(int i = 0; i< Capacidad; i++){
			if(tracker[i]){
				tracker [i] = false;
				return cargador[i];
			}
		}
		return Instantiate (Product);;
	}

	public void Return(GameObject go){
		bool extra = true;
		for (int i = 0; i < Capacidad; i++){
			if(go == cargador[i]){
				tracker[i] = true;
				extra = false;
				cargador [i].transform.position = Vector3.one * 60;
				if(go.tag == "Enemy"){
					go.GetComponent<EnemyScript> ().Active = false;
				} else if (go.tag == "Obstacle"){
					go.GetComponent<ObstacleScript> ().Active = false;
				}
				//Debug.Log (go.tag + " se recicla");
			}
		}
		if (extra){
			Destroy (go);
			//Debug.Log ("se destruye");
		}
	}

	private void OnDestroy(){
		for (int i = 0; i < Capacidad; i++) {
			Destroy (cargador [i]);
		}
	}
}