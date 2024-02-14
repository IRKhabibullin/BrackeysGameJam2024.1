using System;
using UnityEngine;

[Serializable]
public class ShipMemberProfile
{
    [Header("Member data")]
    public int memberHeight;
    public int memberWeight;
    public ShipMemberSex shipMemberSex;
    public int memberAge;
    public int bloodType;
    [Header(header: "Medical indicators")]
    public int heartBeat;
    public BloodPressure bloodPressure;    
}

public enum ShipMemberSex {male, female}
[Serializable]
public class BloodPressure
{
    public int upper;
    public int bottom;
}

