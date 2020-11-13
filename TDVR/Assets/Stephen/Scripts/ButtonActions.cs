using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/////////////////////////////////////////////////////////////////////////////
// 
// ButtonActions describes the actions that can be assigned to GUI buttons
// in a canvas.  To link an action to a button, add the button to the list
// "Button Map" and select the desired action from the dropdown.  
//
// Note: Until the TODO on OpenPane is completed, this script must be
// added to the pane containing the buttons it describes.
// 
/////////////////////////////////////////////////////////////////////////////
public class ButtonActions : MonoBehaviour
{
    // List of available functions to attach to buttons.
    // This provides a dropdown of function names in the inspector.
    enum Act {ChangeScene, OpenPane, Exit};
    // Maps the enumerator to its associated function
    private Dictionary<Act, UnityAction> actionMap = new Dictionary<Act, UnityAction>();

    // Struct containing a button-to-action pairing.
    // This is necessary because dictionaries are not serializable.
    // TODO: Suppress warning that the members of the struct are never assigned to
    [System.Serializable]
    struct ButtonActionPair {
        public Button button;
        public Act action;
    }

    // List mapping buttons and actions in the inspector
    [SerializeField] private ButtonActionPair []buttonMap = null;
    // the pane to open on call of OpenPane()
    [SerializeField] private Canvas paneToOpen = null;
    // the name of the scene to switch to on call of ChangeScene()
    [SerializeField] private string sceneName = "";

    void Start()
    {
        PopulateDict();
        // adds the selected functions to their buttons as listeners.
        foreach (ButtonActionPair b in buttonMap)
            b.button.onClick.AddListener(actionMap[b.action]);
    }

    // Switches to the scene specified in sceneName
    // TODO: Remove need for global sceneName variable - add it as parameter
    void ChangeScene()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the play button!");
        SceneManager.LoadSceneAsync(sceneName);
    }

    // Disable the current pane and enable a new pane, specified in paneToOpen
    // TODO: Remove need for global paneToOpen variable - add it as parameter
    void OpenPane()
    {
        //Output this to console when the Button2 is clicked
        Debug.Log("You have clicked the options button!");
        paneToOpen.enabled = true;
        GetComponent<Canvas>().enabled = false;
    }

    // Quit the game.
    void Exit()
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("You have clicked the exit button!");
        Application.Quit();
    }

    // Associates the enumerators to their functions
    void PopulateDict() {
        actionMap.Add(Act.ChangeScene, ChangeScene);
        actionMap.Add(Act.OpenPane, OpenPane);
        actionMap.Add(Act.Exit, Exit);
    }
}
