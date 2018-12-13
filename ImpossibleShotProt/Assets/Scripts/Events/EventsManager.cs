using UnityEngine;

public class EventsManager : MonoBehaviour {
    private static EventsManager instance;

    public static EventsManager Instance {
        get {
            instance = FindObjectOfType<EventsManager>();
            if(instance == null) {
                GameObject go = new GameObject("EventsManager");
                instance = go.AddComponent<EventsManager>();
            }
            return instance;
        }
    }

    [Header("Event Settings")]    
    [SerializeField] private float timePerEventMin;
    [SerializeField] private float timePerEventMax;

    [Header("Event BulletTime")]    
    [SerializeField] private float bulletTimeTime;
    [SerializeField] private float bulletTimeScale;

    [Header("Event EnemyStream")]
    [SerializeField] private float enemyStreamTime;
    [SerializeField] private float enemyTimeScale;    

    private PatternSpawner patternSpawner;
    private bool activeEvent = false;
    private bool eventActive = false;
    private bool whichEvent = false; //true bullet time/ false timetokill o enemyevent
    
    public bool ActiveEvent{
        get{return eventActive;}
    }

    private void Start(){
        patternSpawner = GetComponent<PatternSpawner>();
    }

    public void ActiveEvents(){
        if(!activeEvent){
            activeEvent = true;
            float timeNextEvent = Random.Range(timePerEventMin, timePerEventMax);
            Invoke("InitEvent", timeNextEvent);
        }
    }

    private void InitEvent(){
        int nextEvent  = Random.Range(1,10);
        eventActive = true;
        Debug.Log("Next event: " + nextEvent);
        if(nextEvent >= 1 && nextEvent <= 5){
            whichEvent = false;
            patternSpawner.InitEvent();
            Invoke("ShowEneTxt", enemyStreamTime * 0.5f);
            Invoke("ActiveEventEnemy", enemyStreamTime);        
        }else{
            whichEvent = true;
            patternSpawner.InitEvent();
            Invoke("ShowBulletTxt", enemyStreamTime * 0.5f);
            Invoke("ActiveEventBullet", bulletTimeTime);
        }
    }

    public void ShowBulletTxt(){
        MenuManager.Instance.ShowBulletEvent();
    }

    public void ShowEneTxt(){
        MenuManager.Instance.ShowEnemyEvent();
    }

    public void DesactiveEvent(){
        Invoke("TimeToNormal", 2f);
        if(whichEvent){
            SoundManager.Instance.EndBulletTime();
        } else {
            SoundManager.Instance.EndTimeToKill();
        }
        activeEvent = false;
        eventActive = false;
        ActiveEvents();
    }

    private void TimeToNormal(){
        Time.timeScale= 1;
    }

    private void ActiveEventBullet(){     
        Time.timeScale = bulletTimeScale;
        patternSpawner.BeginBulletEvent();
        SoundManager.Instance.BulletTime();
    }

    private void ActiveEventEnemy(){
        Time.timeScale = enemyTimeScale;
        patternSpawner.BeginEnemyEvent();
        SoundManager.Instance.TimeToKill();
    }
}
