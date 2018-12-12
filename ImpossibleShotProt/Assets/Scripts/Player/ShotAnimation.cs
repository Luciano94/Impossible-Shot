using UnityEngine;

public class ShotAnimation : MonoBehaviour {

	[SerializeField] private GameObject firstFrame;
	[SerializeField] private GameObject secondFrame;

	private void Start() {
		firstFrame.SetActive(true);
		secondFrame.SetActive(false);
	}

	public void PlayAnimation(){
		firstFrame.SetActive(false);
		secondFrame.SetActive(true);
	}
}
