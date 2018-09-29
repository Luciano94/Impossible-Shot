using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnv : MonoBehaviour {

	public static SpawnEnv instance;

	   public static SpawnEnv Instance {
        get {
            instance = FindObjectOfType<SpawnEnv>();
            if(instance == null) {
                GameObject go = new GameObject("SpawnEnv");
                instance = go.AddComponent<SpawnEnv>();
            }
            return instance;
        }
    }

	[SerializeField] GameObject prefab;
	[SerializeField] float timePerObj;
	[SerializeField] int pool = 50;
	private Queue<GameObject> envArray;

	public void UpdateTime(){
		timePerObj -= 0.02f;
		if(timePerObj <= 0.1f)
			timePerObj =0.1f;
	}
	private void Awake() {
		envArray = new Queue<GameObject>();
		GameObject go;
		for(int i=0; i < pool;i++){
			go = Instantiate(prefab,transform.position,transform.rotation);
			go.SetActive(false);
			envArray.Enqueue(go);
		}
		Invoke("Spawn", timePerObj);
	}

	private void Spawn(){
		GameObject go;
		if(envArray.Count > 0){
			go = envArray.Dequeue();
			go.SetActive(true);
		}
		Invoke("Spawn", timePerObj);
	}

	public void Despawn(GameObject go){
		go.transform.position = transform.position;
		go.SetActive(false);
		envArray.Enqueue(go);
	}
}
