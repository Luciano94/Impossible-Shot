using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour {

	[SerializeField] private float lifeTime; //tiempo de vida del objeto

	private void Awake()
	{
		Destroy(gameObject, lifeTime);
	}

	void Update () {
		transform.Translate(Vector3.back * GameManager.Instance.TerrainSpeed * Time.deltaTime);
	}
}
