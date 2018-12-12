using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AdCountDown : MonoBehaviour {

	public int TimeToWait;
	public GameObject CurrentPanel;
	public GameObject NextPanel;
	private float timer = 0;
	private float timeLeft;
	private Text text;

	void Start(){
		timeLeft = TimeToWait;
		text = GetComponent<Text>();
	}

	void OnEnable(){
		timeLeft = TimeToWait;
	}
	
	void Update () {
		timer += Time.unscaledDeltaTime;
		if(timer >= 1){ 
			timer = 0;
			timeLeft -= 1;
			text.text = timeLeft.ToString();
		}
		if(timeLeft < 0){
			NextPanel.SetActive(true);
			CurrentPanel.SetActive(false);
		}
	}
}
