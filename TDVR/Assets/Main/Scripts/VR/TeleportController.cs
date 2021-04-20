using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    private XRRayInteractor rightRayInteractor;
    private XRRayInteractor leftRayInteractor;

    // Start is called before the first frame update
    void Start()
    {
        // get controllers
        if (rightTeleportRay)
            rightRayInteractor = rightTeleportRay.gameObject.GetComponent<XRRayInteractor>();
        if (leftTeleportRay)
            leftRayInteractor = leftTeleportRay.gameObject.GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        // turn on teleport ray if active
        if (leftTeleportRay)
        {
            bool active = CheckIfActivated(leftTeleportRay);
            leftRayInteractor.allowSelect = active;
            leftTeleportRay.gameObject.SetActive(active);
        }

        if (rightTeleportRay)
        {
            bool active = CheckIfActivated(rightTeleportRay);
            rightRayInteractor.allowSelect = active;
            rightTeleportRay.gameObject.SetActive(active);
        }
    }

    // CheckIfActivated gets input from the controller to tell if the teleport button is activated
    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }

    // SetRotation is called upon teleport to set the player's "neutral" facing towards the front of the teleport pad
    public void SetRotation(Transform reference)
    {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, reference.eulerAngles.y, 0));
        FindObjectOfType<AudioManager>().Play("Teleport"); // TELEPORT SOUND
    }
}
