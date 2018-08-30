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
			//go.SetActive(false);
			cargador [i] = go;
			tracker [i] = true;
		}
	}
	
	public bool Request(GameObject go){
		for(int i = 0; i< Capacidad; i++){
			if(tracker[i]){
				tracker [i] = false;
				go = cargador [i];
				return true;
			}
		}
		return false;
	}

	public void Return(GameObject go){
		for (int i = 0; i < Capacidad; i++){
			if(go == cargador[i]){
				tracker[i] = true;
				cargador [i].transform.position = Vector3.one * 60;
				break;
			}
		}
	}

	private void OnDestroy(){
		for (int i = 0; i < Capacidad; i++) {
			Destroy (cargador [i]);
		}
	}
}