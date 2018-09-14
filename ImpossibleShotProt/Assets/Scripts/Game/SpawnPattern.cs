using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPattern : MonoBehaviour {

    [SerializeField] Transform patterns;
    [SerializeField] float timePerOb;
    private Queue<GameObject> q_patterns;
//    private GameManager gM;
    private GameObject pattern;
    private int cantOfObs;

    private void Awake() {
 //       gM = GameManager.Instance;
        q_patterns = new Queue<GameObject>();
        foreach(Transform child in patterns) {
            q_patterns.Enqueue(child.gameObject);
        }
    }

    public void Spawn() {
        pattern = q_patterns.Dequeue();
        cantOfObs = pattern.GetComponent<Pattern>().TamLista();
        Invoke("SpawnOb", timePerOb);
    }

    private void SpawnOb() {
        GameObject go = pattern.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
        if(pattern.GetComponent<Pattern>().Count() < cantOfObs)
            Invoke("SpawnOb", timePerOb);
        else{
            q_patterns.Enqueue(pattern);
            Invoke("Spawn", 0f);
        }
    }
}
