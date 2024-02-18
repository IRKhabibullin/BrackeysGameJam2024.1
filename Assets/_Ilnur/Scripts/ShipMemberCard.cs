using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipMemberCard : MonoBehaviour
{
    [SerializeField] private SO_ShipMemberProfile profile;
    [SerializeField] private TextMeshProUGUI deadCrew;
    [SerializeField] private TextMeshProUGUI aliveCrew;

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
        height.SetText($"{so.height}");
        weight.SetText($"{so.weight}");
        bloodType.SetText($"{so.bloodType}");
        sex.SetText($"{so.sex}");
        age.SetText($"{so.age}");

        heartBeat.SetText($"{so.heartBeat}");
        bloodPressure.SetText($"{so.bloodPressure.bottom}/{so.bloodPressure.upper}");
        temperature.SetText($"{so.temperature:0.0}");
    }
    private void RefreshCrewStatus(int _aliveCrew)
    {
        aliveCrew.SetText(_aliveCrew.ToString());
        var _deadCrew = 7 - _aliveCrew;
        deadCrew.SetText(_aliveCrew.ToString());
    }
    private void OnEnable()
    {
        ShipEventsBus.ShowShipMemberProfileOnUI += RefreshProfile;
        ShipEventsBus.ShowAliveCrewNumberOnUI += RefreshCrewStatus;
    }
    private void OnDisable()
    {
        ShipEventsBus.ShowShipMemberProfileOnUI -= RefreshProfile;
        ShipEventsBus.ShowAliveCrewNumberOnUI -= RefreshCrewStatus;
    }
}
