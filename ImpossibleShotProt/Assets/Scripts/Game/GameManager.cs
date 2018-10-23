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

    [SerializeField] float multPerEnemy;
    [SerializeField] GameObject spawnPattern;
    [SerializeField] SpawnEnv spawnEnv;
    [SerializeField] float terrainSpeed = 80f;
    [SerializeField] AudioSource deadShot;
    [SerializeField] AudioSource enemyShot;
    [SerializeField] BulletMovement playerMov;
    [SerializeField] ParticleSystem blood;
    private bool tutorialMode = false;
    private float multiplicador;
    private float points = 0;
    int level = 1;
    private PatternSpawner spawn;

    public int Level{
        get{return level;}
    }

    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public void PlayTutorial(){
        tutorialMode = true;
    }

    public bool TutorialMode{
        get{return tutorialMode;}
    }
    public void Death() {
        deadShot.Play();
        playerMov.enabled = false;
        terrainSpeed = 0;
        Handheld.Vibrate();
        Invoke("terminate", 0.5f);
    }

    private void terminate(){
        MenuManager.Instance.FinishGame();
    }

    public void EnemyDeath(int value){
        enemyShot.Play();
        blood.Play();
        multiplicador += multPerEnemy;
        multiplicador = (float)Math.Round(multiplicador, 2);
        var AddedScore = value * multiplicador;
        points += AddedScore;
		MenuManager.Instance.UpdatePoints(points, AddedScore, multiplicador);
    }

    private void Awake(){
		spawn = spawnPattern.GetComponent<PatternSpawner>();
        multiplicador = 1;
		MenuManager.Instance.UpdatePoints(points, 0, multiplicador);
		#if UNITY_ANDROID
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.orientation = ScreenOrientation.Portrait;
		#endif
    }

    public void StartGame(){
        FirstPlay.Instance.play();
        spawn.Begin();
    }
}
