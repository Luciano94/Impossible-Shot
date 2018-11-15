using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScriptsDissapearingGeometry : MonoBehaviour {

    private void OnDestroy()
    {
        Debug.Log("destroyed");
    }
    private void OnDisable()
    {
        Debug.Log("disabled");
    }
    private void OnTransformParentChanged()
    {
        Debug.Log("Parent changed");
    }
}
