using UnityEngine;
using UnityEngine.UI;

public class MusicConfig : MonoBehaviour {

	[SerializeField] Sprite musicOn;
	[SerializeField] Sprite musicOff;
	private Button musicBtn;
	private bool isMusicOn ;

	private void Start() {
		isMusicOn = true; //tomarlo de playerprefs
		SoundManager.Instance.MuteButtonClicked(isMusicOn);
		musicBtn = GetComponent<Button>();
	}

	public void MusicOnOff(){
		if(isMusicOn){
			isMusicOn = false;
			SoundManager.Instance.MuteButtonClicked(isMusicOn);
			musicBtn.image.sprite = musicOff;
		}else{
			isMusicOn = true;
			SoundManager.Instance.MuteButtonClicked(isMusicOn);
			musicBtn.image.sprite = musicOn;
		}
	}

	private void OnDestroy(){
		//guardar isMusicOn en playerprefs
	}
}
