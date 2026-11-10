using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
   // private Vector3 mouseVelocity;

    //private Vector3 lastMousePosition;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; //z coord of parent object
        mOffset = gameObject.transform.position - GetMoiseWorldPos(); // screen pos to world pos

        //lastMousePosition = Input.mousePosition; //initialise first pos

    }
    private Vector3 GetMoiseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition; //x,y

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMoiseWorldPos() + mOffset;

        /*if (Time.deltaTime > 0f) //no divide by 0 
        {
            mouseVelocity = (Input.mousePosition - lastMousePosition) / Time.deltaTime; //calc mouse velocity
        }
        lastMousePosition = Input.mousePosition; //current pos for next frame */
    }

    private void OnMouseUp()
    {
        //TODO
        /*
        Vector3 throwDirection = (Camera.main.transform.forward * mouseVelocity.y + Camera.main.transform.right * mouseVelocity.x).normalized;

        float finalSpeed = Mathf.Min(mouseVelocity.magnitude, 50f); 

        GetComponent<Rigidbody>().velocity = throwDirection * finalSpeed; */
    }

}
