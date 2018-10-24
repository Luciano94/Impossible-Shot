using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtom : MonoBehaviour {

	public void PlayTutorial(){
		GameManager.Instance.PlayTutorial();
		MenuManager.Instance.StartGame();
	}
}
