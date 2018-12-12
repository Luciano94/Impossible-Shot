using UnityEngine;

public class Product : MonoBehaviour {
	[SerializeField] private float MaxDist;
    private bool Activo;
	[SerializeField] private int width = 1;

    public Pattern Patron { get; set; }

    public float Index { set; get; }

    public int Width{
		get{
			return width;
		}
	}

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
		if(Patron != null) Patron.Return (gameObject);
		else Destroy(gameObject);
	}

	public void Sent(){
		Activo = true;
	}

	public bool IsActive(){
		return Activo;
	}

}