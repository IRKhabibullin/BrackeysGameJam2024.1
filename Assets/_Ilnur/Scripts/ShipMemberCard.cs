using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipMemberCard : MonoBehaviour
{
    [SerializeField] private SO_ShipMemberProfile profile;
    [SerializeField] private TextMeshProUGUI nameAndSurname;
    [SerializeField] private Image photo;
    [SerializeField] private TextMeshProUGUI height;
    [SerializeField] private TextMeshProUGUI age;
    [SerializeField] private TextMeshProUGUI sex;

    private void OnValidate()
    {
        if (!profile)
            return;

        nameAndSurname.SetText(profile.nameAndSurname);
        photo.sprite = profile.photo;
        height.SetText($"Height: {profile.height}");
        age.SetText($"Age: {profile.age}");
        sex.SetText($"Sex: {profile.sex}");
    }
}
