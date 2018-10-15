using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour {

	[SerializeField] private GameObject CurrentPanel;
	[SerializeField] private GameObject PanelToReturnTo;

	public void Clicked(){
		CurrentPanel.SetActive(false);
		PanelToReturnTo.SetActive(true);
	}
}
