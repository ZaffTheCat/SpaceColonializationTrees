using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraCode : MonoBehaviour
{
    private static Camera _cameraMain;
    private float _speed = 25.0f;
    private float _lerpSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraMain = GetComponent<Camera>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = _cameraMain.transform.forward;
        Vector3 right = _cameraMain.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardMovement = verticalInput * forward * _speed * Time.fixedDeltaTime;
        Vector3 sideMovement = horizontalInput * right * _speed * Time.fixedDeltaTime;

        Vector3 newCameraPosition = _cameraMain.transform.position + forwardMovement + sideMovement;

        _cameraMain.transform.position = Vector3.Lerp(_cameraMain.transform.position, newCameraPosition, Time.fixedDeltaTime * _lerpSpeed);
    }
}
