using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    private static PowerUpManager instance;

    public static PowerUpManager Instance {
        get {
            instance = FindObjectOfType<PowerUpManager>();
            if (instance == null) {
                GameObject go = new GameObject("PowerUpManager");
                instance = go.AddComponent<PowerUpManager>();
            }
            return instance;
        }
    }

    [SerializeField] private float cantOfKillingSpree = 3;
    [SerializeField] private ParticleSystem spark;
    [SerializeField] private float timePwUp = 5.0f;
    [SerializeField] private int multBoost = 2;
    [SerializeField] private Color colorpart;
    [SerializeField] private UIAura uiAura;
    private float actCantKS = 0;

    private TrailColorTransition trail;
    private bool timeKeeping;
    private float countdown;

    private void Start(){
        trail = FindObjectOfType<TrailColorTransition>();
        timeKeeping = false;
        countdown = timePwUp;
    }
	public void UpdateKillingSpree(){
        actCantKS++;
        if(actCantKS == cantOfKillingSpree){
            ActivatePwUp();
        }
        if(!timeKeeping){
            trail.ColorChange(actCantKS/cantOfKillingSpree);
            Debug.Log(actCantKS/cantOfKillingSpree);
        }
    }

    private void ActivatePwUp(){
        SoundManager.Instance.PowerUp();
        spark.Play();
        GameManager.Instance.Multiplicador *= multBoost;
        timeKeeping = true;
        countdown = timePwUp;
        uiAura.ShineStart();
        Invoke("DesactivatePwUp", timePwUp);
    }

    private void DesactivatePwUp(){
        SoundManager.Instance.EndPowerUp();
        spark.Stop();
        uiAura.ShineStop();
        actCantKS = 0;
        GameManager.Instance.Multiplicador /= multBoost;
        trail.ColorChange(0.0f);
        trail.DesactivePwUp();
    }

    private void Update(){
        if(timeKeeping){
            countdown -= Time.deltaTime;
            if(countdown < 0){ countdown = 0; timeKeeping = false;}
            trail.ColorChange(countdown/timePwUp);
            //Debug.Log(countdown/timePwUp);
        }
    }
}
