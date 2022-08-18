using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject myObject;
    // Start is called before the first frame update
    static bool isFrozen = false;

    public static Vector3 targetPosition;
    void Start()
    {
        // FollowPlayer.isFrozen=false;
    }
    public static void setIsFrozen()
    {
        isFrozen = true;
    }
    public static void clearIsFrozen()
    {
        isFrozen = false;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position=myObject.transform.position;
        // trans


        transform.position = new Vector3(myObject.transform.position.x, (int)myObject.transform.position.y + 3, myObject.transform.position.z);
        if (isFrozen == false)
        {
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");

            transform.RotateAround(transform.position, -Vector3.up, -rotateHorizontal * 2); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
            transform.RotateAround(Vector3.zero, transform.right, -rotateVertical * 2);

        }

    }
}
