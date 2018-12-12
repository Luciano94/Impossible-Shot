using UnityEngine;

public class PauseGame : MonoBehaviour {

	public void Pause(){
		MenuManager.Instance.PauseGame();
	}
}
