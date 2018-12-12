using UnityEngine;

public class PropScript : MonoBehaviour {
	[SerializeField]private float distZ;
	private float speed;

	private void LateUpdate() {
		speed = GameManager.Instance.TerrainSpeed;
		transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		if(transform.position.z < distZ){
			SpawnEnv.Instance.DespawnProp(gameObject);
		}
	}
}
