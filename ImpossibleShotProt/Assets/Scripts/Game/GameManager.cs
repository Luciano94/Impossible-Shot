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

    [SerializeField] float multPerKillingSpree;
    [SerializeField] GameObject spawnPattern;
    [SerializeField] GameObject bulletHole;
    [SerializeField] float terrainSpeed = 80f;
    [SerializeField] AudioSource deadShot;
    [SerializeField] AudioSource enemyShot;
    [SerializeField] BulletMovement playerMov;
    [SerializeField] BulletSpin playerSpin;
    [SerializeField] ParticleSystem blood;
    [SerializeField] ParticleSystem trail;
    [SerializeField] ParticleSystem sliver;
    [SerializeField] int enemiesForKillingSpree = 5;
    [SerializeField] BulletSpin bulletSpin;
    private int actKillingSpree;
    private bool tutorialMode = false;
    private float multiplicador;
    private int points = 0;
    private PatternSpawner spawn;
    private bool isPlaying = false;
    private bool isDeath = false;

    [SerializeField]private HighScoreManager HSmanager;

    private float timeScale;

    public void TimeScale(){
        Time.timeScale = timeScale;
    }

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

    private void OnApplicationFocus(bool focusStatus) {
        if(isPlaying && Time.timeScale != 0 && !focusStatus)
            MenuManager.Instance.PauseGame();
    }

    public void EndTutorial(){
        tutorialMode = false;
        actKillingSpree = 0;
        multiplicador = 1;
        points = 0;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
    }

    public void Death() {
        //deadShot.Play();
        isDeath = true;
        bulletHole.SetActive(true);
        AkSoundEngine.PostEvent("Ingame_finish",gameObject);
        Handheld.Vibrate();
        HSmanager.UpdateHS();
        sliver.Play();
        timeScale = Time.timeScale;
        terrainSpeed = 0.0f;
        trail.Stop();
        bulletSpin.enabled = false;
        playerMov.enabled = false;
        spawn.PauseSpawn();
        Invoke("terminate", 1.0f);
    }

    public void Pause(){
        terrainSpeed =0.0f;
        bulletSpin.enabled = false;
        playerMov.enabled = false;
        trail.Stop();
        spawn.PauseSpawn();
    }

    public void DePause(){
        bulletSpin.enabled = true;
        playerMov.enabled = true;
        terrainSpeed = 80.0f;
        trail.Play();
        spawn.Begin();
    }
    public void Revive(){
        isDeath = false;
        AkSoundEngine.PostEvent("Ingame_start",gameObject);
        Time.timeScale = timeScale;
        bulletHole.SetActive(false);
       // playerMov.enabled = true;
       // terrainSpeed = 80.0f;
       // spawn.Begin();
        MenuManager.Instance.ContinueGame();
    }

    private void terminate(){
        Time.timeScale = 0.0f;
        trail.Play();
        bulletSpin.enabled = true;
        MenuManager.Instance.FinishGame();
    }

    public void RestartGame(){
        Invoke("StartGame",0.5f);
        SceneManager.LoadScene(0);
    }

    public void EnemyDeath(int value){
        //enemyShot.Play();
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
        PlayGamesPlatform.Activate();
        bulletHole.SetActive(false);
        isPlaying = false;
        if (!PlayerPrefs.HasKey("Tutorial"))
            PlayerPrefs.SetInt("Tutorial", 1);
        
		spawn = spawnPattern.GetComponent<PatternSpawner>();
        multiplicador = 1;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
    }
    private void Start(){
		AkSoundEngine.PostEvent("Menu",gameObject);
    }
    public void StartGame(){
        AkSoundEngine.PostEvent("Ingame_start",gameObject);
        FirstPlay.Instance.play();
        isPlaying = true;
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
