using UnityEngine;

public class SoundManager : MonoBehaviour {

	static private SoundManager instance = null;
	static public SoundManager Instance{
		get{
			instance = FindObjectOfType<SoundManager>();
			if(instance == null){
				GameObject go = new GameObject("SoundManager");
				instance = go.AddComponent<SoundManager>();
			}
			return instance;
		}
	}
	
	private bool isSoundOn;

	void Start(){
		isSoundOn = true;
		if(PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetInt("Sound") == 0){
            isSoundOn = false;
        }
		UpdateSoundStatus();
	}

	void OnDestroy(){
        if (isSoundOn){
            PlayerPrefs.SetInt("Sound", 1);
        }else{
            PlayerPrefs.SetInt("Sound", 0);
        }
	}

	public bool IsSoundOn(){
		return isSoundOn;
	}

	private void UpdateSoundStatus(){
		if(isSoundOn){
			AkSoundEngine.PostEvent("mute_off", gameObject);
		} else{
			AkSoundEngine.PostEvent("mute_on", gameObject);
		}
	}
	public void MuteButtonClicked(){
		isSoundOn = !isSoundOn;
		UpdateSoundStatus();
	}

	public void GameStart(bool isTutorial){
		StopSound();
		if(isTutorial){
			Tutorial1();
		} else {
		AkSoundEngine.PostEvent("Ingame_start", gameObject);}
	}
	public void GameFinish(){
		AkSoundEngine.PostEvent("Ingame_finish", gameObject);
	}

	public void PlayAgain(){
		AkSoundEngine.PostEvent("Play_Again", gameObject);
	}

	public void Menu(){
		AkSoundEngine.StopAll();
		AkSoundEngine.PostEvent("Menu", gameObject);
	}
	public void MenuTouch(){
		AkSoundEngine.PostEvent("Menu_touch", gameObject);
	}
	public void BackToMenu(){
		AkSoundEngine.PostEvent("Vuelta_menu", gameObject);
	}
	public void MetalImpact(){
		AkSoundEngine.PostEvent("Metal_impact", gameObject);
	}
	public void WoodImpact(){
		AkSoundEngine.PostEvent("Wood_impact", gameObject);
	}
	public void EnemyImpact(){
		AkSoundEngine.PostEvent("Target_impact", gameObject);
	}

	public void EnemyScream(){
		AkSoundEngine.PostEvent("Surprise", gameObject);
	}

	public void PowerUp(){
		AkSoundEngine.PostEvent("Special_bullet", gameObject);
	}
	public void EndPowerUp(){
		AkSoundEngine.PostEvent("special_bullet_finish",gameObject);
	}
	private void Tutorial1(){
		AkSoundEngine.PostEvent("Tutorial", gameObject);
	}
	public void Tutorial2(){
		AkSoundEngine.PostEvent("Tutorial2", gameObject);
	}
	public void Tutorial3(){
		AkSoundEngine.PostEvent("Tutorial3", gameObject);
	}
	public void Tutorial4(){
		AkSoundEngine.PostEvent("Tutorial4", gameObject);
	}
	public void StopSound(){
		AkSoundEngine.StopAll();
	}
}
