using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGainDisplay : MonoBehaviour {
    [SerializeField] [Range(0.0f, 2.0f)] float TimeToFade;
    [SerializeField] Transform bullet;
    [SerializeField] [Range(0.0f, 1f)] float offset;
    [SerializeField] [Range(1, 5)] private int TransitionSharpness;
    [SerializeField] Transform TargetPos;
    private Vector3 initPos;
    private UnityEngine.UI.Text Txt;
    private float Timer;
    private float DisplayedScore;
    private float LerpState = 0;

    // Use this for initialization
    void Start() {
        initPos = new Vector3();
        Txt = GetComponent<UnityEngine.UI.Text>();
        Txt.text = "";
        Timer = TimeToFade;
        DisplayedScore = 0;
    }

    // Update is called once per frame
    void Update() {
        Timer += Time.deltaTime;
        if (Timer > TimeToFade) {
            LerpText();
        }
    }

    public void AddScore(float score) {
        if (score != 0) {
            PosPointsInScreen();
            Timer = 0;
            DisplayedScore += score;
            Txt.text = "+" + DisplayedScore;
            //fade in
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
        if (LerpState > 1.0f) { LerpState = 1.0f; }
        Txt.transform.position = Vector3.Lerp(Txt.transform.position, targetPos, LerpState);
        if (LerpState >= 1.0f)
        {
            Txt.text = " ";
            DisplayedScore = 0;
            LerpState = 0;
        }
    }
}
