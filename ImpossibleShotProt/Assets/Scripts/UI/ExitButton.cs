using UnityEngine;

public class ExitButton : MonoBehaviour {

	public void Exit(){
		SoundManager.Instance.MenuTouch();
		#if UNITY_EDITOR
        	 UnityEditor.EditorApplication.isPlaying = false;
    	 #else
         	Application.Quit();
    	 #endif
	}
}
