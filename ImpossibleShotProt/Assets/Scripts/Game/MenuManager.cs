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
	[SerializeField] private Text pointsTxt;
	[SerializeField] private PointGainDisplay pointDisplay;
	//[SerializeField] private Text levelTxt;
	//[SerializeField] private Text eneTxt;
	[SerializeField] private AudioSource startSound; 
	[SerializeField] private BulletMovement playerMov;

	private void Awake() {
		principal.SetActive(true);
		playerMov.enabled = false;
		inGame.SetActive(false);
		pause.SetActive(false);
		Time.timeScale = 0f;

	} 

	public void PauseGame(){
		playerMov.enabled = false;
		inGame.SetActive(false);
		pause.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		pause.SetActive(false);
		inGame.SetActive(true);
		playerMov.enabled = true;
		Time.timeScale = 1f;
	}

	public void StartGame(){
		Time.timeScale = 1f;
		playerMov.enabled = true;
		startSound.Play();
		principal.SetActive(false);
		inGame.SetActive(true);
	}

	public void UpdatePoints(float TotalPoints, int newPoints, float mult){
		pointsTxt.text = TotalPoints.ToString() + " x" + mult.ToString();
		pointDisplay.AddScore (newPoints);
	}

	/*public void UpdateLvl(int value){
		levelTxt.text = "Level: " + value.ToString();
	}*/

	/*public void UpdateEnemies(int actEne, int totalEne){
		eneTxt.text = actEne.ToString() + " / " + totalEne.ToString();
	}*/

	private void Update() {
		if(Input.GetButton("Submit") && (inGame.activeSelf)){
			PauseGame();
		}
	}
}
