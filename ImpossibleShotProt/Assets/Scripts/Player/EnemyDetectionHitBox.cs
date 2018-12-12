using UnityEngine;

public class EnemyDetectionHitBox : MonoBehaviour {
	[SerializeField] private int FramesToPredict= 0;
	[SerializeField] private float MaxSize=0;

	private float TotalDistance;
	private float TotalFrames;
	private float InitPos;
	private float InitHeight;
	private CapsuleCollider col;

	void Start () {
		var PC = GetComponent<CapsuleCollider> ();
		GameObject go = new GameObject ("Forward Enemy Detection collider");
		go.layer = 12;
		go.tag = "Bullet";

		go.transform.parent = transform;
		go.transform.position = transform.position;

		col = go.AddComponent<CapsuleCollider> ();
		col.center = PC.center;
		InitPos = PC.center.z;
		col.direction = PC.direction;
		col.radius = PC.radius;
		col.height = PC.height;
		InitHeight = PC.height;
		col.isTrigger = true;

		TotalDistance = 0;
		TotalFrames = 1;
	}
	
	void Update () {
		var speed = GameManager.Instance.TerrainSpeed;
		if (Time.deltaTime > 0) {
			TotalFrames += 1 * Time.timeScale;
			TotalDistance += speed * Time.deltaTime;
		}
	}

	void LateUpdate(){
		col.height = InitHeight + ((TotalDistance / TotalFrames) * FramesToPredict);
		col.center =new Vector3(col.center.x,col.center.y, InitPos + ((col.height - InitHeight)/2));
		if (MaxSize > 0 && col.height > MaxSize) {
			col.height = MaxSize;
		}
	}
}