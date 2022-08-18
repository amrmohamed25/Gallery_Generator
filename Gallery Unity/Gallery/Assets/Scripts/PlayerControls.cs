using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update
    static bool isFrozen = false;
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
        int speed=2;
        if (isFrozen == false)
        {
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");
            transform.RotateAround(transform.position, -Vector3.up, -rotateHorizontal * 2);

            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(5f * Time.deltaTime*speed, 0.0f, 0.0f, Space.Self);
            }
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-5f * Time.deltaTime*speed, 0.0f, 0.0f, Space.Self);

            }
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0.0f, 0.0f, 5f * Time.deltaTime*speed, Space.Self);

            }
            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0.0f, 0.0f, -5f * Time.deltaTime*speed, Space.Self);

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame.quitGame();
        }
    }
}
