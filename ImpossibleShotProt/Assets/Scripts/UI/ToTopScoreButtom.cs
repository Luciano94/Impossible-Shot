using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ToTopScoreButtom : MonoBehaviour {
	
	public void TopScoreButtom(){
		GameManager.Instance.highScoreManager.getScorefromFile(ScoreFileManager.LoadScore());

#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
#endif
    }

	public void LoginGoogle(){
		Social.localUser.Authenticate((bool success) => {
        	if(success){
				MenuManager.Instance.ShowLogin();
				Debug.Log("Sirve");
			}else{
				MenuManager.Instance.ShowDontLogin();
				Debug.Log("no Sirve");				
			}// handle success or failure
         });
	}
}
