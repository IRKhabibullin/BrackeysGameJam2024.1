using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMember : MonoBehaviour
{
    [SerializeField] SO_ShipMemberProfile shipMemberProfile;

    [Header("Body parts")]
    public SO_BodyPart Head;
    public SO_BodyPart Torso;
    public SO_BodyPart RightHand;
    public SO_BodyPart LeftHand;
    public SO_BodyPart RightLeg;
    public SO_BodyPart LeftLeg;
    Dictionary<string, SO_BodyPart> soBodyParts;
    void Start()
    {
        soBodyParts = new() 
        {
            {"Head", Head}, {"Torso", Torso}, 
            {"RightHand", RightHand}, {"LeftHand", LeftHand},
            {"RightLeg", RightLeg}, {"LeftLeg", LeftLeg},
        };
    }
    /// <summary>
    /// Применяет состояние для части тела
    /// </summary>
    /// <param name="bodyPartName"> Название части тела (Head, Torso, RightHand, LeftHand, RightLeg, LeftLeg) </param>
    /// <param name="bodyPartState"> Название  состояния части тела (healty, damaged, xRay, xRayInfected) </param>
    public void SetBodyPartState(string bodyPartName, string bodyPartStateName)
    {
        var _bodyPartSprite = gameObject.transform.Find(bodyPartName).GetComponent<SpriteRenderer>().sprite;
        _bodyPartSprite = soBodyParts[bodyPartName].GetSpriteByState(bodyPartStateName);
    }
}