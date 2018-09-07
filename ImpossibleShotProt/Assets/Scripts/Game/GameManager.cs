using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			instance = FindObjectOfType<GameManager>();
			if(instance == null)
			{
				GameObject go = new GameObject("GameManager");
				instance = go.AddComponent<GameManager>();
			}
			return instance;
		}
	}

	[SerializeField] private float terrainSpeed = 50;		//Velocidad del terreno y enemigos
	[SerializeField] private float timePerLevel = 30;		//duracion de cada nivel
	[SerializeField] private float timePerEnemy = 5;		//tiempo entre cada enemigo en los spawners
	[SerializeField] private float enemyTimePerLevel = 1;   //tiempo que se resta en el spawner en cada nivel
	[SerializeField] private float speedPerEnemy = 0.5f;	//aumento de velocidad por enemigo eliminado					
	[SerializeField] private float fieldOfView;
    [SerializeField] private float fieldPerLevel;
    private float timeCurrentLevel;                         //tiempo que transcurrio en el nivel actual
    private float NextFOV;
    private bool acceleration = false;
    private float targetSpeed;
    private float baseFOV;
    private float baseSpeed;

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
        baseSpeed = 0f;
		acceleration = false;
	}

    private void LateUpdate() {
		LevelControl();
		AcceleracionControl();
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
            terrainSpeed = Mathf.Lerp(terrainSpeed, baseSpeed, 0.2f * Time.deltaTime);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV, 0.9f * Time.deltaTime);
        }
	}
}

