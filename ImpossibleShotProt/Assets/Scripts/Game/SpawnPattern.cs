using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPattern : MonoBehaviour {

    [SerializeField] Transform patternsGOEasy;
    [SerializeField] Transform patternsGONormal;
    [SerializeField] Transform patternsGOHard;
    [SerializeField] float timePerOb;
    [SerializeField] private int cantOfPatterns = 1;
    [SerializeField] private int lvlToNormal = 5;
    [SerializeField] private int lvlToHard = 10;
    private Queue<GameObject> q_patterns;
    private Queue<GameObject> q_PatternsLvl;
    private GameObject pattern;
    private int cantOfObs;
    private int actualCOP;
    public int LvlToNormal{
        get{return lvlToNormal;}
    }

    public int LvlToHard{
        get{return lvlToHard;}
    }

    private void Awake() {
        q_patterns = new Queue<GameObject>();
        q_PatternsLvl = new Queue<GameObject>();
        ChargePatterns(patternsGOEasy);
        Invoke ("Spawn", timePerOb);
    }

    private void ChargePatterns(Transform patterns){
        foreach(Transform child in patterns)
            q_patterns.Enqueue(child.gameObject);
        for(int i = 0; i<cantOfPatterns;i++){
            GameObject go = q_patterns.Dequeue();
            q_PatternsLvl.Enqueue(go);
            q_patterns.Enqueue(go);
        }
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
        pattern = q_PatternsLvl.Dequeue();
        cantOfObs = pattern.GetComponent<Pattern>().TamLista();
        q_PatternsLvl.Enqueue(pattern);
        Invoke("SpawnOb", timePerOb);
    }

    public void RandomizePattern(){
        
        CancelInvoke();
        Randomize();
        cantOfPatterns++;
        q_PatternsLvl.Clear();
        for(int i = 0; i<cantOfPatterns;i++){
            GameObject go = q_patterns.Dequeue();
            q_PatternsLvl.Enqueue(go);
            q_patterns.Enqueue(go);
        }
        Invoke("Spawn",0f);
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
        if(pattern.GetComponent<Pattern>().Count() < cantOfObs)
            Invoke("SpawnOb", timePerOb);
        else{
            Debug.Log("Level: "+ GameManager.Instance.Level);
            GameManager.Instance.PassLvl();
            Invoke("Spawn",0f);
        }
    } 


    public void TimePerObs(){
        timePerOb -= 0.20f;
        if(timePerOb <= 0.5f){
            timePerOb = 0.5f;
        }
    }
}
