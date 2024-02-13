using System;
using UnityEngine;

[Serializable]
public class ShipMemberProfile
{
    [Header("Member data")]
    [SerializeField]
    int memberHeight;
    [SerializeField]
    int memberWeight;
    [SerializeField]
    ShipMemberSex shipMemberSex = ShipMemberSex.male;
    [SerializeField]
    int memberAge;
    [SerializeField] 
    int bloodType = 0;
    [Header(header: "Medical indicators")]
    [SerializeField]
    int heartBeat;
    [SerializeField]
    BloodPressure bloodPressure;    
}

public enum ShipMemberSex {male, female}
[Serializable]
public class BloodPressure
{
    [SerializeField]
    int upper;
    [SerializeField]
    int bottom;
}

