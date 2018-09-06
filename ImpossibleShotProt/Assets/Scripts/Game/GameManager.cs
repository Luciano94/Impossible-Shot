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

	[SerializeField] private float terrainSpeed = 20;		//Velocidad del terreno y enemigos
	[SerializeField] private float speedPerLevel = 10;		//cantidad de aceleracion por nivel
	[SerializeField] private float timePerLevel = 30;		//duracion de cada nivel
	[SerializeField] private float timePerEnemy = 5;		//tiempo entre cada enemigo en los spawners
	[SerializeField] private float enemyTimePerLevel = 1;   //tiempo que se resta en el spawner en cada nivel
	[SerializeField] private float speedPerEnemy = 0.5f;	//aumento de velocidad por enemigo eliminado
	private float timeCurrentLevel;							//tiempo que transcurrio en el nivel actual
	[SerializeField] private float fieldOfView;
    [SerializeField] private float fieldPerLevel;
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
		terrainSpeed += speedPerEnemy;
	}

	public void Death()
	{
        SceneManager.LoadScene(0);
	}

	private void Awake()
	{
		timeCurrentLevel = 0;
		fieldOfView = Camera.main.fieldOfView;
	}

	private void Update()
	{
		timeCurrentLevel += Time.deltaTime;
		if(timeCurrentLevel >= timePerLevel)
		{
			timeCurrentLevel = 0;
			terrainSpeed += speedPerLevel;
			if (timePerEnemy > 1)
				timePerEnemy -= enemyTimePerLevel;
			else timePerEnemy = 0.5f;
			//fieldOfView = Mathf.Lerp(fieldOfView, fieldOfView + fieldPerLevel, 0.01f);
		}
	}
}
