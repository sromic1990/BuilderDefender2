using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    private float orthoSize;
    private float targetOrthoSize;

    private void Start()
    {
        orthoSize = vCam.m_Lens.OrthographicSize;
        targetOrthoSize = orthoSize;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrthoSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrthoSize = 10;
        float maxOrthoSize = 30;
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);

        float zoomSpeed = 5f;
        orthoSize = Mathf.Lerp(orthoSize, targetOrthoSize, Time.deltaTime * zoomSpeed);
        
        vCam.m_Lens.OrthographicSize = orthoSize;
    }
}
