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
	[SerializeField] private GameObject tutorialPanel;
	[SerializeField] private GameObject mainScreen;
	[SerializeField] private GameObject inGame;
	[SerializeField] private GameObject pause;
	[SerializeField] private GameObject finish;
	[SerializeField] private GameObject adPanel;
	[SerializeField] private GameObject ingamePanel;
	[SerializeField] private GameObject exitPanel;
	[SerializeField] private Text finishPoints;
	[SerializeField] private Text pointsTxt;
	[SerializeField] private PointGainDisplay pointDisplay;
	[SerializeField] private AudioSource startSound; 
	[SerializeField] private BulletMovement playerMov;
	[SerializeField] private CameraMovement cameraMovement;
	[SerializeField] private GunCanonScript gun;
	[SerializeField] private ShotAnimation shotAnimation;
    private float timeScaleActual;

	[Header ("HighScore Text")]/*HighScore Menu */
	[SerializeField] private GameObject HSPanel;
	
	[Header ("Tutorial Text")]/*Toturial Text*/
	[SerializeField] private Text dPadTxt;
	[SerializeField] private Text enemyTxt;
    [SerializeField] private int countdown = 3;
    [SerializeField] private float timePerNumber = 0.5f;

	[Header ("Event Text")]/*Event Txt */
	[SerializeField] private Text eventBulletTxt;
	[SerializeField] private Text eventEnemyTxt;

	[Header("Login Google Play")]
	[SerializeField] private Text loginTxt;


	private void Awake() {
		principal.SetActive(true);
		SoundManager.Instance.Menu();
		if(PlayerPrefs.GetInt("Tutorial") == 1){
			tutorialPanel.SetActive(true);
			mainScreen.SetActive(false);
		}else{
			tutorialPanel.SetActive(false);
			mainScreen.SetActive(true);
		}
		inGame.SetActive(false);
		pause.SetActive(false);
		finish.SetActive(false);
		adPanel.SetActive(false);
		Time.timeScale = 0f;
		cameraMovement.enabled = false;
	} 

	public void PauseGame(){
		inGame.SetActive(false);
		finish.SetActive(false);
		pause.SetActive(true);
		GameManager.Instance.Pause();
	}

	public void ExitGame(){
		principal.SetActive(false);
		exitPanel.SetActive(true);
	}

	public void Resume(){
		pause.SetActive(false);
		inGame.SetActive(true);
		finish.SetActive(false);
		if(GameManager.Instance.TutorialMode){
			GameManager.Instance.DePause(); 
		}else{
			PauseReturn();
		}
	}

	public void ToHighScorePanel(){
		HSPanel.SetActive(true);
	}

	public void StartGame(){
		playerMov.enabled = false;
		principal.SetActive(false);
		finish.SetActive(false);
		inGame.SetActive(true);
		shotAnimation.PlayAnimation();
		InitGame();
	}

	private void InitGame(){
		Time.timeScale = 1.0f;
		adPanel.GetComponentInChildren<AdButton>().Reset();
		adPanel.SetActive(false);
		cameraMovement.enabled = true;
		gun.GameStart();
		GameManager.Instance.StartGame();
	}

	public void UpdatePoints(float TotalPoints, float newPoints, float mult){
		finishPoints.text = TotalPoints.ToString();
		pointsTxt.text = TotalPoints.ToString();
		pointDisplay.AddScore (newPoints);
	}

	public void FinishGame(){
		ingamePanel.SetActive(false);
		if(AdButton.AdAvailable()){
			adPanel.SetActive(true);
		} else{
			finish.SetActive(true);
		}
	}

	public void ContinueGame(){
		ingamePanel.SetActive(true);
		finish.SetActive(false);
		PauseReturn();
	}

	/*Event Methods */
	private void HideBulletEvent(){
		eventBulletTxt.text = "";
		eventBulletTxt.enabled = false;
	}

	public void HideEnemyEvent(){
		eventEnemyTxt.text = "";
		eventEnemyTxt.enabled = false;
	}

	public void ShowEnemyEvent(){
		eventEnemyTxt.enabled = true;
		if (countdown > 0){
            eventEnemyTxt.text = countdown.ToString();
            countdown--;
            Invoke("ShowEnemyEvent", timePerNumber);
        } else{
            eventEnemyTxt.text = "TIME TO KILL!!!";
		    countdown = 3;
            Invoke("HideEnemyEvent", timePerNumber);
        }
	}

	public void ShowBulletEvent(){
		eventBulletTxt.enabled = true;
		if (countdown > 0){
            eventBulletTxt.text = countdown.ToString();
            countdown--;
            Invoke("ShowBulletEvent", timePerNumber);
        } else{
            eventBulletTxt.text = "BULLET TIME!!!";
			countdown = 3;
            Invoke("HideBulletEvent", timePerNumber);
        }
	}

	public void PauseReturn(){
		eventBulletTxt.enabled = true;
		if (countdown > 0){
            eventBulletTxt.text = countdown.ToString();
            countdown--;
            Invoke("PauseReturn", timePerNumber);
        } else{
            eventBulletTxt.text = "START";
			countdown = 3;
			GameManager.Instance.DePause();
            Invoke("HideBulletEvent", timePerNumber);
        }
	}
	
	/*end events methods */

    public void FinalCountdown(){
        if (countdown > 0){
            enemyTxt.text = countdown.ToString();
            countdown--;
            Invoke("FinalCountdown", timePerNumber);
        } else{
			countdown = 3;
            enemyTxt.text = "START";
            Invoke("HideStart", timePerNumber);
        }
    }

    private void HideStart() {
        enemyTxt.text = " ";
    }

	private void Update() {
		if(Input.GetButton("Submit") && (inGame.activeSelf)){
			PauseGame();
		}
	}

	public void ShowLogin(){
		loginTxt.text = "Login Success";
		Invoke("HideShowLogin", 0.5f);
	}

	private void HideShowLogin(){
		loginTxt.text = "";
	}

	public void ShowDontLogin(){
		loginTxt.text = "Login  dont Success.";
		Invoke("HideDontLogin", 0.5f);
	}

	public void HideDontLogin(){
		loginTxt.text = "";
	}
}
