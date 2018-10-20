using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPattern : MonoBehaviour {

    [SerializeField] Transform[] totalOfPatterns;
    [SerializeField] int cantLogTOP = 0;
    [SerializeField] float timePerOb;
    [SerializeField] private int cantOfPatterns = 1;
    [SerializeField] private float timePerPat;
    private Queue<GameObject> q_patterns;
    private Queue<GameObject> q_PatternsLvl;
    private GameObject pattern;
    private float timePerPatter = 0;
    private int actualCOP;

    private void Awake() {
        q_patterns = new Queue<GameObject>();
        q_PatternsLvl = new Queue<GameObject>();
        ChargePatterns(totalOfPatterns[cantLogTOP]);
    }

    private void ChargePatterns(Transform patterns){
        foreach(Transform child in patterns)
            q_patterns.Enqueue(child.gameObject);
        cantOfPatterns = q_patterns.Count;
        for(int i = 0; i<cantOfPatterns;i++){
            GameObject go = q_patterns.Dequeue();
            q_PatternsLvl.Enqueue(go);
            q_patterns.Enqueue(go);
        }
        RandomizePattern();
        Spawn();
    }

   /* public void ReCharguePatterns(){
        int level = GameManager.Instance.Level;
        q_patterns.Clear();
        q_PatternsLvl.Clear();
        if(level == lvlToNormal)
            ChargePatterns(patternsGONormal);
        if(level == lvlToHard)
            ChargePatterns(patternsGOHard);
    }*/

    public void Spawn() {
        if(q_PatternsLvl.Count > 0) {
            pattern = q_PatternsLvl.Dequeue();
            SpawnOb();
        }
    }

    public void RandomizePattern(){
        
        CancelInvoke();
        Randomize();
        q_PatternsLvl.Clear();
        for(int i = 0; i<cantOfPatterns;i++){
            GameObject go = q_patterns.Dequeue();
            q_PatternsLvl.Enqueue(go);
            q_patterns.Enqueue(go);
        }
    }

    private void Randomize(){
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
    }

    private void SpawnOb() {
        GameObject go = pattern.GetComponent<Pattern>().Request();
        go.transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z);
        if(pattern.GetComponent<Pattern>().Count() > 0)
            Invoke("SpawnOb", timePerOb);
        else{
            Invoke("Spawn", timePerPatter);
        }
    } 


    public void TimePerObs(){
        Debug.Log(totalOfPatterns.Length);
        if(cantLogTOP < totalOfPatterns.Length -1){
            cantLogTOP++;
            ChargePatterns(totalOfPatterns[cantLogTOP]);
        }
        else{ 
            cantLogTOP = totalOfPatterns.Length -1;
            ChargePatterns(totalOfPatterns[cantLogTOP]);
        }
        if(cantLogTOP == totalOfPatterns.Length){
            timePerPatter = timePerPat;
        }
        timePerOb -= 0.20f;
        if(timePerOb <= 0.5f){
            timePerOb = 0.5f;
        }
    }
}
