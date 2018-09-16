using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	private static InputManager instance = null;
	IInput input;
	public static InputManager Instance{
		get{
			instance = FindObjectOfType<InputManager> ();
			if (instance == null){
				GameObject go = new GameObject ("InputManager");
				instance = go.AddComponent<InputManager> ();
			}
			return instance;
		}
	}

	void Awake ()
	{
		instance = this;
		#if UNITY_ANDROID
		input = new InputAndroid();
		#else
		input = new InputPC();
		#endif
		input.Awake ();
	}
	public DirectionVec GetDirection()
	{
		return input.GetDirection();
	}
	private void Update(){
		input.Update ();
	}
}
