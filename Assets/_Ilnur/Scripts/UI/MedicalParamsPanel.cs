using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MedicalParamsPanel : MonoBehaviour
{
    [SerializeField] private Toggle pulseSwitch;
    [SerializeField] private TextMeshProUGUI pulseValue;
    
    [SerializeField] private Toggle pressureSwitch;
    [SerializeField] private TextMeshProUGUI pressureValue;
    
    [SerializeField] private Toggle temperatureSwitch;
    [SerializeField] private TextMeshProUGUI temperatureValue;

    private ModifiedMedParams _shipMemberMedParams;

    private void OnEnable()
    {
        ShipEventsBus.ResettingPanel += TurnOffSwitches;
        pulseSwitch.onValueChanged.AddListener(TogglePulseSwitch);
        pressureSwitch.onValueChanged.AddListener(TogglePressureSwitch);
        temperatureSwitch.onValueChanged.AddListener(ToggleTemperatureSwitch);
        PlanetEventsBus.ShipMemberSentBack += SaveShipMemberData;
        ShipEventsBus.LettingShipMemberIn += ResetShipMemberData;
        ShipEventsBus.BurningShipMember += ResetShipMemberData;
    }
    
    private void OnDisable()
    {
        ShipEventsBus.ResettingPanel -= TurnOffSwitches;
        pulseSwitch.onValueChanged.RemoveListener(TogglePulseSwitch);
        pressureSwitch.onValueChanged.RemoveListener(TogglePressureSwitch);
        temperatureSwitch.onValueChanged.RemoveListener(ToggleTemperatureSwitch);
        PlanetEventsBus.ShipMemberSentBack -= SaveShipMemberData;
        ShipEventsBus.LettingShipMemberIn -= ResetShipMemberData;
        ShipEventsBus.BurningShipMember -= ResetShipMemberData;
    }

    private void TurnOffSwitches()
    {
        pulseSwitch.isOn = false;
        pressureSwitch.isOn = false;
        temperatureSwitch.isOn = false;
    }

    private void SaveShipMemberData(ShipMember shipMember)
    {
        _shipMemberMedParams = shipMember.modifiedMedParams;
    }

    private void ResetShipMemberData()
    {
        _shipMemberMedParams = null;
        pulseValue.SetText("");
        pressureValue.SetText("");
        temperatureValue.SetText("");
    }

    private void TogglePulseSwitch(bool switchValue)
    {
        pulseValue.SetText(switchValue && _shipMemberMedParams != null ? _shipMemberMedParams.heartBeat.ToString() : "");
    }

    private void TogglePressureSwitch(bool switchValue)
    {
        pressureValue.SetText(switchValue && _shipMemberMedParams != null
            ? $"{_shipMemberMedParams.bloodPressure.upper}/{_shipMemberMedParams.bloodPressure.bottom}"
            : "");
    }

    private void ToggleTemperatureSwitch(bool switchValue)
    {
        temperatureValue.SetText(switchValue && _shipMemberMedParams != null ? _shipMemberMedParams.temperature.ToString() : "");
    }
}
