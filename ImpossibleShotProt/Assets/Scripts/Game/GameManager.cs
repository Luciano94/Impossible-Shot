using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

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

    [SerializeField] float multPerKillingSpree;
    [SerializeField] GameObject spawnPattern;
    [SerializeField] SpawnEnv spawnEnv;
    [SerializeField] float terrainSpeed = 80f;
    [SerializeField] AudioSource deadShot;
    [SerializeField] AudioSource enemyShot;
    [SerializeField] BulletMovement playerMov;
    [SerializeField] BulletSpin playerSpin;
    [SerializeField] ParticleSystem blood;
    [SerializeField] ParticleSystem trail;
    [SerializeField] int enemiesForKillingSpree = 5;
    private int actKillingSpree;
    private bool tutorialMode = false;
    private float multiplicador;
    private int points = 0;
    private PatternSpawner spawn;

    [SerializeField]private HighScoreManager HSmanager;

    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public void PlayTutorial(){
        tutorialMode = true;
    }
    
    public int Score{
        get{return points;}
    } 
    public bool TutorialMode{
        get{return tutorialMode;}
    }

    public HighScoreManager highScoreManager{
        get{return HSmanager;}
    }
    public void EndTutorial(){
        tutorialMode = false;
        actKillingSpree = 0;
        multiplicador = 1;
        points = 0;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
    }
    public void Death() {
        deadShot.Play();
        playerMov.enabled = false;
        playerSpin.enabled = false;
        Handheld.Vibrate();
        HSmanager.UpdateHS();
        terminate();
        Time.timeScale = 0.0f;
    }

    public void Revive(){
        Time.timeScale = 1.0f;
        MenuManager.Instance.ContinueGame();
    }

    private void terminate(){
        MenuManager.Instance.FinishGame();
    }

    public void EnemyDeath(int value){
        enemyShot.Play();
        blood.Play();
        spawn.UpdateStage();
        killingSpree();
        int AddedScore = (int)(value * multiplicador);
        points += AddedScore;
		MenuManager.Instance.UpdatePoints(points, AddedScore, multiplicador);
    }

    private void killingSpree(){
        actKillingSpree++;
        if(actKillingSpree == enemiesForKillingSpree){
            actKillingSpree = 0;
            multiplicador += multPerKillingSpree;
            multiplicador = (float)Math.Round(multiplicador, 2);
        }
    }

    private void Awake(){
        if (!PlayerPrefs.HasKey("Tutorial"))
            PlayerPrefs.SetInt("Tutorial", 1);
        
		spawn = spawnPattern.GetComponent<PatternSpawner>();
        multiplicador = 1;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);

    }

    public void StartGame(){
        FirstPlay.Instance.play();
        if(!tutorialMode){
            spawn.Begin();
        }
    }

    public void TutorialSpawnBegin(){
        if(tutorialMode){
            spawn.Begin();
        }
    }

    public bool InTutorial(){
        return tutorialMode;
    }
}
