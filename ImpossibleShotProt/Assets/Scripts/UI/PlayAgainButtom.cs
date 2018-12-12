using UnityEngine;

public class PlayAgainButtom : MonoBehaviour {

	public void PlayAgain(){
		if(GameManager.Instance.Score > ScoreFileManager.LoadScore()){
			ScoreFileManager.SaveScore();
		}
		FirstPlay.Instance.RestartGame();
	}
}
