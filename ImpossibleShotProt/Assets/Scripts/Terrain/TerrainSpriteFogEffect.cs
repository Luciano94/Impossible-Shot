using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class TerrainSpriteFogEffect : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    private Color targetColor;
    private float initialPosition;
    private float targetPosition;
    
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        //targetColor = RenderSettings.fogColor;

        initialPosition = RenderSettings.fogEndDistance;
        targetPosition = RenderSettings.fogStartDistance;
        if (targetPosition < 0) { targetPosition = 0; }
	}
	
	void Update () {
        if (transform.position.z >= 0 && (initialPosition - targetPosition) != 0 ) {
            float lerpState = transform.position.z / (initialPosition - targetPosition);
            spriteRenderer.color = Color.Lerp(initialColor, Color.black, lerpState);
        }
	}
}
