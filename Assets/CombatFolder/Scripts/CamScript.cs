using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 initialOffset;
    private Vector3 cameraPosition;
    public float smoothness;
    [Header("Type A")]
    public CamConstrainStruct livingRoom;
    public bool inLivingRoom;
    [Header("Type B (deactivate it by removing ref for enemy)")]
    public GameObject enemy;
    public float rot_spd;

    void Start()
    {
        initialOffset = transform.position - targetObject.position;
    }

    void Update()
    {
        cameraPosition = targetObject.position + initialOffset;
        // when in living room
        if (inLivingRoom)
		{
            cameraPosition = new Vector3(Mathf.Clamp(cameraPosition.x, livingRoom.xMin, livingRoom.xMax),
            cameraPosition.y,
            Mathf.Clamp(cameraPosition.z, Mathf.Max(transform.position.x + livingRoom.z_accordingToX_min, livingRoom.zMin), Mathf.Min(transform.position.x + livingRoom.z_accordingToX_max, livingRoom.zMax)));
        }
		transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.deltaTime);

        // look at
        if (enemy != null)
		{
            var target = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
            float x = Mathf.Clamp(transform.rotation.eulerAngles.x, 40f, 50f);
            transform.rotation = Quaternion.Euler(x, 0, 0);
        }
    }
}
