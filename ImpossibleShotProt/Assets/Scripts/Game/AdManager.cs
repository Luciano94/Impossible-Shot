using UnityEngine;
using UnityEngine.Monetization;

public class AdManager : MonoBehaviour {

	private static AdManager instance = null;

    public static AdManager Instance {
        get {
            instance = FindObjectOfType<AdManager>();
            if(instance == null) {
                GameObject go = new GameObject("Ad Manager");
                instance = go.AddComponent<AdManager>();
            }
            return instance;
        }
    }

#if UNITY_IOS
   private string gameId = "2887145";
#elif UNITY_ANDROID
    private string gameId = "2881746";
#endif

    bool testMode = true;

    public string placementId = "rewardedVideo";

	private void Awake(){
		DontDestroyOnLoad(gameObject);
        Debug.LogWarning("This class is currently useless, and will most likely cause erros with ad placements.");
	}
	private void Start () {
		Monetization.Initialize (gameId, testMode);
	}
	

    public bool AdReady(){
        return Monetization.IsReady (placementId);
    }
   
}
