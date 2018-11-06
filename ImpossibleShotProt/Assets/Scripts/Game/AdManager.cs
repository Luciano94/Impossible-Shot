using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour {

	private static AdManager instance = null;

    public static AdManager Instance {
        get {
            instance = FindObjectOfType<AdManager>();
            if(instance == null) {
                GameObject go = new GameObject("Ad Manager");
                instance = go.AddComponent<AdManager>();
            }
            return instance;
        }
    }

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
