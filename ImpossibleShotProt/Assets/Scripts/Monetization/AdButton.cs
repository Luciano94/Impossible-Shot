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

	[SerializeField] string placementId = "rewardedVideo";
    [SerializeField] Button SourceButton;
    [SerializeField] GameObject Panel;
    private Button adButton;

    public void Start(){
		adButton = GetComponent<Button>();
		SourceButton.interactable = true;
		if (adButton) {
            adButton.onClick.AddListener (ShowAd);
        }
	}

	private void Update(){
		if(shouldReset && Monetization.IsReady (placementId)){
			SourceButton.interactable = true;
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
		SourceButton.interactable = false;
        Panel.SetActive(false);
		GameManager.Instance.Revive();
	}
}
