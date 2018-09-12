using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {
	[SerializeField][Range(0f,1f)] private float RotationSharpness;
	[SerializeField][Range(0f,1f)] private float FallSharpness;
	private bool ended;
	private bool end;

	// Use this for initialization
	void Start () {
		end = false;
		ended = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (end){
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (Vector3.down), RotationSharpness);
			transform.position = new Vector3 (transform.position.x,
											Mathf.Lerp (transform.position.y, 0, FallSharpness),
											transform.position.z);
			Debug.Log (Mathf.Clamp(transform.position.y,0,1));
			if(Mathf.Clamp(transform.position.y,0,1) == 0){
				Debug.Log (Mathf.Clamp(transform.position.y,0,1));
			}
		}
	}

	public bool HasEnded(){
		return ended;
	}

	public void End(){
		end = true;
	}
}
