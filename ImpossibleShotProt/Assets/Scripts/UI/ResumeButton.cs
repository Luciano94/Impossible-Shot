using UnityEngine;

public class ResumeButton : MonoBehaviour {
	public void Resume(){
		SoundManager.Instance.MenuTouch();
		MenuManager.Instance.Resume();
	}
}
