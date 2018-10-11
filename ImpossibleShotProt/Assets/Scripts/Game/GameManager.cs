using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance {
        get {
            instance = FindObjectOfType<GameManager>();
            if(instance == null) {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    //[SerializeField] float fOVPerLevel = 5f;
    //[SerializeField] float maxFOV = 80f;
    //[SerializeField] float maxSpeed = 100f;
    //[SerializeField] float speedPerLevel = 10f;
    //[SerializeField] int pointsperEne = 100;
    [SerializeField] GameObject spawnPattern;
    [SerializeField] SpawnEnv spawnEnv;
    [SerializeField] float terrainSpeed = 80f;
    [SerializeField] int cantOfEnemiesPerLevel = 5;
    [SerializeField] AudioSource deadShot;
    [SerializeField] AudioSource enemyShot;
    [SerializeField] BulletMovement playerMov;
    [SerializeField] ParticleSystem blood;
    private int points = 0;
    int level = 1;
    private int cantOfEnemies;
    private int cantEnemies=0;
    private SpawnPattern spawn;

    public int Level{
        get{return level;}
    }

    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public void Death() {
        deadShot.Play();
        playerMov.enabled = false;
        terrainSpeed = 0;
        Handheld.Vibrate();
        Invoke("terminate", 0.5f);
    }

    private void terminate(){
        SceneManager.LoadScene(0);
    }

    public void EnemyDeath(int value){
        enemyShot.Play();
        blood.Play();
        points += value * level;
        MenuManager.Instance.UpdatePoints(points);
        //EnemiesControl();
    }

   /* private void EnemiesControl(){
        if(cantOfEnemies >= cantOfEnemiesPerLevel){
            level++;
            if(level % 2 == 0)
                LvlPar();
            else 
                LvlImpar();
            if(level == spawn.LvlToNormal)
                spawn.ReCharguePatterns();
            if(level == spawn.LvlToHard)
                spawn.ReCharguePatterns();
            cantEnemies ++;
            cantOfEnemiesPerLevel += cantEnemies;
            cantOfEnemies = 0;
        }
    }*/

    public void PassLvl(){
        level++;
        spawn.TimePerObs();
        //spawn.RandomizePattern();
    }

   /* private void LvlPar(){
        spawn.TimePerObs();  
    }

    private void LvlImpar(){
        spawn.TimePerObs();
        spawn.RandomizePattern();
    }*/

    private void Awake(){
		cantOfEnemies = 0;
		spawn = spawnPattern.GetComponent<SpawnPattern>();
		MenuManager.Instance.UpdatePoints(points);
		#if UNITY_ANDROID
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.orientation = ScreenOrientation.Portrait;
		#endif
    }
}
