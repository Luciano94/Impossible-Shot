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

    [SerializeField] GameObject spawnPattern;
    [SerializeField] float terrainSpeed = 50f;
    [SerializeField] float fOVPerLevel = 5f;
    [SerializeField] float maxFOV = 80f;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float speedPerLevel = 10f;
    [SerializeField] int cantOfEnemiesPerLevel = 5;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float maxBulletSpeed = 50f;
    [SerializeField] float bulletSpeedPerLevel = 5f;
    [SerializeField] int pointsperEne = 100;
    [SerializeField] int points = 0;
    int level = 1;
    private int cantOfEnemies;

    public float BulletSpeed{
        get{return bulletSpeed;}
    }
    public float TerrainSpeed {
        get { return terrainSpeed; }
    }

    public void Death() {
        SceneManager.LoadScene(0);
    }

    public void EnemyDeath(){
        cantOfEnemies ++;
        points += pointsperEne;
        MenuManager.Instance.UpdateEnemies(cantOfEnemies,cantOfEnemiesPerLevel);
        MenuManager.Instance.UpdatePoints(points);
        EnemiesControl();
    }

    private void EnemiesControl(){
        if(cantOfEnemies >= cantOfEnemiesPerLevel){
            SpeedControl();
            BulletControl();
            FovControl();
            spawnPattern.GetComponent<SpawnPattern>().RandomizePattern();
            cantOfEnemiesPerLevel ++;
            cantOfEnemies = 0;
            level++;
            MenuManager.Instance.UpdateEnemies(cantOfEnemies,cantOfEnemiesPerLevel);
            MenuManager.Instance.UpdateLvl(level);
        }
    }

    private void SpeedControl(){
        if(terrainSpeed + speedPerLevel <= maxSpeed) 
            terrainSpeed += speedPerLevel;
    }
    private void  FovControl(){
        if(Camera.main.fieldOfView + fOVPerLevel <= maxFOV){
            float nextFOV = Camera.main.fieldOfView + fOVPerLevel;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, nextFOV, 1f * Time.deltaTime);
        }
    }
    private void BulletControl(){
        if(bulletSpeed + bulletSpeedPerLevel <= maxBulletSpeed ) 
            bulletSpeed += bulletSpeedPerLevel;
    }

    private void Awake() {
        cantOfEnemies = 0;
        MenuManager.Instance.UpdatePoints(points);
        MenuManager.Instance.UpdateEnemies(cantOfEnemies,cantOfEnemiesPerLevel);
        MenuManager.Instance.UpdateLvl(level);
    }
}
