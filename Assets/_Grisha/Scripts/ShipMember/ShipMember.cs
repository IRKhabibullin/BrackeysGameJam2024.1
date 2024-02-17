using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipMember : MonoBehaviour
{
    public SO_ShipMemberProfile shipMemberProfile;
    public ModifiedMedParams modifiedMedParams;

    [Header("Body parts")]
    [SerializeField] private SO_BodyPart Head;
    [SerializeField] private SO_BodyPart Torso;
    [SerializeField] private SO_BodyPart RightHand;
    [SerializeField] private SO_BodyPart LeftHand;
    [SerializeField] private SO_BodyPart RightLeg;
    [SerializeField] private SO_BodyPart LeftLeg;

    [Header("Altered body parts")]
    public List<BodyPart> damagedBodyParts;
    public BodyPart infectedBodyPart;

    [Header("Body parts sprites")]
    [SerializeField] private SpriteRenderer headSuit;
    [SerializeField] private SpriteRenderer torsoSuit;
    [SerializeField] private SpriteRenderer rightHandSuit;
    [SerializeField] private SpriteRenderer leftHandSuit;
    [SerializeField] private SpriteRenderer rightLegSuit;
    [SerializeField] private SpriteRenderer leftLegSuit;
    
    [SerializeField] private SpriteRenderer headXRay;
    [SerializeField] private SpriteRenderer torsoXRay;
    [SerializeField] private SpriteRenderer rightHandXRay;
    [SerializeField] private SpriteRenderer leftHandXRay;
    [SerializeField] private SpriteRenderer rightLegXRay;
    [SerializeField] private SpriteRenderer leftLegXRay;
    
    
    private Dictionary<BodyPart, (SO_BodyPart, SpriteRenderer, SpriteRenderer)> _bodyPartsMap;

    public bool IsFinalInfectionStage => infectedBodyPart == BodyPart.Head;

    public bool IsInfected => infectedBodyPart != BodyPart.None;
    public bool IsDamaged => damagedBodyParts.Count > 0;

    void Start()
    {
        _bodyPartsMap = new() 
        {
            {BodyPart.Head, (Head, headSuit, headXRay)},
            {BodyPart.Torso, (Torso, torsoSuit, torsoXRay)}, 
            {BodyPart.RightHand, (RightHand, rightHandSuit, rightHandXRay)},
            {BodyPart.LeftHand, (LeftHand, leftHandSuit, leftHandXRay)},
            {BodyPart.RightLeg, (RightLeg, rightLegSuit, rightLegXRay)},
            {BodyPart.LeftLeg, (LeftLeg, leftLegSuit, leftLegXRay)},
        };
    }

    /// <summary>
    /// Перерисовываем все части тела с учетом ран и заражения личинкой
    /// </summary>
    [ContextMenu("RedrawBodyParts")]
    public void RedrawBodyParts()
    {
        foreach (var (bodyPart, (so, suitPart, xRayPart)) in _bodyPartsMap)
        {
            suitPart.sprite = so.healthy;
            xRayPart.sprite = so.xRay;

            if (damagedBodyParts.Contains(bodyPart))
            {
                suitPart.sprite = so.damaged;
            }

            if (infectedBodyPart == bodyPart)
            {
                xRayPart.sprite = so.xRayInfected;
            }
        }
    }
    public void ApplyDamage()
    {
        var healthyBodyParts = _bodyPartsMap.Keys.ToList();
        healthyBodyParts.RemoveAll(item => damagedBodyParts.Contains(item));

        var bodyPartToDamageIndex = Random.Range(0, healthyBodyParts.Count);

        damagedBodyParts.Add(healthyBodyParts[bodyPartToDamageIndex]);
    }
    public void ApplyInfection()
    {
        List<BodyPart> bodyPartsToInfect = new() 
        {
            BodyPart.RightHand, BodyPart.LeftHand, 
            BodyPart.RightLeg, BodyPart.LeftLeg
        };
        var bodyPartToInfectIndex = Random.Range(0, bodyPartsToInfect.Count);

        infectedBodyPart = bodyPartsToInfect[bodyPartToInfectIndex];
    }
    public void MoveInfection()
    {
        switch (infectedBodyPart)
        {
            case BodyPart.Head:
                return;
            case BodyPart.Torso:
                infectedBodyPart = BodyPart.Head;
                break;
            default:
                infectedBodyPart = BodyPart.Torso;
                break;
        }
    }
    public void SetDefaultMedParams()
    {
        modifiedMedParams.bloodPressure = shipMemberProfile.bloodPressure;
        modifiedMedParams.heartBeat = shipMemberProfile.heartBeat;
        modifiedMedParams.temperature = shipMemberProfile.temperature;
    }
    public void ModifyMedParams()
    {
        var _defaultBloodPressure = shipMemberProfile.bloodPressure;
        var _pressureModifier = 20;
        modifiedMedParams.bloodPressure.upper = Random.Range(_defaultBloodPressure.upper - _pressureModifier, _defaultBloodPressure.upper + _pressureModifier);
        modifiedMedParams.bloodPressure.bottom = Random.Range(_defaultBloodPressure.bottom - _pressureModifier, _defaultBloodPressure.bottom + _pressureModifier);

        // normileze blood pressure a little bit, if difference between upper and bottom to low
        if(modifiedMedParams.bloodPressure.upper - modifiedMedParams.bloodPressure.bottom <= 10)
        {
            modifiedMedParams.bloodPressure.upper += 5;
            modifiedMedParams.bloodPressure.bottom -= 5;
        }

        modifiedMedParams.heartBeat = Random.Range(shipMemberProfile.heartBeat - 20, shipMemberProfile.heartBeat + 40);

        modifiedMedParams.temperature = Random.Range(shipMemberProfile.temperature - 3.5f, shipMemberProfile.temperature + 3.5f);
    }
}


public enum BodyPart
{
    None,
    Head,
    Torso,
    RightHand,
    LeftHand,
    RightLeg,
    LeftLeg
}
[System.Serializable]
public class ModifiedMedParams
{
    public int heartBeat;
    public BloodPressure bloodPressure;    
    public float temperature;
}