using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControl : MonoBehaviour
{
    
    public float spd;
    public float rot_spd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var target = new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
        }
        // walking in one axis
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
        }
        
    }
}
