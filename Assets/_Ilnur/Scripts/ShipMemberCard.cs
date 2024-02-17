using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipMemberCard : MonoBehaviour
{
    [SerializeField] private SO_ShipMemberProfile profile;

    [Header("Member data")]
    [SerializeField] private TextMeshProUGUI nameAndSurname;
    [SerializeField] private Image photo;
    [SerializeField] private TextMeshProUGUI height;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private TextMeshProUGUI sex;
    [SerializeField] private TextMeshProUGUI bloodType;
    [SerializeField] private TextMeshProUGUI age;

    [Header("Medical data")]
    [SerializeField] private TextMeshProUGUI heartBeat;
    [SerializeField] private TextMeshProUGUI bloodPressure;
    [SerializeField] private TextMeshProUGUI temperature;

    private void OnValidate()
    {
        if (!profile)
            return;

        nameAndSurname.SetText(profile.nameAndSurname);
        photo.sprite = profile.photo;
        height.SetText($"Height: {profile.height}");
        age.SetText($"Age: {profile.age}");
        sex.SetText(sourceText: $"Sex: {profile.sex}");
    }
    private void RefreshProfile(SO_ShipMemberProfile so)
    {
        nameAndSurname.SetText(so.nameAndSurname);
        photo.sprite = so.photo;
        height.SetText($"Height: {so.height}");
        weight.SetText($"Weight: {so.weight}");
        bloodType.SetText($"Blood type: {so.bloodType}");
        sex.SetText($"Gender: {so.sex}");
        age.SetText($"Age: {so.age}");

        heartBeat.SetText($"Pulse: {so.heartBeat}");
        bloodPressure.SetText($"Pressure: {so.bloodPressure.bottom}/{so.bloodPressure.upper}");
        temperature.SetText($"Temperature: {so.temperature:0.0}");
    }
    private void OnEnable()
    {
        ShipEventsBus.ShowShipMemberProfileOnUI += RefreshProfile;
    }
    private void OnDisable()
    {
        ShipEventsBus.ShowShipMemberProfileOnUI -= RefreshProfile;
    }
}
