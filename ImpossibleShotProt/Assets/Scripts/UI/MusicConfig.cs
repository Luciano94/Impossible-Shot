using UnityEngine;
using UnityEngine.UI;

public class MusicConfig : MonoBehaviour {

	[SerializeField] Sprite musicOn;
	[SerializeField] Sprite musicOff;
	private Button musicBtn;
	public bool musicOnOff ;

	private void Start() {
		musicOnOff = true;
		musicBtn = GetComponent<Button>();
	}

	public void MusicOnOff(){
		if(musicOnOff){
			musicOnOff = false;
			//wwise mute
			musicBtn.image.sprite = musicOff;
		}else{
			musicOnOff = true;
			//wwise sound
			musicBtn.image.sprite = musicOn;
		}
	}
}
