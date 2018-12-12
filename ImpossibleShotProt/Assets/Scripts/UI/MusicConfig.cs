using UnityEngine;
using UnityEngine.UI;

public class MusicConfig : MonoBehaviour {

	[SerializeField]private Sprite musicOn;
	[SerializeField]private Sprite musicOff;
	private Button musicBtn;

	private void Start() {
		musicBtn = GetComponent<Button>();
		UpdateSprite();
	}
	
	private void UpdateSprite(){
		bool isMusicOn = SoundManager.Instance.IsSoundOn();
		if(isMusicOn){
			musicBtn.image.sprite = musicOn;
		}else{
			musicBtn.image.sprite = musicOff;
		}
	}

	public void MusicOnOff(){
		SoundManager.Instance.MuteButtonClicked();
		UpdateSprite();
	}
}
