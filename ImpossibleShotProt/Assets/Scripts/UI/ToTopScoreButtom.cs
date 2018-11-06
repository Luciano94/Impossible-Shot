using UnityEngine;

public class ToTopScoreButtom : MonoBehaviour {
	
	public void TopScoreButtom(){
		GameManager.Instance.highScoreManager.getScorefromFile(ScoreFileManager.LoadScore());
	}
}
