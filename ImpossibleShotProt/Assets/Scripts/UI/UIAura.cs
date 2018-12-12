using UnityEngine;
using UnityEngine.UI;

enum AuraState{
    Invisible = 0,
    ShineUp,
    BackAndForth,
    ShineDown,
    TotalStates
}

public class UIAura : MonoBehaviour {

    [SerializeField][Range(0.0f, 1.0f)] private float MinAlpha;
    [SerializeField][Range(0.0f, 1.0f)] private float MaxAlpha;
    [SerializeField][Range(1, 20)] private int TransitionSharpness;
    private Image image;
    private Color initialColor;
    private AuraState state;
    private float lerpState;
    private bool backAndForthDirection;

    void Start(){
        image = GetComponent<Image>();
        state = 0;
        lerpState = 0;
        initialColor = image.color;
        image.color = new Color(initialColor.r,initialColor.g,initialColor.b,0);
        backAndForthDirection = true;
    }

    void Update () {
        float alpha;
        switch (state) {
            case AuraState.Invisible:
                image.color = Color.clear;
                break;
            case AuraState.ShineUp:
                lerpState += Time.deltaTime * TransitionSharpness;
                if (lerpState >= 1) {
                     lerpState = 1;
                }
                alpha = Mathf.Lerp(0, MinAlpha, lerpState);
                image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                if (lerpState >= 1) { 
                    lerpState = 0; state++;
                }
                break;
            case AuraState.BackAndForth:
                if (backAndForthDirection) {
                    lerpState += Time.deltaTime * TransitionSharpness;
                    if (lerpState >= 1) { 
                        lerpState = 1; backAndForthDirection = false; 
                    }
                } else {
                    lerpState -= Time.deltaTime * TransitionSharpness;
                    if (lerpState <= 0) { 
                        lerpState = 0; backAndForthDirection = true;
                    }
                }
                alpha = Mathf.Lerp(MinAlpha, MaxAlpha, lerpState);
                image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                break;
            case AuraState.ShineDown:
                lerpState += Time.deltaTime * TransitionSharpness;
                if (lerpState >= 1) { 
                    lerpState = 1; 
                    state = 0; 
                }
                alpha = Mathf.Lerp(initialColor.a,0,lerpState);
                image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                break;
            default:
                state = 0;
                break;
            

        }
	}

    public void ShineStart() {
        lerpState = 0;
        state = AuraState.ShineUp;
    }
    
    public void ShineStop() {
        lerpState = 0;
        initialColor.a = image.color.a;
        state = AuraState.ShineDown;
    }
}
