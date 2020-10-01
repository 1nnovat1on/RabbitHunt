//Copyright Colin Jackson 2020 October 1st - Happy Spookfest

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class activeSwitcher : MonoBehaviour
{
    static  GameObject Models;
    static GamePad CurrentChild;
    bool OneTimer = true;

    void Start()
    {
        Models = GameObject.Find("3DModels");

    }

    void Update()
    {
        var gamepad = Gamepad.current;
            
        //if there's no gamepad connected
        if (gamepad == null)
        {
            return;
        } 

        if (gamepad.rightTrigger.wasPressedThisFrame && OneTimer == true)
        {
            Debug.Log("Right trigger pressed");
            ChangeActiveCharacter();
            OneTimer = false;
        }

        if (gamepad.rightTrigger.wasReleasedThisFrame && OneTimer == false)
        {
            Debug.Log("Right trigger released");
            OneTimer = true;
        }

    }

    public static void ChangeActiveCharacter()
    {
        int index = -1;
        
        for(int i = 0; i < Models.transform.childCount; i++)
        {
            
            CurrentChild = (GamePad) Models.transform.GetChild(i).GetComponent(typeof(GamePad));
            
            if (CurrentChild.setGamepadControl == true)
            {
                CurrentChild.setGamepadControl = false;
                index = i;
            }
        }

        //check if we did indeed find a true value
        if(index != -1 && index < Models.transform.childCount - 1)
        {
            //unless we are at the last index, make the consecutive child active
            if (index < Models.transform.childCount)
            {
                CurrentChild = (GamePad) Models.transform.GetChild(index + 1).GetComponent(typeof(GamePad));
                Debug.Log("Activated " + CurrentChild.ToString() + " - Child " + (index + 2) + " out of " + Models.transform.childCount);
            }

        }
        
        //if did not yet loop or we are at the last child, just enable the first character in the list of children
        else
        {
            CurrentChild = (GamePad) Models.transform.GetChild(0).GetComponent(typeof(GamePad));
            Debug.Log("Activated " + CurrentChild.ToString() + " - Child 1 out of " + Models.transform.childCount);
        }
        
        if(CurrentChild != null)
        {
            CurrentChild.setGamepadControl = true;
        }
        
        
    }
}
