using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	public void toMenu(){
		SoundManager.Instance.MenuTouch();
		SceneManager.LoadScene(0);
	}
}
