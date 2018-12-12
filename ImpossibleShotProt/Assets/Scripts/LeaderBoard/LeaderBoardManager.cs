using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour {

	private static LeaderBoardManager instance;

	public static LeaderBoardManager Instance{
		get{
			instance = FindObjectOfType<LeaderBoardManager>();
			if(instance == null){
				GameObject go = new GameObject("LeaderBoardManager");
				instance = go.AddComponent<LeaderBoardManager>();
			}
			return instance;
		}
	}
	
	[SerializeField] private Text[] leaderBoard;

	private void Awake() {
		foreach (Text txt in leaderBoard){
			txt.text = "0";
		}
	}
}
