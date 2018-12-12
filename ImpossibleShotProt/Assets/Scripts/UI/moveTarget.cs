using UnityEngine;

public class moveTarget : MonoBehaviour {
    [SerializeField]private Transform Bullet;


	void Update () {
        transform.position = new Vector3(Bullet.position.x, transform.position.y, transform.position.z);
	}
}
