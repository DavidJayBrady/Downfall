
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float cameraSpeed = 15.0f;
    
    public GameObject _camera;

    public int controllerID = 1;
    public Vector2 lookVector = new Vector2(-1.0f, -1.0f);
    protected Rigidbody2D _rigidbody2D; // Named with an underscore to avoid issues with Component.rigidbody2D

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerVelocity();
        UpdatePlayerLook();
        UpdateCamera();
    }

    // Update the player's velocity
    private void UpdatePlayerVelocity()
    {
        _rigidbody2D.velocity = Common.GetScaledVectorInput(controllerID, "Left Horizontal", "Left Vertical", speed) * new Vector2(1.0f, 0.5f);
    }

    // Returns true when the player is in camera mode
    public bool IsCameraMode()
    {
        return Common.GetControllerInputAxis(controllerID, "Right Trigger") > 0.1f;
    }

    // Update the camera's position and/or velocity
    private void UpdateCamera()
    {
        if (IsCameraMode())
        {
            // Detach the camera from the player
            _camera.transform.parent = null;
            Vector3 deltaPos = Common.GetScaledVectorInput(controllerID, "Right Horizontal", "Right Vertical", cameraSpeed * Time.deltaTime) * new Vector2(1.0f, 0.5f);
            _camera.transform.position += deltaPos;
        }
        else
        {
            // reattach the camera to the player
            _camera.transform.parent = this.transform;
            _camera.transform.localPosition = new Vector3(0.0f, 0.0f, -19.0f); // z=-19 is default
        }
    }

    // Update the direction the player is looking
    // Controlled by right joystick
    private void UpdatePlayerLook()
    {
        if (!IsCameraMode())
        {
            Vector2 tempLookVector = Common.GetScaledVectorInput(controllerID, "Right Horizontal", "Right Vertical"); ;
            if (tempLookVector.magnitude > 0.5f)
            {
                Common.VectorNormalize(ref tempLookVector);
                lookVector = tempLookVector;
            }
        }
    }
}
