using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPress : MonoBehaviour
{
    //  public GameObject myObject;
    public bool isMoving = false;
    public GameObject myCamera;
    public Vector3 targetPosition;
    public Vector3 oldTargetPosition = new Vector3(0, 0, 0);
    public Quaternion targetAngle = Quaternion.Euler(0, 0, 0);
    public Quaternion oldTargetAngle;
    public GameObject currentGameObject;
    public GameObject person;
    void Start()
    {
        myCamera = GameObject.Find("Main Camera");
        person = GameObject.Find("Person");
    }

    void Update()
    {
        if (isMoving == true)
        {
            person.transform.position = Vector3.Lerp(person.transform.position, targetPosition, Time.deltaTime * 1);
            person.transform.rotation = Quaternion.Slerp(person.transform.rotation, targetAngle, Time.deltaTime * 1);
            myCamera.transform.rotation = Quaternion.Slerp(person.transform.rotation, targetAngle, Time.deltaTime * 1);
            if (Mathf.Abs(person.transform.position.x - targetPosition.x) < 1 && Mathf.Abs(person.transform.position.z - targetPosition.z) < 1)
            {
                currentGameObject.GetComponent<CubeScript>().myObject.transform.gameObject.SetActive(true);
                isMoving = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            bool condition = Physics.Raycast(ray, out rayHit);
            if (condition && rayHit.transform.gameObject.name == "CubePrefab(Clone)" && rayHit.transform.GetComponent<CubeScript>().myObject!=null)
            {
                if (currentGameObject != null)
                    currentGameObject.GetComponent<CubeScript>().myObject.transform.gameObject.SetActive(false);
                currentGameObject = rayHit.transform.gameObject;
                oldTargetPosition = new Vector3(person.transform.position.x, person.transform.position.y, person.transform.position.z);
                oldTargetAngle = Quaternion.Euler(person.transform.rotation.eulerAngles);
                // if (rayHit.transform.eulerAngles.y == 180.0f)
                // {
                    Vector3 backward=rayHit.transform.gameObject.transform.forward;
                    // targetPosition = new Vector3(rayHit.transform.position.x, 0, rayHit.transform.position.z - 7);
                    // targetPosition=new Vector3(rayHit.transform.gameObject.transform.position.x,0,rayHit.transform.gameObject.transform.position.z-8);
                    targetPosition=new Vector3(rayHit.transform.position.x,0,rayHit.transform.position.z)+7*new Vector3(backward.x,0,backward.z);
                // }
                // else if(rayHit.transform.eulerAngles.y == 270.0f){

                // }
                // else
                // {
                //     targetPosition = new Vector3(rayHit.transform.transform.position.x, 0, rayHit.transform.transform.position.z + 7);
                // }
                Debug.Log("   " + rayHit.transform.transform.rotation.eulerAngles);
                targetAngle = Quaternion.Euler(0, rayHit.transform.transform.rotation.eulerAngles.y - 180, 0);
                isMoving = true;
                FollowPlayer.setIsFrozen();
                PlayerControls.setIsFrozen();
                Debug.Log(rayHit.transform.gameObject.name);
            }
            if (condition == false || rayHit.transform.gameObject.name != "CubePrefab(Clone)")
            {
                resetPositionAndAngle();
            }
        }
    }
    void resetPositionAndAngle()
    {
        Debug.Log("Resetted");
        if (oldTargetPosition != new Vector3(0, 0, 0) && oldTargetAngle != Quaternion.Euler(0, 0, 0))
        {
            person.transform.position = new Vector3(oldTargetPosition.x, oldTargetPosition.y, oldTargetPosition.z);
            person.transform.rotation = Quaternion.Euler(oldTargetAngle.eulerAngles);
            myCamera.transform.rotation = Quaternion.Euler(oldTargetAngle.eulerAngles);
        }
        oldTargetPosition = new Vector3(0, 0, 0);
        targetAngle = Quaternion.Euler(0, 0, 0);
        if (currentGameObject != null)
            currentGameObject.GetComponent<CubeScript>().myObject.transform.gameObject.SetActive(false);
        FollowPlayer.clearIsFrozen();
        PlayerControls.clearIsFrozen();
    }
}
