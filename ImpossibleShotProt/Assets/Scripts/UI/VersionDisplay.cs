using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<UnityEngine.UI.Text>().text += Application.version;
	}
}
