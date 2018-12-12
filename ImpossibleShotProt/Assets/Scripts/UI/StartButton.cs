using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
	public void Play(){
		SoundManager.Instance.MenuTouch();
		MenuManager.Instance.StartGame();
	}
}
