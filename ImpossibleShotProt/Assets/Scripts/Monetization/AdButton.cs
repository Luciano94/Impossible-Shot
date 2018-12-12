using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent (typeof (Button))]
public class AdButton : MonoBehaviour {

	private static bool hasWatchedContinueAd = false;

	public static bool AdAvailable(){
		return !hasWatchedContinueAd;
	}

	[SerializeField] string placementId = "rewardedVideo";
    [SerializeField] GameObject Panel;
    private Button adButton;

    public void Start(){
		adButton = GetComponent<Button>();
		if (adButton) {
            adButton.onClick.AddListener (ShowAd);
        }
	}

	private void ShowAd () {
        SoundManager.Instance.MenuTouch();
        ShowAdCallbacks options = new ShowAdCallbacks ();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent (placementId) as ShowAdPlacementContent;
        ad.Show (options);
    }

    private void HandleShowResult (ShowResult result) {
        if (result == ShowResult.Finished) {
            AdWatched();
        } else if (result == ShowResult.Failed) {
            AdWatched();
        }
    }

	private void AdWatched(){
		hasWatchedContinueAd = true;
        Panel.SetActive(false);
		GameManager.Instance.Revive();
	}

	public void Reset(){
		if( Monetization.IsReady (placementId)){
			hasWatchedContinueAd = false;
		}
	}
}
