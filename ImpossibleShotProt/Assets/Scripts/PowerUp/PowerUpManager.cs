using UnityEngine;

public class PowerUpManager : MonoBehaviour {
	
	private static PowerUpManager instance;

    public static PowerUpManager Instance {
        get {
            instance = FindObjectOfType<PowerUpManager>();
            if(instance == null) {
                GameObject go = new GameObject("PowerUpManager");
                instance = go.AddComponent<PowerUpManager>();
            }
            return instance;
        }
    }

	[SerializeField]private int cantOfKillingSpree = 3;
    [SerializeField]private ParticleSystem trail;
    [SerializeField]private ParticleSystem spark;
    [SerializeField]private float timePwUp = 5.0f;
    [SerializeField]private int multBoost = 2;
    [SerializeField]private Color colorpart;
    private int actCantKS = 0;

	public void UpdateKillingSpree(){
        actCantKS++;
        if(actCantKS == cantOfKillingSpree){
            ActivatePwUp();
        }
    }

    private void ActivatePwUp(){
        ParticleSystem.MainModule settings = trail.main;
        settings.startColor = colorpart;
        spark.Play();
        GameManager.Instance.Multiplicador *= multBoost;
        Invoke("DesactivatePwUp", timePwUp);
    }

    private void DesactivatePwUp(){
        ParticleSystem.MainModule settings = trail.main;
        settings.startColor = new Color(1,1,1,1);
        spark.Stop();
        actCantKS = 0;
        GameManager.Instance.Multiplicador /= multBoost;
    }
}
