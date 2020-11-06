using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
    }

    void ShiftScene()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the play button!");
    }

    void OptionsPane()
    {
        //Output this to console when the Button2 is clicked
        Debug.Log("You have clicked the options button!");
    }

    void Exit()
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("You have clicked the exit button!");
    }
}
