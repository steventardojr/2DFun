using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform playerTransform;
    private Transform cameraTransform;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        cameraTransform = GetComponent<Transform>();
        offset = cameraTransform.position - playerTransform.position;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 updatedPosition = playerTransform.position + offset;
        cameraTransform.position = updatedPosition;
	
	}
}
