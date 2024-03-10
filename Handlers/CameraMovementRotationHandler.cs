using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementRotationHandler : MonoBehaviour
{
    [SerializeField] private Transform followObject;
    [SerializeField] private CinemachineVirtualCamera topDownCamera;

    [SerializeField] private float panSpeed = 1f;

    [SerializeField] private float rotationSpeed = 1f;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float maxZoom = 60f;
    [SerializeField] private float minZoom = 20f;

    private InputHandler inputHandler;
    private bool hasTouch;

    private void Start()
    {
        inputHandler = InputHandler.Instance;

        inputHandler.OnTouchStart += InputHandler_OnTouchStart;
        inputHandler.OnTouchEnded += InputHandler_OnTouchEnded;
    }

    private void InputHandler_OnTouchEnded()
    {
        hasTouch = false;
    }
    private void InputHandler_OnTouchStart()
    {
        hasTouch = true;
    }

    private void Update()
    {
        if (!hasTouch) return;

        if (inputHandler.GetTouchCount() == 1)
        {
            //If we are touching on left side we are panning else we are rotating the camera
            if (inputHandler.IsLeftSideTouch())
            {
                PanCamera();
            }
            else
            {
                RotateCamera();
            }
        }
        else if(inputHandler.GetTouchCount() == 2)
        {
            ZoomCamera();
        }
    }

    private void PanCamera()
    {
        Vector3 delta = new Vector3(inputHandler.GetTouchDeltaPosition().x, 0, inputHandler.GetTouchDeltaPosition().y) * panSpeed * Time.deltaTime;
        followObject.transform.Translate(-delta);
    }

    private void RotateCamera()
    {
        Vector3 deltaRotation = new Vector3(0, inputHandler.GetTouchDeltaPosition().x, 0) * rotationSpeed * Time.deltaTime;
        followObject.transform.Rotate(-deltaRotation, Space.World);
    }
    private void ZoomCamera()
    {
        Vector2 touch1PrevPos = inputHandler.GetTouchCurrentPosition() - inputHandler.GetTouchDeltaPosition();
        Vector2 touch2PrevPos = inputHandler.GetTouch2CurrentPosition() - inputHandler.GetTouch2DeltaPosition();

        float prevTouchDeltaMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
        float touchDeltaMagnitude = (inputHandler.GetTouchCurrentPosition() - inputHandler.GetTouch2CurrentPosition()).magnitude;

        float deltaMagnitudeDifference = prevTouchDeltaMagnitude - touchDeltaMagnitude;

        float zoomAmount = deltaMagnitudeDifference * zoomSpeed * Time.deltaTime;

        float newFov = Mathf.Clamp(topDownCamera.m_Lens.FieldOfView + zoomAmount, minZoom, maxZoom);

        topDownCamera.m_Lens.FieldOfView = newFov;

    }
}
