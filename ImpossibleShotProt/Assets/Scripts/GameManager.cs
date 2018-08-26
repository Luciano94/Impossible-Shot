using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

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

	[SerializeField] private float terrainSpeed = 20;
	[SerializeField] private float speedPerLevel = 10;
	[SerializeField] private float timePerLevel = 30;
	private float timeCurrentLevel;

	public float TerrainSpeed
	{
		get
		{
			return terrainSpeed;
		}
	}

	private void Awake()
	{
		timeCurrentLevel = 0;
	}

	private void Update()
	{
		timeCurrentLevel += Time.deltaTime;
		if(timeCurrentLevel >= timePerLevel)
		{
			timeCurrentLevel = 0;
			terrainSpeed += speedPerLevel;
		}
	}
}
