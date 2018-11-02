using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdButton : MonoBehaviour {

	
	private static bool hasWatchedContinueAd = false;
	private static bool shouldReset = false;
	public static void ResetContinueAd(){
		shouldReset = true;
	}
	public static bool AdAvailable(){
		return !hasWatchedContinueAd;
	}

	UnityEngine.UI.Button button;

	public void Awake(){
		button = GetComponent<UnityEngine.UI.Button>();
		button.gameObject.SetActive(true);
	}

	private void Update(){
		if(shouldReset){
			button.gameObject.SetActive(true);
			hasWatchedContinueAd = false;
			shouldReset = false;
		}
	}

	public void WatchContinueAd(){
		hasWatchedContinueAd = true;
		button.gameObject.SetActive(false);
		Debug.Log("Ad watched");
		//poner ad
		GameManager.Instance.Revive();
	}
}
