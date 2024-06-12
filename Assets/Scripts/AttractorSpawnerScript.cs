using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AttractorSpawner : MonoBehaviour
{
    public GameObject _attractorObject;

    private static Vector3 attractorOrigin;
    private static float attractorRange;

    // Start is called before the first frame update
    void Start()
    {
        attractorOrigin = Vector3.zero;
        SetRange(55.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOrigin(Vector3 origin)
    {
        attractorOrigin = origin;
    }
    public void SetRange(float range)
    {
        attractorRange = range;
        if( attractorRange >= 0f)
        {
            attractorRange = 0.1f;
        }
    }

    public void SpawnAttractors()
    {
        Vector3 attractorPlacement;
        for (int i = 0; i < 10; i++)
        {
            attractorPlacement = new Vector3(attractorOrigin.x + Random.Range(0, attractorRange), attractorOrigin.y + Random.Range(0, attractorRange), attractorOrigin.z + Random.Range(0, attractorRange));
            Instantiate(_attractorObject, attractorPlacement, Quaternion.identity);
        }
    }
}
