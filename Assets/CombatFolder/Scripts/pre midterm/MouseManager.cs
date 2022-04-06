using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    static public MouseManager me;
    public Vector3 mousePos;
    public LayerMask ignoreMe;
    public bool slctedEnemy = false;
    public GameObject enemySelected;
    public bool showCursor;

    void Start()
    {
        Cursor.visible = showCursor;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Awake()
	{
        me = this;
	}

	private void Update()
	{
        // STEP 1: declare a ray, use mouse's screenspace pixel coordinate
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // STEP 2: declare mouse ray distance
        float mouseRayDist = 10000f;

        // STEP 2B: declare a blank RaycastHit variable
        RaycastHit rayHit = new RaycastHit();

        // STEP 3: debug draw the raycast
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * mouseRayDist, Color.yellow);

        // STEP 4: shoot the raycast
        if (Physics.Raycast(mouseRay, out rayHit, mouseRayDist, ~ignoreMe))
        {
            mousePos = rayHit.point;

            //Detect the layer hits enemy or not, and set the public bool
            if(rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                slctedEnemy = true;
                enemySelected = rayHit.transform.gameObject;
            }
            else
            {
                slctedEnemy = false;
                enemySelected = null;
            }
        }
    }
}
