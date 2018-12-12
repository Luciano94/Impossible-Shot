using UnityEngine;

public class BackButtom : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(GameManager.Instance.IsPlaying){
				if(!GameManager.Instance.IsDeath){
					MenuManager.Instance.PauseGame();
				}
			}else{
				MenuManager.Instance.ExitGame();
			}
		}
	}
}
