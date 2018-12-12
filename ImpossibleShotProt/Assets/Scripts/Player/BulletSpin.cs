using UnityEngine;

public class BulletSpin : MonoBehaviour {

    [SerializeField] private int SpinSpeed = 10;
	
	void Update () {
       gameObject.transform.Rotate(0, 0, SpinSpeed * Time.deltaTime * -1);
    }
}
