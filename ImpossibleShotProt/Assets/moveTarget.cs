using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTarget : MonoBehaviour {
    [SerializeField] Transform Bullet;


	void Update () {
        transform.position = new Vector3(Bullet.position.x, transform.position.y, transform.position.z);
	}
}
