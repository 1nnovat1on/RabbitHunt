using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeSwitcher : MonoBehaviour
{
    GameObject Models;
    GamePad test;

    void Start()
    {
        Models = GameObject.Find("3DModels");

    }

    void update()
    {

    }

    void ChangeActiveCharacter()
    {
        int index = -1;
        
        for(int i = 0; i < Models.transform.childCount; i++)
        {
            
            test = (GamePad) Models.transform.GetChild(i).GetComponent(typeof(GamePad));
            
            if (test.setGamepadControl == true)
            {
                test.setGamepadControl = false;
                index = i;
            }
        }

        //check if we did indeed find a true value
        if(index != -1)
        {
            //unless we are at the last index, make the consecutive child active
            if (index < Models.transform.childCount)
            {
                test = (GamePad) Models.transform.GetChild(index + 1).GetComponent(typeof(GamePad));
            }

        }
        
        //if did not yet loop or we are at the last child, just enable the first character in the list of children
        else
        {
            test = (GamePad) Models.transform.GetChild(0).GetComponent(typeof(GamePad));
        }
        
        if(test != null)
        {
            test.setGamepadControl = true;
        }
        
        
    }
}
