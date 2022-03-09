using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelManager : MonoBehaviour
{
    public GameObject Door;
    public List<GameObject> Triggers = new List<GameObject>();
    public bool canOpen = false;
    public GameObject Enemy;

    void Start()
    {
        
    }


    void Update()
    {
        for (int i = 0; i < Triggers.Count; i++)
        {
            canOpen = Triggers[i].GetComponent<InteractableObjectScript>().triggerOn;

        }


        if(this.name == "Storage")
        {
            if(Enemy.GetComponent<Enemy>().doorTrigger)
            {
                canOpen = true;
            }
        }

        
        if(canOpen == true)
        {
            Door.GetComponent<DoorScript>().ControllDoor();
        }

    }
}
