using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MotherController : MonoBehaviour
{
    public GameObject[] kidsHouse;
    public List<GameObject> kids_House;
    public List<GameObject> kids;
    //public GameObject []kids;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemy>().health <= 0)
		{
			foreach (var bear in kidsHouse)
			{
                bear.GetComponent<SmallBear>().health = 0;
			}
		}
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
        print("out kids()");
		for (int i = (GetComponent<Enemy>().changeLimit) * 3; i < (GetComponent<Enemy>().changeLimit) * 3 + 3; i++)
		{
            print("send out kid: "+kidsHouse[i].name);
            GameObject kidChild = kidsHouse[i].gameObject;
            kids.Add(kidChild);
            kidChild.GetComponent<Kids>().enabled = true;
            kidChild.GetComponent<SmallBear>().enabled = true;
            kidChild.GetComponent<AIController>().enabled = true;
            kidChild.GetComponent<EffectHoldersHolderScript>().enabled = true;
            kidChild.transform.parent = null;
            kidChild.GetComponent<Rigidbody>().useGravity = true;
            kidChild.GetComponent<CapsuleCollider>().enabled = true;
            kidChild.GetComponent<SmallBear>().myEntrances = new List<DoorScript>(GetComponent<Enemy>().myEntrances);
            //kidChild.GetComponent<Enemy>().ChangePhase(Enemy.AIPhase.InBattle1, 1);
            //kidsHouse[i] = null;
        }
  //foreach (GameObject kid in kidsHouse)
        //{
        //    GameObject kidChild = kid.transform.GetChild(0).gameObject;
        //    kids.Add(kidChild);
        //    //kidChild.gameObject.SetActive(true);
        //    kidChild.GetComponent<Kids>().enabled = true;
        //    kidChild.GetComponent<SmallBear>().enabled = true;
        //    kidChild.GetComponent<AIController>().enabled = true;
        //    kidChild.transform.parent = null;
        //    kidChild.GetComponent<Rigidbody>().useGravity = true;
        //    kidChild.GetComponent<CapsuleCollider>().enabled = true;
        //    kidChild.GetComponent<Enemy>().ChangePhase(Enemy.AIPhase.InBattle1, 1);
        //    //kidChild.GetComponent<Enemy>().EnemyCanvas.SetActive(true);
        //}
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
            //kids[i].transform.position = Vector3.Lerp(kids[i].transform.position, kidsHouse[i].transform.position, Time.time);
            //kids[i].SetActive(false);
        }
    }

}
