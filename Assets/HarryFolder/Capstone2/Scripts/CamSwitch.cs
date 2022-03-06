using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    [Header("Base Cam and Lerp Cam")]
    public Camera theCam; //Base cam we use
    private Camera lerpCam; //Duplicated cam for lerping
    public float distanceRemain;
    public GameObject targetCam;
    private Vector3 originalPos;
    private Quaternion originalQua;
    private Vector3 targetPos;
    private Quaternion targetQua;
    public float camSpeed;
    public bool inZoom = false;
    public bool zoomIn = true;
    public bool zoomOut = false;
    public bool lerping = false;
    public bool reset;
    public GameObject player;
    void Start()
    {
        targetPos = targetCam.transform.position;
        targetQua = targetCam.transform.rotation;
        originalPos = theCam.transform.position;
        originalQua = theCam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.T))
        {
            lerpCam = GameObject.Instantiate(theCam);//instantiate the duplicate cam at same pos and qua
            theCam.enabled = false;
            lerpCam.transform.position = theCam.transform.position;
            lerpCam.transform.rotation = theCam.transform.rotation;
            //theCam.gameObject.SetActive(false);//disable maincam
            //theCam.enabled = false;
            //lerp
            zoomIn = true;
            zoomOut = false;
            player.SetActive(false);
            //theCam.GetComponent<CamScript>().enabled = false;
            lerpCam.GetComponent<CamScript>().enabled = false;
            originalPos = lerpCam.transform.position;
            originalQua = lerpCam.transform.rotation;
        }

        if (zoomIn && ! zoomOut)
        {
            ZoomIn();
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            zoomOut = true;
            zoomIn = false;
        }
        if (!zoomIn && zoomOut)
        {
            ZoomOut();
            player.SetActive(true);
            if (Vector3.Distance(theCam.transform.position, lerpCam.transform.position) < distanceRemain)
            {
                
                reset = true;
            }
        }

        if (reset)
        {
            theCam.enabled = true;
            lerpCam.enabled = false;
            Destroy(lerpCam.gameObject);
            theCam.enabled = true;
            Destroy(this);
        }
            
            
        
        
    }

    public void ZoomIn()
    {
        lerpCam.transform.position = Vector3.Lerp(lerpCam.transform.position, targetPos, camSpeed * Time.deltaTime);
        lerpCam.transform.rotation = Quaternion.Lerp(lerpCam.transform.rotation, targetQua, camSpeed * Time.deltaTime);
    }

    public void ZoomOut()
    {
        lerpCam.transform.position = Vector3.Lerp(lerpCam.transform.position, originalPos, camSpeed * Time.deltaTime);
        lerpCam.transform.rotation = Quaternion.Lerp(lerpCam.transform.rotation, originalQua, camSpeed * Time.deltaTime);

    }
    
}
