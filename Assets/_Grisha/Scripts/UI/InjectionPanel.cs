using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InjectionPanel : MonoBehaviour
{
    [SerializeField] Button injectionButton;
    [SerializeField] TextMeshProUGUI injectionsNumber;
    void RefreshInjectionsNumber(int newValue)
    {
        injectionsNumber.SetText(sourceText: $" {newValue}/7");
    }
    void Inject()
    {
        ShipEventsBus.HealingShipMember?.Invoke();
    }
    void OnEnable()
    {
        injectionButton.onClick.AddListener(Inject);
        ShipEventsBus.ShowInjectionsNumberOnUI += RefreshInjectionsNumber;
    }
    void OnDisable()
    {
        ShipEventsBus.ShowInjectionsNumberOnUI -= RefreshInjectionsNumber;
    }
}