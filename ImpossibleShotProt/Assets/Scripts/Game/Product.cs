using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {
	[SerializeField] float MaxDist;
	private Pattern patron;
	private bool Activo;
	[SerializeField]private int width = 1;
	private PositionManager pM;
	private float index;

	public Pattern Patron {
		get{return patron; }
		set{patron = value; }
	}

	public float Index{
		set{index=value;}
		get{return index;}
	}
	public int Width{
		get{
			return width;
		}
	}

	private void Awake(){
		Activo = false;
		pM = PositionManager.Instance;
	}
	private void Update(){
		if (transform.position.z <= MaxDist * -1){
			ReturnToFactory ();
		}
	}

	public void ReturnToFactory(){
		pM.freePosition(index,width);
		Activo = false;
		patron.Return (gameObject);
	}

	public void Sent(){
		Activo = true;
	}

	public bool IsActive(){
		return Activo;
	}

}