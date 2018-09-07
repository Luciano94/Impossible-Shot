using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {
	[SerializeField] Factory Factory;
	[SerializeField] float MaxDist;
	private bool Activo;

	private void Awake(){
		Activo = false;
	}
	private void Update(){
		if (transform.position.z <= MaxDist * -1){
			ReturnToFactory ();
		}
	}

	public void ReturnToFactory(){
		Activo = false;
		Factory.Return (gameObject);
	}

	public void Sent(){
		Activo = true;
	}

	public bool IsActive(){
		return Activo;
	}

}