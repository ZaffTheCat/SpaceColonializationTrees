using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceTrunkOnPlane : MonoBehaviour
{
    public Camera _cameraMain;
    public GameObject _prefab;
    public AttractorSpawner _attractorSpawner;
    public BranchManager _branchManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layer = 6;
        int layerMask = 1 << layer;

        // LMB pressed
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(0);
            RaycastHit hit;
            Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1000.0f);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 point = hit.point;
                point.y += 0.3f;

                Debug.Log(1);

                _branchManager.AddBranch(Instantiate(_prefab, point, Quaternion.identity));
                _attractorSpawner.SetOrigin(point);
                Debug.Log(2);

                _attractorSpawner.SpawnAttractors();

                Debug.Log(3);
                _attractorSpawner.Populate();
            }

        }
    }
}
