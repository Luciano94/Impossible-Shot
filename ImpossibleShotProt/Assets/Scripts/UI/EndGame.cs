using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public void EndTheGame(){
		if(GameManager.Instance.Score > ScoreFileManager.LoadScore()){
			ScoreFileManager.SaveScore();
		}
		SceneManager.LoadScene(0);
	}
}
