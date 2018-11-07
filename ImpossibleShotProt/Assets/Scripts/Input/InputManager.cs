using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	private static InputManager instance;
	public static InputManager Instance{
		get{
			instance = FindObjectOfType<InputManager> ();
			if (instance == null){
				GameObject go = new GameObject ("Input Manager");
				instance = go.AddComponent<InputManager> ();
			}
			return instance;
		}
	}

	IInput input;
	void Awake (){
		instance = this;
		#if UNITY_ANDROID
		input = new InputAndroidAlternative();
		#else
		input = new InputPC();
		#endif
		input.Awake ();
	}

	public Direction GetDirection(){
		return input.GetDirection();
	}
	
	private void Update(){
		input.Update ();
	}
	//Alternative android input only
	public void GoUp(){ input.GoUp();}
	public void GoDown(){input.GoDown();}
	public void GoRight(){input.GoRight();}
	public void GoLeft(){input.GoLeft();}
}
