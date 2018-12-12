using UnityEngine;

public class StartButton : MonoBehaviour {
	public void Play(){
		SoundManager.Instance.MenuTouch();
		MenuManager.Instance.StartGame();
	}
}
