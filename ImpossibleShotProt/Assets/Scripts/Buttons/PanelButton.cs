using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour {
	[SerializeField] GameObject Current;
	[SerializeField] GameObject Next;
	
	public void Clicked(){
		Next.SetActive(true);
		Current.SetActive(false);
	}
}
