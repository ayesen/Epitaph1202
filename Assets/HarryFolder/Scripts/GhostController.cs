using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public NavMeshAgent ghostRider;

    public GameObject target;
    // Update is called once per frame
    void Update()
    {
        ghostRider.SetDestination(target.transform.position);
    }
}
