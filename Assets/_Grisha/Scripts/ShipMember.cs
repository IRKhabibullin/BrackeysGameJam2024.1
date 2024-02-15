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

    /// <summary>
    /// Применяет состояние для части тела
    /// </summary>
    /// <param name="bodyPartName"> Название части тела (Head, Torso, RightHand, LeftHand, RightLeg, LeftLeg) </param>
    /// <param name="bodyPartState"> Название  состояния части тела (healty, damaged, xRay, xRayInfected) </param>
    public void SetBodyPartState(string bodyPartName, string bodyPartStateName)
    {
        SO_BodyPart _soBodyPart = DefineBodyPart(bodyPartName);  
        Dictionary<string, Sprite> _stateSprites = new() 
        {
            {"healthy", _soBodyPart.healthy}, {"damaged", _soBodyPart.damaged}, 
            {"xRay", _soBodyPart.xRay}, {"xRayInfected", _soBodyPart.xRayInfected}
        };

        gameObject.transform.Find(bodyPartName).GetComponent<SpriteRenderer>().sprite = _stateSprites[bodyPartStateName];
    }
    SO_BodyPart DefineBodyPart(string bodyPartName)
    {
        Dictionary<string, SO_BodyPart> _soBodyParts = new() 
        {
            {"Head", Head}, {"Torso", Torso}, 
            {"RightHand", RightHand}, {"LeftHand", LeftHand},
            {"RightLeg", RightLeg}, {"LeftLeg", LeftLeg},
        };
        return _soBodyParts[bodyPartName];
    }

}