using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AttractorSpawner : MonoBehaviour
{
    public GameObject _attractorObject;
    public GameObject _branchObject;
    public BranchManager _branchManager;

    [Header("Attractor Spawn Settings")]
    private static Vector3 attractorOrigin;
    private static float attractorRange;
    private static int attractorCount;

    [Header("Attraction Distance Settings")]
    private static float minimumDistance = 3.5f;
    private static float cullDistance = 2.5f;

    private List<GameObject> attractorSpawns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        attractorOrigin = Vector3.zero;
        SetRange(5.0f);
        attractorCount = 20;
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
        if( attractorRange <= 0f)
        {
            attractorRange = 0.1f;
        }
    }

    public void SpawnAttractors()
    {
        Vector3 attractorPlacement;
        for (int i = 0; i < attractorCount; i++)
        {
            attractorPlacement = new Vector3(attractorOrigin.x + Random.Range(-attractorRange, attractorRange), attractorOrigin.y + Random.Range(0, attractorRange), attractorOrigin.z + Random.Range(-attractorRange, attractorRange));
            attractorSpawns.Add(Instantiate(_attractorObject, attractorPlacement, Quaternion.identity));
        }
    }

    public void Populate()
    {
        //iterate through each branch
        for(int i = 0; i <_branchManager._branchList.Count; i++)
        {
            //TODO: make sure not too many branches
            if (i > 100)
            {
                break;
            }
            int leafCount = 0;
            Vector3 cumulativeDirection = Vector3.zero;
            //iterate through each attractor
            for(int j=0; j< attractorSpawns.Count; j++)
            {
                //calculate the distance between branch and attratcor
                float distance = Vector3.Distance(_branchManager._branchList[i].transform.position, attractorSpawns[j].transform.position);

                //delete attractots that are too close
                if (distance < cullDistance)
                {
                    Destroy(attractorSpawns[j]);
                    attractorSpawns.RemoveAt(j);
                    continue;
                }
                //create branch in the direction of the attractor if in range
                else if (distance < minimumDistance) {
                    Vector3 direction = (attractorSpawns[j].transform.position - _branchManager._branchList[i].transform.position);
                    direction = direction.normalized;

                    cumulativeDirection += direction;
                    leafCount++;
                }

            }

            if (leafCount > 0)
            {
                Vector3 directionAverage = cumulativeDirection / leafCount;
                Vector3 position = _branchManager._branchList[i].transform.position + directionAverage;
                Vector3 angleDirection = -_branchManager._branchList[i].transform.right;

                Debug.DrawRay(position, directionAverage, Color.red, 1000);
                Quaternion quaternionDirection = Quaternion.LookRotation(Quaternion.AngleAxis(90, angleDirection) *  directionAverage, Vector3.up);
                _branchManager.AddBranch((Instantiate(_branchObject, position, quaternionDirection)));
            }
        }
        //empty the list at the end and delete unused attractors
        _branchManager._branchList.Clear();

        for (int i = 0; i < attractorSpawns.Count; i++)
        {
            //Destroy(attractorSpawns[i]);
        }
        //attractorSpawns.Clear();
    }
}
