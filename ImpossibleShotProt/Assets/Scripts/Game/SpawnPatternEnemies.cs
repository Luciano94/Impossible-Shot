using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatternEnemies : MonoBehaviour {
	[SerializeField] Transform patterns;
	[SerializeField] float timeperEnemy;
	private Queue<GameObject> q_patterns;
	private GameObject pattern;
	private int cantofEnemies;

	private void Awake() {
		q_patterns = new Queue<GameObject>();
        foreach(Transform child in patterns) {
        	q_patterns.Enqueue(child.gameObject);
        }
	}

	    public void Spawn() {
        pattern = q_patterns.Dequeue();
        cantofEnemies = pattern.GetComponent<Pattern>().TamLista();
        Invoke("SpawnOb", timeperEnemy);
    }

    private void SpawnOb() {
        GameObject go = pattern.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
        if(pattern.GetComponent<Pattern>().Count() < cantofEnemies)
            Invoke("SpawnOb", timeperEnemy);
        else{
            q_patterns.Enqueue(pattern.gameObject);
            Invoke("Spawn", 0f);
        }
    }
}
