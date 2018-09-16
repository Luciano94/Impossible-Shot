using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPattern : MonoBehaviour {

    [SerializeField] Transform patterns;
    [SerializeField] float timePerOb;
    private Queue<GameObject> q_patterns;
    private GameObject pattern;
    private int cantOfObs;

    private void Awake() {
        q_patterns = new Queue<GameObject>();
        foreach(Transform child in patterns) {
            q_patterns.Enqueue(child.gameObject);
        }
    }

    public void Spawn() {
        pattern = q_patterns.Dequeue();
        cantOfObs = pattern.GetComponent<Pattern>().TamLista();
        q_patterns.Enqueue(pattern);
        Invoke("SpawnOb", timePerOb);
    }

    public void RandomizePattern(){
        
        CancelInvoke();
        int tam = q_patterns.Count;
        GameObject[] pat = new GameObject[tam];
        for(int k = 0; k<tam;k++)
            pat[k] = q_patterns.Dequeue();
        int i = tam;
        while(i >1){
            i--;
            int j = Random.Range(0, i);
            GameObject go = pat[j];
            pat[j]= pat[i];
            pat[i]=go;
        }
        q_patterns.Clear();
        foreach(GameObject go in pat)
            q_patterns.Enqueue(go);
        Invoke("Spawn",0f);
    }

    private void SpawnOb() {
        GameObject go = pattern.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
        if(pattern.GetComponent<Pattern>().Count() < cantOfObs)
            Invoke("SpawnOb", timePerOb);
        else
            Invoke("Spawn",0f);
    }
}
