using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMember : MonoBehaviour
{
    [SerializeField] SO_ShipMemberProfile shipMemberProfile;

    [Header("Body parts")]
    public SO_BodyPart head;
    public SO_BodyPart torso;
    public SO_BodyPart rightHand;
    public SO_BodyPart leftHand;
    public SO_BodyPart rightLeg;
    public SO_BodyPart leftLeg;

    /// <summary>
    /// Применяет состояние для части тела
    /// </summary>
    /// <param name="bodyPartName"> Название части тела (Head, Torso, RightHand, LeftHand, RightLeg, LeftLeg) </param>
    /// <param name="bodyPartState"> Спрайт  состояния части тела (healty, damabed, xRay, xRayInfected) </param>
    public void SetBodyPartState(string bodyPartName, SpriteRenderer bodyPartState)
    {
        var _spriteRenderer = gameObject.transform.Find(bodyPartName).GetComponent<SpriteRenderer>();
        _spriteRenderer = bodyPartState;
    }
}