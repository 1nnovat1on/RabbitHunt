using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePad : MonoBehaviour
{
    //I remember my dream
    Transform ObjectsTransform;
    Rigidbody ObjectsRigidbody;

    float yAxisLimiter;
    bool yAxisLimit = false;
    void FixedUpdate()
    {   
        ObjectsTransform = GetComponent<Transform>();
             
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        //each time the stick is actuated (rotated/moved) get the read on the distance and direction from origin 
        //and move the camera in the direction as fast as a velocity multiplied by the magnitude of the distance (the solution is vectors)
        if (gamepad.rightStick.IsActuated())
        {
            //xy coords of stick position on a unity circle plane
            Vector2 RstickPosition = gamepad.rightStick.ReadValue();
            Vector3 rotationVector = new Vector3(-RstickPosition.y, RstickPosition.x, 0f);
            
            Debug.Log("Right Stick Moved to " + RstickPosition.ToString() );   
            //rotate works around the object's axis(local), while eulerAngles works around the global axis       
            //ObjectsTransform.eulerAngles = new Vector3((ObjectsTransform.position.z - (RstickPosition.y * 150)), (ObjectsTransform.rotation.y + (RstickPosition.x * 150)), ObjectsTransform.rotation.z);
            //ObjectsTransform.Rotate((RstickPosition.x * 100), (RstickPosition.y * 100), 0);

            
            //but in the end, we use rotate, which will rotate our object in a vector direction while the stick is actuated
            ObjectsTransform.Rotate(rotationVector, 2f, Space.Self);
            
            //stabilize the z axis at 0
            Quaternion currentRotation = ObjectsTransform.rotation;
            if (currentRotation.eulerAngles.x > 87 && currentRotation.eulerAngles.x < 89)
            {
                
                if (yAxisLimit != true)
                {
                    Debug.Log("ACTIVATE");
                    yAxisLimiter = currentRotation.eulerAngles.y;
                    yAxisLimit = true;
                }
                currentRotation.eulerAngles = new Vector3(87, yAxisLimiter, 0);
            }

            yAxisLimit = false;
            
            currentRotation.eulerAngles = new Vector3(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0);
            //currentRotation.eulerAngles = new Vector3(ObjectsTransform.rotation.x, ObjectsTransform.rotation.y, 0);
            ObjectsTransform.rotation = currentRotation;
        }


        if (gamepad.leftStick.IsActuated())
        {
            //xy coords of stick position on a unity circle plane
            Vector2 LstickPosition = gamepad.leftStick.ReadValue();
            Vector3 travelVector = new Vector3(LstickPosition.x, 0f, LstickPosition.y);
            Debug.Log("Left Stick Moved to " + LstickPosition.ToString() );      
            //ObjectsTransform.position = new Vector3(ObjectsTransform.position.x, ObjectsTransform.position.y, ObjectsTransform.position.z + (LstickPosition.x * .1f));
            //ObjectsTransform.Translate(0, 0, LstickPosition.x * 1f);
            //ObjectsRigidbody.velocity = new Vector3(ObjectsRigidbody.velocity.x, ObjectsRigidbody.velocity.y, ObjectsRigidbody.velocity.z + (LstickPosition.x * .1f));
            ObjectsTransform.Translate( travelVector * .5f);
            
        }

    }

    IEnumerable coroutine()
    {
        yield return new WaitForSeconds(1f);
    }

    void searchForInput()
    {

    }




}
