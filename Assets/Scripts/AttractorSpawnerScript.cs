using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    private static float minimumDistance = 6.0f;
    private static float minimumCullDistance = 4.5f;

    [Header("Attraction")]
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
        if (attractorRange <= 0f)
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
        for (int i = 0; i < _branchManager._branchList.Count; i++)
        {
            int leafCount = 0;
            Vector3 cumulativeDirection = Vector3.zero;
            float distance = 0.0f;

            //iterate through each attractor
            for (int j = 0; j < attractorSpawns.Count; j++)
            {
                //calculate the distance between branch and attratcor
                distance = Vector3.Distance(_branchManager._branchList[i].transform.position, attractorSpawns[j].transform.position);

                //delete attractots that are too close
                if (distance < minimumCullDistance)
                {
                    Destroy(attractorSpawns[j]);
                    attractorSpawns.RemoveAt(j);
                    continue;
                }
                //create branch in the direction of the attractor if in range
                else if (distance < minimumDistance)
                {
                    Vector3 direction = (attractorSpawns[j].transform.position - _branchManager._branchList[i].transform.position);
                    direction = direction.normalized;

                    cumulativeDirection += direction;
                    leafCount++;
                }

            }

            //create the new branch in the direction of the average leaf
            if (leafCount > 0)
            {
                Vector3 directionAverage = cumulativeDirection / leafCount;
                Vector3 position = _branchManager._branchList[i].transform.position + _branchManager._branchList[i].transform.up * _branchManager._branchList[i].transform.localScale.y;
                Vector3 angleDirection = -_branchManager._branchList[i].transform.right;
                
                Quaternion quaternionDirection = Quaternion.LookRotation(Quaternion.AngleAxis(90, angleDirection) * directionAverage, Vector3.up);
                Vector3 scale = _branchManager._branchList[i].transform.localScale * 0.8f;
                if (scale.magnitude < 0.17f)
                {
                    scale = new Vector3(0.1f, 0.1f, 0.1f);
                }

                _branchManager.AddBranch(Instantiate(_branchObject, position, quaternionDirection));
                _branchObject.transform.localScale = scale;//(_branchManager._branchList[i].transform.localScale.x * 0.8f, _branchManager._branchList[i].transform.localScale.y * 0.8f, _branchManager._branchList[i].transform.localScale.z * 0.8f);

                CullAttractors(position);
            }
        }
    }

    public void ClearLists()
    {
        //empty the list at the end and delete unused attractors
        _branchManager._branchList.Clear();
        for (int i = 0; i < attractorSpawns.Count; i++)
        {
            Destroy(attractorSpawns[i]);
        }
        attractorSpawns.Clear();
    }

    private void CullAttractors(Vector3 branchPosition)
    {
        for (int j = 0; j < attractorSpawns.Count; j++)
        {
            //calculate the distance between branch and attratcor
            float distance = Vector3.Distance(branchPosition, attractorSpawns[j].transform.position);
            //delete attractots that are too close
            if (distance < minimumCullDistance)
            {
                Destroy(attractorSpawns[j]);
                attractorSpawns.RemoveAt(j);
                continue;
            }

        }
    }
}
