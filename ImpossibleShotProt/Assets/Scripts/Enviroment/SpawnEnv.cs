using System.Collections.Generic;
using UnityEngine;

public class SpawnEnv : MonoBehaviour {

    public static SpawnEnv instance;

    public static SpawnEnv Instance {
        get {
            instance = FindObjectOfType<SpawnEnv>();
            if (instance == null) {
                GameObject go = new GameObject("SpawnEnv");
                instance = go.AddComponent<SpawnEnv>();
            }
            return instance;
        }
    }

    [SerializeField]private GameObject[] prefabs;
    [SerializeField]private GameObject[] props;
    [SerializeField]private float timePerObj;
    [SerializeField]private int pool = 50;
    [SerializeField]private int propPool = 10;
    private Queue<GameObject> envArray;
    private Queue<GameObject> propArray;
    private int random;

    public void UpdateTime() {
        timePerObj -= 0.02f;
        if (timePerObj <= 0.1f){
            timePerObj = 0.1f;
        }
    }

    private void Awake() {
        envArray = new Queue<GameObject>();
        propArray = new Queue<GameObject>();
        GameObject go;
        int prefabIndex = 0;
        for (int i = 0; i < pool; i++) {
            go = Instantiate(prefabs[prefabIndex], transform.position, transform.rotation);
            if(prefabIndex == prefabs.Length - 1){
                prefabIndex= 0;
            }else{ 
                prefabIndex++;
            }
            go.SetActive(false);
            envArray.Enqueue(go);
        }
        int propIndex = 0;
        for (int i = 0; i < propPool; i++){
            go = Instantiate(props[propIndex], transform.position, transform.rotation);
            if(propIndex == props.Length - 1){
                propIndex= 0;
            }else{ 
                propIndex++;
            }
            go.SetActive(false);
            propArray.Enqueue(go);
        }
        Invoke("Spawn", timePerObj);
    }

    private void Spawn() {
        GameObject go;
        if (envArray.Count > 0) {
            go = envArray.Dequeue();
            go.SetActive(true);
        }
        random = Random.Range(0,10);
        if(random <= 1 && propArray.Count > 0){
            go = propArray.Dequeue();
            go.SetActive(true);
        }
        Invoke("Spawn", timePerObj);
    }

    public void Despawn(GameObject go) {
        go.transform.position = transform.position;
        go.SetActive(false);
        envArray.Enqueue(go);
    }

    public void DespawnProp(GameObject go){
        go.transform.position = transform.position;
        go.SetActive(false);
        propArray.Enqueue(go); 
    }
}
