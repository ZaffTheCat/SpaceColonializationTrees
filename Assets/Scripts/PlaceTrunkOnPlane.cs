using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTrunkOnPlane : MonoBehaviour
{
    [SerializeField]
    public static Camera cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // LMB pressed
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1000.0f);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit");
            }

        }
        
    }
}
