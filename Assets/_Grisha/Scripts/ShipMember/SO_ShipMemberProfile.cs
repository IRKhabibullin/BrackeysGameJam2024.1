using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipMember/Profile")]
public class SO_ShipMemberProfile: ScriptableObject
{
    [Header("Member data")]
    public string nameAndSurname;
    public int height;
    public int weight;
    public ShipMemberSex sex;
    public int age;
    public int bloodType;
    public Sprite photo;

    [Header(header: "Medical indicators")]
    public int heartBeat;
    public BloodPressure bloodPressure;    
}

public enum ShipMemberSex
{
    Male,
    Female
}

[Serializable]
public class BloodPressure
{
    public int upper;
    public int bottom;
}

