using UnityEngine;

public class TerrainMovment : MonoBehaviour {

	[SerializeField]private Transform back;
	private float speed;

	private void Update(){
		speed = GameManager.Instance.TerrainSpeed;
		Reset();
	}

	private void LateUpdate() {
		Movment();
	}

	private void Movment(){
		if (transform.position.z > -110) {
			transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		}
	}

	private void Reset() {
		if (transform.position.z < -110) {
			transform.position = back.TransformDirection(back.position);
		}
	}
}
