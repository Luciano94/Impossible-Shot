using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPattern : MonoBehaviour {

    [SerializeField] Transform patterns;
    [SerializeField] float timePerOb;
    private Queue<GameObject> q_patterns;
    private GameManager gM;
    private float timer;
    private GameObject pattern;
    private int cantOfObs;

    private void Awake() {
        gM = GameManager.Instance;
        timer = 0;
        q_patterns = new Queue<GameObject>();
        foreach(Transform child in patterns) {
            q_patterns.Enqueue(child.gameObject);
        }
    }

    public bool Spawn() {
        if(q_patterns.Count == 0) return false;
        pattern = q_patterns.Dequeue();
        cantOfObs = pattern.GetComponent<Pattern>().TamLista();
        Invoke("SpawnOb", timePerOb);
        return true;
    }

    private void SpawnOb() {
        GameObject go = pattern.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
        cantOfObs++;
        if(pattern.GetComponent<Pattern>().Count() < cantOfObs)
            Invoke("SpawnOb", timePerOb);
        else
            Invoke("Spawn", timePerOb);
    }
}
