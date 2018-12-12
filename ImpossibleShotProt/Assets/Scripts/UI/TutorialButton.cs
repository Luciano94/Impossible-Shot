using UnityEngine;

public class TutorialButton : MonoBehaviour{

	public void PlayTutorial(){
                SoundManager.Instance.MenuTouch();
                TutorialManager.Instance.TutorialSelected();
                PlayerPrefs.SetInt("Tutorial", 0);
                PlayerPrefs.Save();
	}
}
