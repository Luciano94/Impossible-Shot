using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent (typeof (Button))]
public class AdButton : MonoBehaviour {

	
	private static bool hasWatchedContinueAd = false;
	private static bool shouldReset = false;

	public static void ResetContinueAd(){
		shouldReset = true;
	}
	public static bool AdAvailable(){
		return !hasWatchedContinueAd;
	}

	public string placementId = "rewardedVideo";
    private Button adButton;

#if UNITY_IOS
  	private string gameId = "2887145";
#elif UNITY_ANDROID
    private string gameId = "2887146";
#endif

	public void Start(){
		adButton = GetComponent<Button>();
		adButton.gameObject.SetActive(true);
		if (adButton) {
            adButton.onClick.AddListener (ShowAd);
        }

        if (Monetization.isSupported) {
            Monetization.Initialize (gameId, true);
        }
	}

	private void Update(){
		if(shouldReset && Monetization.IsReady (placementId)){
			adButton.gameObject.SetActive(true);
			hasWatchedContinueAd = false;
			shouldReset = false;
		}

	}

	private void ShowAd () {
        ShowAdCallbacks options = new ShowAdCallbacks ();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent (placementId) as ShowAdPlacementContent;
        ad.Show (options);
    }

    private void HandleShowResult (ShowResult result) {
        if (result == ShowResult.Finished) {
            AdWatched();
        } else if (result == ShowResult.Skipped) {
            Debug.LogWarning ("The player skipped the video - DO NOT REWARD!");
        } else if (result == ShowResult.Failed) {
            AdWatched();
        }
    }

	private void AdWatched(){
		hasWatchedContinueAd = true;
		adButton.gameObject.SetActive(false);
		GameManager.Instance.Revive();
	}
}
