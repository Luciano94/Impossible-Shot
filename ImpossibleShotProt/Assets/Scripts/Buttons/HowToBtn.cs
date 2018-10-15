using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToBtn : MonoBehaviour {

	[SerializeField] GameObject HowToPanel;
	[SerializeField] GameObject CurrentPanel;

	public void Clicked(){
		CurrentPanel.SetActive(false);
		HowToPanel.SetActive(true);
	}
}
