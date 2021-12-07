using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kids : MonoBehaviour
{
    Transform selfPose;
    private float xRotation;
    private float yRotation;
    private float zRotation;
    private float lerpSpeed;
    public bool isActivated;
    public Transform thisParent;
    public float timer;
    private NavMeshAgent self;
    private GameObject target;
    private Rigidbody rB;

    void Start()
    {
        selfPose = this.transform;
        xRotation = selfPose.rotation.x;
        yRotation = selfPose.rotation.y;
        zRotation = selfPose.rotation.z;
        lerpSpeed = 0.5f;
        timer = 0f;
        target = GameObject.Find("Player");
        rB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player");
        if (this.transform.parent == null && !isActivated)
        {
            print("got no papa");
            timer += Time.deltaTime;
            if (timer >=2f)
            {
                self = this.gameObject.GetComponent<NavMeshAgent>();
                self.enabled = true;
                self.Warp(transform.position);
                isActivated = true;

            }

        }
        /*if (isActivated)
        { 
          selfPose.rotation = Quaternion.identity;
          if (xRotation != 0 || yRotation != 0 || zRotation != 0)
          {
              transform.rotation = Quaternion.Lerp(selfPose.rotation, new Quaternion(0,0,0,0), lerpSpeed * Time.deltaTime);
              print("rotated");
          }
          
          print("warped");
          //self.SetDestination(target.transform.position);
        } */
        
    }
}
