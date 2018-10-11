using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGainDisplay : MonoBehaviour {
	[SerializeField][Range(0.0f,2.0f)] float TimeToFade;
	private UnityEngine.UI.Text Txt;
	private float Timer;
	private int DisplayedScore;

	// Use this for initialization
	void Start () {
		Txt = GetComponent<UnityEngine.UI.Text> ();	
		Timer = TimeToFade;
		DisplayedScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		if (Timer >= TimeToFade) {
			Txt.text = ""; //fade out
			DisplayedScore = 0;
		}
	}

	public void AddScore(int score){
		if (score != 0){
			Timer = 0;
			DisplayedScore += score;
			Txt.text = "+" + DisplayedScore;
			//fade in
		}
	}
}
