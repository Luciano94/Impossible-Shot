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
	bool isInputAndroidAlternative;
	void Awake ()
	{
		instance = this;
		Debug.Log ("IM created");

	}

	public void Initialize(bool AlternativeInput){
		#if UNITY_ANDROID
		if(AlternativeInput){
			input = new InputAndroidAlternative();
			isInputAndroidAlternative = true;
		} else {
			input = new InputAndroid();
			isInputAndroidAlternative = false;
		}
		#else
		input = new InputPC();
		isInputAndroidAlternative = false;
		#endif
		input.Awake ();
	}
	public Direction GetDirection()
	{
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

	public bool IsInputAndroidAlternative(){
		return isInputAndroidAlternative;
	}
}
