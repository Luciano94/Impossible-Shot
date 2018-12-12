using UnityEngine;

public class TutorialPattern : MonoBehaviour {
	public void Return(){
		if(GameManager.Instance.TutorialMode){
			TutorialManager.Instance.UpdateObstaclesHit();
		}
	}
}