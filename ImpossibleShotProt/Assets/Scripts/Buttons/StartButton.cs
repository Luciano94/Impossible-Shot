using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
	public void Play(){
		InputManager.Instance.Initialize (FindObjectOfType<InputSelect>().GetAlternative());
		MenuManager.Instance.StartGame();
	}
}
