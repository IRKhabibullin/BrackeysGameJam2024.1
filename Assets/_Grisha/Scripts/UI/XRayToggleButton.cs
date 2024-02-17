using UnityEngine;
using UnityEngine.UI;

public class XRayToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject xRayObject;
    [SerializeField] private Toggle xRaySwitch;

    private void OnEnable()
    {
        xRaySwitch.onValueChanged.AddListener(ToggleXRay);
    }
    private void OnDisable()
    {
        xRaySwitch.onValueChanged.RemoveListener(ToggleXRay);
    }

    private void ToggleXRay(bool value)
    {
        xRayObject.SetActive(value);
    }
}
