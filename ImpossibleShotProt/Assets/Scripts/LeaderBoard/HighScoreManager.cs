using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {
	
	[SerializeField]private Text ScorePoints;

	public void getScorefromFile(int score){
		ScorePoints.text = score.ToString();
	}

	public void UpdateHS(){
		ScorePoints.text = GameManager.Instance.Score.ToString();
	}
}
