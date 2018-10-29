using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour {
	[SerializeField] private GameObject[] Lista;
	private GameObject[] pool;
	private TutorialPattern tutorialPattern;
	private int counter = 0;

	void Awake(){
		pool = new GameObject[Lista.Length];
		for (int i = 0; i<Lista.Length; i++){
			GameObject go = Instantiate (Lista [i]);
			go.transform.position = Vector3.one * 60;
			go.GetComponent<Product> ().Patron = this;
			pool [i] = go;
		}
	}

	void Start(){
		tutorialPattern = GetComponent<TutorialPattern>();
	}

	public void Return(GameObject go){
		for (int i = 0; i < Lista.Length; i++){
			if(go == pool[i]){
				pool [i].transform.position = Vector3.one * 60;
			}
			if(tutorialPattern && i == Lista.Length-1){tutorialPattern.OnPatternEnd(); }
		}
	}

	public GameObject Request(){
		int count = counter;
		counter++;
		if(counter>= Lista.Length){
			counter = 0;
		}
        pool[count].GetComponent<Product>().Sent();
		return pool [count];
	}

    public int TamLista() {
        return pool.Length-1;
    }

    public int Count() {
        return counter;
    }

	public GameObject[] GetList(){
		return Lista;
	}
}
