using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to control the speed of the camera.

    private float leftBoundary; // The leftmost point the camera can go.
    private float rightBoundary; // The rightmost point the camera can go.

    private void Start()
    {
        // Calculate the boundaries based on the width of the platform.
        var platformRenderer = GameObject.Find("Ground").GetComponent<Renderer>();
        var platformWidth = platformRenderer.bounds.size.x;
        var cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        leftBoundary = platformRenderer.transform.position.x - platformWidth / 2 + cameraHalfWidth;
        rightBoundary = platformRenderer.transform.position.x + platformWidth / 2 - cameraHalfWidth;
    }

    private void Update()
    {
        // Move the camera to the right.
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Clamp the camera's position to the boundaries.
        var clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary, rightBoundary);
        transform.position = clampedPosition;
    }
}
