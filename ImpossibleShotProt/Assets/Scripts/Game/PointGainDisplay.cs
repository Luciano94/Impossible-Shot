using UnityEngine;
using UnityEngine.UI;

public class PointGainDisplay : MonoBehaviour {
    [SerializeField] [Range(0.0f, 2.0f)] private float TimeToFade;
    [SerializeField] private Transform bullet;
    [SerializeField] [Range(0.0f, 1f)] private float offset;
    [SerializeField] [Range(1, 5)] private int TransitionSharpness;
    [SerializeField] private Transform TargetPos;
    private Vector3 initPos;
    private Text Txt;
    private float Timer;
    private float DisplayedScore;
    private float LerpState = 0;

    void Start() {
        initPos = new Vector3();
        Txt = GetComponent<Text>();
        Txt.text = "";
        Timer = TimeToFade;
        DisplayedScore = 0;
    }

    void Update() {
        Timer += Time.deltaTime;
        if (Timer > TimeToFade) {
            LerpText();
        }else {
            initPos = Txt.transform.position;
            LerpState = 0;
        }
    }

    public void AddScore(float score) {
        if (score != 0) {
            PosPointsInScreen();
            Timer = 0;
            DisplayedScore += score;
            Txt.text = "+" + DisplayedScore;
        }
    }

    private void PosPointsInScreen()
    {
        Vector3 pos = new Vector3(bullet.position.x, bullet.position.y + offset, 0);
        initPos = pos;
        Txt.transform.position = Camera.main.WorldToScreenPoint(pos);
    }

    private void LerpText()
    {
        LerpState += TransitionSharpness * Time.deltaTime;
        Vector3 targetPos = Camera.main.WorldToScreenPoint(TargetPos.position);
        targetPos = new Vector3(targetPos.x , targetPos.y + offset, 0);
        if (LerpState > 1.0f) { LerpState = 1.0f; }
        Txt.transform.position = Vector3.Lerp(initPos, targetPos, LerpState);
        if (LerpState >= 1.0f){
            Txt.text = " ";
            DisplayedScore = 0;
        }
    }
}
