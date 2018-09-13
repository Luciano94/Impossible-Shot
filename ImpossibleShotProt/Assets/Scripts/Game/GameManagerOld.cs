using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerOld : MonoBehaviour {



	[SerializeField] private float terrainSpeed = 50;		//Velocidad del terreno y enemigos
	[SerializeField] private float timePerLevel = 30;		//duracion de cada nivel
	[SerializeField] private float timePerEnemy = 5;		//tiempo entre cada enemigo en los spawners
	[SerializeField] private float enemyTimePerLevel = 1;   //tiempo que se resta en el spawner en cada nivel
	[SerializeField] private float speedPerEnemy = 0.5f;	//aumento de velocidad por enemigo eliminado					
	[SerializeField] private float fieldOfView;
    [SerializeField] private float fieldPerLevel;
	[SerializeField] private float MaxFOV;
	[SerializeField] private float SecondsToBegin = 3;		//tiempo entre que comienza el que se dispara la bala y comienza el nivel
	[SerializeField] private Spawner spawnerA;
	[SerializeField] private Spawner spawnerB;
    private float timeCurrentLevel;                         //tiempo que transcurrio en el nivel actual
    private float NextFOV;
    private bool acceleration = false;
    private float targetSpeed;
    private float baseFOV;
    private float baseSpeed;
	private bool shouldWait;								//si terminṕ el tiempo de espera para el nivel

	public float TimePerEnemy
	{
		get
		{
			return timePerEnemy;
		}
	}
	
	public float FOV{
		get{
			return fieldOfView;
		}
	}
	
	public float TerrainSpeed
	{
		get
		{
			return terrainSpeed;
		}
	}

	public void Aceleration()
	{
        targetSpeed = terrainSpeed + speedPerEnemy;
        NextFOV = Camera.main.fieldOfView + fieldPerLevel;
		Mathf.Clamp(NextFOV,baseFOV,MaxFOV);
		acceleration = true;
	}

	public void Death()
	{
        SceneManager.LoadScene(0);
	}

	private void Awake()
	{
		timeCurrentLevel = 0;
		baseFOV = Camera.main.fieldOfView;
        baseSpeed = 10f;
		acceleration = false;
		shouldWait = true;
	}

    private void LateUpdate() {
		if (shouldWait) {
			Wait();
		} else {
			LevelControl ();
			AcceleracionControl ();
		}
		if(terrainSpeed < baseSpeed)
			SceneManager.LoadScene(0);
    }

	private void LevelControl(){
		timeCurrentLevel += Time.deltaTime;
        if(timeCurrentLevel >= timePerLevel) {
            timeCurrentLevel = 0;
            timePerEnemy -= enemyTimePerLevel;
            if(timePerEnemy < 1)
                timePerEnemy = 0.5f;
        }
	}

	private void AcceleracionControl(){
		if(acceleration) {
            terrainSpeed = Mathf.Lerp(terrainSpeed, targetSpeed+0.5f, 0.9f * Time.deltaTime);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, NextFOV, 0.9f * Time.deltaTime);
            if(terrainSpeed >= targetSpeed)
                acceleration = false;
        }
        else {
            terrainSpeed = Mathf.Lerp(terrainSpeed, baseSpeed-5, 0.2f * Time.deltaTime);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV, 0.9f * Time.deltaTime);
        }
	}

	private void Wait(){
		SecondsToBegin -= Time.deltaTime;
		if(SecondsToBegin <= 0){
			shouldWait = false;
			spawnerA.Go ();
			spawnerB.Go ();
			Debug.Log ("Go!");
		}
	}
}

