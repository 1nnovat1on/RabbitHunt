using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camCon : MonoBehaviour
{
    public GameObject player;
    public float sensitivity;
    public RectTransform rt;
    public Vector3 centerPt;
    public float radius;
 
    void Start()
    {
        rt = GetComponent<RectTransform> ();
        centerPt = rt.anchoredPosition;

    }
     void FixedUpdate ()
     {
        float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");
        transform.Rotate(transform.up * rotateHorizontal * sensitivity) ;
        transform.Rotate(transform.right * rotateVertical * sensitivity) ;

        Vector3 position = new Vector3 (rt.anchoredPosition.x, rt.anchoredPosition.y, 0);
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 newPos = position + movement;
        Vector3 offset = newPos - centerPt;
        rt.anchoredPosition = centerPt + Vector3.ClampMagnitude(offset, radius);
     }
}

