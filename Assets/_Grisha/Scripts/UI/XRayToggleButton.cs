using UnityEngine;
using UnityEngine.UI;

public class XRayToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject xRayObject;
    [SerializeField] private Toggle xRaySwitch;

    private void OnEnable()
    {
        xRaySwitch.onValueChanged.AddListener(ToggleXRay);
        ShipEventsBus.ResettingPanel += TurnOffXRay;
    }

    private void TurnOffXRay()
    {
        xRaySwitch.isOn = false;
        xRayObject.SetActive(false);
    }

    private void OnDisable()
    {
        xRaySwitch.onValueChanged.RemoveListener(ToggleXRay);
        ShipEventsBus.ResettingPanel -= TurnOffXRay;
    }

    private void ToggleXRay(bool value)
    {
        xRayObject.SetActive(value);
    }
}
