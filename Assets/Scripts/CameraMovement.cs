using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject targetObject;  // The object to follow
    public Vector3 offset;  // The distance to maintain relative to the target

    // Use this for initialization
    void Start() {
        if (targetObject == null) {
            Debug.LogError("No target assigned for " + gameObject.name);
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()  // We use LateUpdate() to make sure the player's movement has been processed for this frame
    {
        if (targetObject != null) {
            transform.position = targetObject.transform.position + offset;
        }
    }
}
