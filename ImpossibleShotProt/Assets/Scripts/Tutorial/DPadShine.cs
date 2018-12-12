using UnityEngine;

public class DPadShine : MonoBehaviour {

    [SerializeField] private UnityEngine.UI.Image[] ObjectsToMark;
    [SerializeField] private Color ShineColor;
    [SerializeField] [Range(1, 25)] private int Speed;
    private Color[] initialColor;

    private uint stage;
    private int total;

    private bool doneShining = false;

    private float lerpState;
    private int currentImage = 1;
    private bool oneByOneDirection = true;

    private void Start(){
        total = ObjectsToMark.Length;

        initialColor = new Color[total];

        for (int i = 0; i < total; i++) {
            initialColor[i] = ObjectsToMark[i].color;
        }

        lerpState = 0;
    }

    void Update() {
        switch (stage) {
            case 0:
                break;

            case 1: //fade into color
                doneShining = false;
                lerpState += Speed * Time.deltaTime;
                if (lerpState >= 1) { lerpState = 1; stage = 2;  }
                ChangeColors();
                break;

            case 2: //fade out of color
                lerpState -= Speed * Time.deltaTime;
                if (lerpState <= 0) { lerpState = 0; stage = 3;  }
                ChangeColors();
                break;
            case 3:
                OneByOne();
                if (currentImage == total && lerpState == 0 && oneByOneDirection) {
                    currentImage = 1; stage = 0; doneShining = true;
                    for (int i = 0; i < total; i++){
                        ObjectsToMark[i].color = initialColor[i];
                      }
                }
                break;
        }
	}

    public void Shine(float t){ Invoke("Go", t); }

    private void Go() { stage = 1; }

    private void ChangeColors() {
        for (int i = 0; i < total; i++){
            ObjectsToMark[i].color = Color.Lerp(initialColor[i], ShineColor, lerpState);
        }
    }

    private void OneByOne() {
        ObjectsToMark[currentImage].color = Color.Lerp(initialColor[currentImage], ShineColor, lerpState);
        if (oneByOneDirection){
            lerpState += Speed * 5 * Time.deltaTime;
            if (lerpState >= 1) { lerpState = 1; oneByOneDirection = false;}
        } else {
            lerpState -= Speed * 5 * Time.deltaTime;
            if (lerpState <= 0) { lerpState = 0; oneByOneDirection = true; currentImage++; }
        }
    }

    public bool DoneShining(){ return doneShining;}
}
