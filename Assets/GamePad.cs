//Copyright Colin Jackson 9/25/2020

using UnityEngine;
using UnityEngine.InputSystem;

public class GamePad : MonoBehaviour
{
    Transform ObjectsTransform;
   
    void FixedUpdate()
    {   
        ObjectsTransform = GetComponent<Transform>();
             
        var gamepad = Gamepad.current;
        
        //if there's no gamepad connected
        if (gamepad == null)
        {
            return;
        }    

        //rotation
        if (gamepad.rightStick.IsActuated())
        {
            //xy coords of stick position on a unity square plane
            Vector2 RstickPosition = gamepad.rightStick.ReadValue();
            Vector3 rotationVector = new Vector3(-RstickPosition.y, RstickPosition.x, 0f);
            
            Debug.Log("Right Stick Moved to " + RstickPosition.ToString() );       
            
            //in the end, we use rotate, which will rotate our object in a vector direction while the stick is actuated
            ObjectsTransform.Rotate(rotationVector, 2f, Space.Self);
            
            Quaternion currentRotation = ObjectsTransform.rotation;
            
            //clamp: the angle to never reach x = 90 degrees, to avoid the chaotic effect of gimbal lock OH LEGENDARY GIMBAL LOCK, I HAVE DEFEATED YOU THO STILL YOU ARE AT YOUR CORE A MATHEMATICAL MYSTERY TO ME
            if (currentRotation.eulerAngles.x > 75 && currentRotation.eulerAngles.x < 89)
            {   
                //the user will be locked at 7 degrees but still able to affect the y axis. The speed of rotation of the y axis at this limit is relative to the size of x value   
                currentRotation.eulerAngles = new Vector3(75, currentRotation.eulerAngles.y, 0);
            }
            
            //clamp: same for x reflected across y axis
            //because of euler angle calculation in the engine, translate the angles into a difference from 0 or 360
            float difference = Mathf.DeltaAngle(0, ObjectsTransform.eulerAngles.x);

            if (difference < -75f && difference > -89f)
            {   //the user will be locked at 75 degrees but still able to affect the y axis. The speed of rotation of the y axis at this limit is relative to the size of x value  
                currentRotation.eulerAngles = new Vector3(-75, currentRotation.eulerAngles.y, 0);
            }
            
            //stabilize the z axis at 0
            currentRotation.eulerAngles = new Vector3(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0);
            
            ObjectsTransform.rotation = currentRotation;
        }

        //position
        //each time the stick is actuated (rotated/moved) get the read on the distance and direction from origin 
        //and move the camera in the direction as fast as a velocity multiplied by the magnitude of the distance (the solution is vectors)
        if (gamepad.leftStick.IsActuated())
        {
            //xy coords of stick position on a unity square plane
            Vector2 LstickPosition = gamepad.leftStick.ReadValue();
            Vector3 travelVector = new Vector3(LstickPosition.x, 0f, LstickPosition.y);
            
            Debug.Log("Left Stick Moved to " + LstickPosition.ToString() );      
            
            ObjectsTransform.Translate( travelVector * .5f);     
        }

    }


}
