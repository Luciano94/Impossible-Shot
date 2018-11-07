using UnityEngine;

public class Factory : MonoBehaviour {
	[SerializeField] GameObject Product;
	[SerializeField] int Capacidad;
	private GameObject[] cargador;
	private bool[] tracker;

	void Start () {
		cargador = new GameObject[Capacidad];
		tracker = new bool[Capacidad];
		for(int i = 0; i< Capacidad; i++){
			GameObject go = Instantiate (Product);
			go.transform.position = Vector3.one * 60;
			cargador [i] = go;
			tracker [i] = true;
		}
	}
	
	public GameObject Request(){
		for(int i = 0; i< Capacidad; i++){
			if(tracker[i]){
				tracker [i] = false;
				cargador [i].GetComponent<Product> ().Sent ();
				return cargador[i];
			}
		}
		GameObject go = Instantiate (Product);
		go.GetComponent<Product> ().Sent ();
		return go;
	}

	public void Return(GameObject go){
		bool extra = true;
		for (int i = 0; i < Capacidad; i++){
			if(go == cargador[i]){
				tracker[i] = true;
				extra = false;
				cargador [i].transform.position = Vector3.one * 60;
			}
		}
		if (extra){
			Debug.Log (Product.tag + " extra destruido");
			Destroy (go);
		}
	}
}