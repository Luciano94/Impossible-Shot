using GooglePlayGames;
using UnityEngine.SocialPlatforms;
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

    [SerializeField]private float multPerKillingSpree;
    [SerializeField]private GameObject spawnPattern;
    [SerializeField]private GameObject bulletHole;
    [SerializeField]private float terrainSpeed = 80f;
    [SerializeField]private AudioSource deadShot;
    [SerializeField]private AudioSource enemyShot;
    [SerializeField]private BulletMovement playerMov;
    [SerializeField]private BulletSpin playerSpin;
    [SerializeField]private ParticleSystem blood;
    [SerializeField]private ParticleSystem trail;
    [SerializeField]private ParticleSystem sliver;
    [SerializeField]private int enemiesForKillingSpree = 5;
    [SerializeField]private BulletSpin bulletSpin;
    [SerializeField]private HighScoreManager HSmanager;
    private int actKillingSpree;
    private bool tutorialMode = false;
    private float multiplicador;
    private int points = 0;
    private PatternSpawner spawn;
    private bool isPlaying = false;
    private bool isDeath = false;
    private float timeScale;

    public bool IsPlaying{
        get{return isPlaying;}
    }

    public bool IsDeath{
        get{return isDeath;}
    }

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

    private void Awake(){
        PlayGamesPlatform.Activate();
        bulletHole.SetActive(false);
        isPlaying = false;
        if (!PlayerPrefs.HasKey("Tutorial")){
            PlayerPrefs.SetInt("Tutorial", 1);
        }
		spawn = spawnPattern.GetComponent<PatternSpawner>();
        multiplicador = 1;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
    }

    private void OnApplicationFocus(bool focusStatus) {
        if(isPlaying && Time.timeScale != 0 && !focusStatus){
            MenuManager.Instance.PauseGame();
        }
    }

    public void EndTutorial(){
        tutorialMode = false;
        actKillingSpree = 0;
        multiplicador = 1;
        points = 0;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
        SoundManager.Instance.GameStart(false);
    }

    public void Death() {
        bulletSpin.enabled = false;
        playerMov.enabled = false;
        isDeath = true;
        bulletHole.SetActive(true);
        SoundManager.Instance.GameFinish();
        Handheld.Vibrate();
        HSmanager.UpdateHS();
        sliver.Play();
        timeScale = Time.timeScale;
        terrainSpeed = 0.0f;
        trail.Stop();
        spawn.Death();
        Invoke("terminate", 1.0f);
    }

    public void Pause(){
        if(tutorialMode){
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }else{
            playerMov.enabled = false;
            spawn.PauseSpawn();
        }
            terrainSpeed =0.0f;
            bulletSpin.enabled = false;
            trail.Stop();
    }

    public void DePause(){
        bulletSpin.enabled = true;
        terrainSpeed = 80.0f;
        trail.Play();
        if(tutorialMode){
            Time.timeScale = timeScale;
        }else { 
            playerMov.enabled = true;
            spawn.Begin();
        }
    }

    public void Revive(){
        isDeath = false;
        SoundManager.Instance.GameStart(false);
        Time.timeScale = timeScale;
        bulletHole.SetActive(false);
        MenuManager.Instance.ContinueGame();
    }

    private void terminate(){
        Time.timeScale = 0.0f;
        trail.Play();
        bulletSpin.enabled = true;
        MenuManager.Instance.FinishGame();
    }

    public void EnemyDeath(int value){
        SoundManager.Instance.EnemyImpact();
        if(!blood.isPlaying){
            blood.Play();
        }
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

    public void StartGame(){
        SoundManager.Instance.GameStart(tutorialMode);
        FirstPlay.Instance.play();
        isPlaying = true;
        if(!tutorialMode){
            spawn.Begin();
        }
    }

    public bool InTutorial(){
        return tutorialMode;
    }
}
