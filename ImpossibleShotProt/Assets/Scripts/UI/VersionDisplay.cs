using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VersionDisplay : MonoBehaviour {
	void Start () {
		GetComponent<Text>().text ="V"+ Application.version;
	}
}
