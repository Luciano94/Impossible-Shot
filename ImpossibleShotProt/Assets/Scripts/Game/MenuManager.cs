using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private static MenuManager instance;

	public static MenuManager Instance{
		get{
			instance = FindObjectOfType<MenuManager>();
			if(instance == null){
				GameObject go = new GameObject("MenuManager");
				instance = go.AddComponent<MenuManager>();
			}
			return instance;
		}
	}

	[SerializeField] private GameObject principal;
	[SerializeField] private GameObject inGame;
	[SerializeField] private GameObject pause;
	[SerializeField] private GameObject finish;
	[SerializeField] private Text finishPoints;
	[SerializeField] private Text pointsTxt;
	[SerializeField] private PointGainDisplay pointDisplay;
	[SerializeField] private AudioSource startSound; 
	[SerializeField] private BulletMovement playerMov;
	[SerializeField] private CameraMovement cameraMovement;

	private void Awake() {
		principal.SetActive(true);
		playerMov.enabled = false;
		inGame.SetActive(false);
		pause.SetActive(false);
		finish.SetActive(false);
		Time.timeScale = 0f;
		cameraMovement.enabled = false;
	} 

	public void PauseGame(){
		playerMov.enabled = false;
		inGame.SetActive(false);
		finish.SetActive(false);
		pause.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		pause.SetActive(false);
		inGame.SetActive(true);
		finish.SetActive(false);
		playerMov.enabled = true;
		Time.timeScale = 1f;
	}

	public void StartGame(){
		Time.timeScale = 1f;
		playerMov.enabled = true;
		startSound.Play();
		principal.SetActive(false);
		finish.SetActive(false);
		inGame.SetActive(true);
		playerMov.enabled = false;
		cameraMovement.enabled = true;
	}

	public void UpdatePoints(float TotalPoints, float newPoints, float mult){
		finishPoints.text = TotalPoints.ToString();
		pointsTxt.text = TotalPoints.ToString() + " x" + mult.ToString();
		pointDisplay.AddScore (newPoints);
	}

	public void FinishGame(){
		inGame.SetActive(false);
		finish.SetActive(true);
	}

	private void Update() {
		if(Input.GetButton("Submit") && (inGame.activeSelf)){
			PauseGame();
		}
	}
}
