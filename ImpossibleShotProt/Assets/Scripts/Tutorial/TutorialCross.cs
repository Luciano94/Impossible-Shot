using UnityEngine;

public class TutorialCross : MonoBehaviour {

	void Start(){
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
	}
	public void Show(){
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}
	public void Hide(float time){
		Invoke("Deactivate",time);
	}
	private void Deactivate(){
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
	}

	public bool GetActive(){
		return gameObject.transform.GetChild(0).gameObject.activeInHierarchy;
	}
}
