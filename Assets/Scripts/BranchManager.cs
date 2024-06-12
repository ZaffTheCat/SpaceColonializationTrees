using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    public List<GameObject> _branchList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBranch(GameObject brachGameObject)
    {
        _branchList.Add(brachGameObject);
    }
}
