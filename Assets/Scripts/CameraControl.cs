using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraCode : MonoBehaviour
{
    private static Camera cameraMain;
    private float speed = 25.0f;
    private float lerpSpeed = 10.0f;

    [Header("Mouse Attributes")]
    private float mouseX;
    private float mouseY;

    [Header("Mouse Settings")]
    private float mouseSpeed = 100.0f;
    private float mouseLerpSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GetComponent<Camera>();

        mouseX = 0.0f;
        mouseY = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraMovement();
        CameraRotation();
    }

    protected void CameraMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = cameraMain.transform.forward;
        Vector3 right = cameraMain.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardMovement = verticalInput * forward * speed * Time.fixedDeltaTime;
        Vector3 sideMovement = horizontalInput * right * speed * Time.fixedDeltaTime;

        Vector3 newCameraPosition = cameraMain.transform.position + forwardMovement + sideMovement;

        cameraMain.transform.position = Vector3.Lerp(cameraMain.transform.position, newCameraPosition, Time.fixedDeltaTime * lerpSpeed);
    }

    protected void CameraRotation()
    {
        if(Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * mouseSpeed;
            mouseY -= Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * mouseSpeed;
            mouseY = Mathf.Clamp(mouseY, -80.0f, 80.0f);

            Quaternion rotation = Quaternion.AngleAxis(mouseY, this.transform.right) * Quaternion.AngleAxis(mouseX, Vector3.up);
            cameraMain.transform.rotation = Quaternion.Lerp(cameraMain.transform.rotation, rotation, Time.deltaTime * mouseLerpSpeed);
        }
    }
}
