using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlay : MonoBehaviour {
	private static FirstPlay instance;

    public static FirstPlay Instance {
        get {
            instance = FindObjectOfType<FirstPlay>();
            if(instance == null) {
                GameObject go = new GameObject("FirstPlay");
                instance = go.AddComponent<FirstPlay>();
            }
            return instance;
        }
    }
	
	private bool firstPlay = true;

	public void play(){
		firstPlay = false;
	}

	public bool Played{
		get{return firstPlay;}
	}

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
