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
    enum Act {ChangeScene, OpenPane, Exit, NextWave};
    // Maps the enumerator to its associated function
    private Dictionary<Act, UnityAction> actionMap = new Dictionary<Act, UnityAction>();

    // Struct containing a button-to-action pairing.
    // This is necessary because dictionaries are not serializable.
    // TODO: Suppress warning that the members of the struct are never assigned to
    [System.Serializable]
    struct ButtonActionPair {
        public Button button;
        public Act action;
        public string sceneName;
    }

    // List mapping buttons and actions in the inspector
    [SerializeField] private ButtonActionPair []buttonMap = null;
    // the pane to open on call of OpenPane()
    [SerializeField] private Canvas paneToOpen = null;
    // the name of the scene to switch to on call of ChangeScene()
    [SerializeField] private WaveSpawner wave = null;

    void Start()
    {
        PopulateDict();
        // adds the selected functions to their buttons as listeners.
        foreach (ButtonActionPair b in buttonMap) {
            if (b.action == Act.ChangeScene) {
                b.button.onClick.AddListener(() => ChangeScene(b.sceneName));
            } else {
                b.button.onClick.AddListener(actionMap[b.action]);
            }
        }

    }

    // Switches to the scene specified in sceneName
    // TODO: Remove need for global sceneName variable - add it as parameter
    void ChangeScene(string sceneName)
    {
        Debug.Log("Changing to scene \'" + sceneName + "\'");
        SceneManager.LoadSceneAsync(sceneName);
    }

    // Disable the current pane and enable a new pane, specified in paneToOpen
    // TODO: Remove need for global paneToOpen variable - add it as parameter
    void OpenPane()
    {
        if (paneToOpen != null) {
            Debug.Log("Opening the pane \'" + paneToOpen.name + "\'");
            paneToOpen.enabled = true;
            GetComponent<Canvas>().enabled = false;
        } else {
            Debug.Log("Could not open pane; null reference \'paneToOpen\'");
        }
    }

    // Quit the game.
    void Exit()
    {
        Debug.Log("You have clicked the exit button!");
        Application.Quit();
    }

    // Advance to the next wave
    void NextWave() 
    {
        if (wave != null) {
            Debug.Log("Advancing to the next wave");
            StartCoroutine(wave.SpawnWave());
            GetComponent<Canvas>().enabled = false;
        } else {
            Debug.Log("Could not open pane; null reference \'paneToOpen\'");
        }
    }

    // Associates the enumerators to their functions
    void PopulateDict() {
        actionMap.Add(Act.OpenPane, OpenPane);
        actionMap.Add(Act.Exit, Exit);
        actionMap.Add(Act.NextWave, NextWave);
    }
}
