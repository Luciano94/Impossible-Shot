using UnityEngine;
using UnityEngine.Monetization;

public class MonetizationInitializer : MonoBehaviour {

#if UNITY_IOS
  	private string gameId = "2887139";
#elif UNITY_ANDROID
    private string gameId = "2887141";
#else
    private string gameId = "0000000";
#endif

    void Start () {
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }
}
