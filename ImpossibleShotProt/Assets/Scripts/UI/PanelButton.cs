﻿using UnityEngine;

public class PanelButton : MonoBehaviour {
	[SerializeField]private GameObject[] Current;
	[SerializeField]private GameObject[] Next;
	
	public void Clicked(){
		SoundManager.Instance.MenuTouch();
		if (Current.Length > 0){
			for (int i = 0; i < Current.Length ;i++){
				Current[i].SetActive(false);
			}
		}
		if(Next.Length > 0){
			for(int i = 0; i < Next.Length; i++){
				Next[i].SetActive(true);
			}
		}
	}
}
