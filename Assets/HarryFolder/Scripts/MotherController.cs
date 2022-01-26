using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class MotherController : MonoBehaviour
{
    public GameObject[] kidsHouse;
    public List<GameObject> kids;
    //public GameObject []kids;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject kid in kidsHouse)
            {
                GameObject kidChild = kid.transform.GetChild(0).gameObject;
                kids.Add(kidChild);
                kidChild.gameObject.SetActive(true);
                kidChild.transform.parent = null;
                kidChild.GetComponent<Rigidbody>().useGravity = true;
                kidChild.GetComponent<CapsuleCollider>().enabled = true;
                kidChild.GetComponent<Kids>().enabled = true;
            }
            
            
            
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < kids.Count-1; i++)
            {
                kids[i].transform.SetParent(kidsHouse[i].transform);
                kids[i].GetComponent<Rigidbody>().useGravity = false;
                kids[i].GetComponent<CapsuleCollider>().enabled = false;
                kids[i].GetComponent<Kids>().isActivated = false;
                kids[i].GetComponent<Kids>().timer = 0f;
                kids[i].GetComponent<Kids>().enabled = false;
                kids[i].GetComponent<NavMeshAgent>().enabled = false;
                kids[i].GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                kids[i].GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, 0);
                kids[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
                kids[i].transform.position = Vector3.Lerp(kids[i].transform.position, kidsHouse[i].transform.position, Time.time);
            }
            
        }*/
    }

    public void OutKids()
    {
        foreach (GameObject kid in kidsHouse)
        {
            GameObject kidChild = kid.transform.GetChild(0).gameObject;
            kids.Add(kidChild);
            kidChild.gameObject.SetActive(true);
            kidChild.GetComponent<Kids>().enabled = true;
            kidChild.transform.parent = null;
            kidChild.GetComponent<Rigidbody>().useGravity = true;
            kidChild.GetComponent<CapsuleCollider>().enabled = true;
            kidChild.GetComponent<Enemy>().ChangePhase(Enemy.AIPhase.InBattle1, 1);
            //kidChild.GetComponent<Enemy>().EnemyCanvas.SetActive(true);
        }
    }

    public void BackKids()
    {
        for (int i = 0; i < kids.Count; i++)
        {
            kids[i].GetComponent<Enemy>().EnemyCanvas.SetActive(false);
            kids[i].GetComponent<SmallBear>().ResetSmallBear();
            kids[i].transform.SetParent(kidsHouse[i].transform);
            kids[i].GetComponent<Rigidbody>().useGravity = false;
            kids[i].GetComponent<CapsuleCollider>().enabled = false;
            kids[i].GetComponent<Kids>().isActivated = false;
            kids[i].GetComponent<Kids>().timer = 0f;
            kids[i].GetComponent<Kids>().enabled = false;
            /*kids[i].GetComponent<NavMeshAgent>().enabled = false;
            kids[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            kids[i].GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, 0);
            kids[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);*/
            kids[i].transform.position = Vector3.Lerp(kids[i].transform.position, kidsHouse[i].transform.position, Time.time);
            kids[i].SetActive(false);
        }
    }

}
