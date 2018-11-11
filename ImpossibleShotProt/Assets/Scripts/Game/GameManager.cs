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

    private float timeScale;

    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public float Multiplicador{
        get{return multiplicador;}
        set{multiplicador = value;}
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
        Handheld.Vibrate();
        HSmanager.UpdateHS();
        terminate();
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;
    }

    public void Revive(){
        Time.timeScale = timeScale;
        MenuManager.Instance.ContinueGame();
    }

    private void terminate(){
        MenuManager.Instance.FinishGame();
    }

    public void RestartGame(){
        Invoke("StartGame",0.5f);
        SceneManager.LoadScene(0);
    }

    public void EnemyDeath(int value){
        enemyShot.Play();
        if(!blood.isPlaying)
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
            PowerUpManager.Instance.UpdateKillingSpree();
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
