using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class ShipMember : MonoBehaviour
{
    [SerializeField] SO_ShipMemberProfile shipMemberProfile;

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

    void Start()
    {
        _bodyPartsMap = new() 
        {
            {BodyPart.Head, (Head, headSuit, headXRay)},
            {BodyPart.Torso, (Torso, torsoSuit, torsoXRay)}, 
            {BodyPart.RightHand, (RightHand, rightHandSuit, rightHandXRay)},
            {BodyPart.LeftHand, (Head, leftHandSuit, leftHandXRay)},
            {BodyPart.RightLeg, (RightLeg, rightLegSuit, rightLegXRay)},
            {BodyPart.LeftLeg, (Head, leftLegSuit, leftLegXRay)},
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
        List<BodyPart> _healtyBodyParts = _bodyPartsMap.Keys.ToList();
        _healtyBodyParts.RemoveAll(item => damagedBodyParts.Contains(item));

        int _bodyPartToDamageIndex = Random.Range(0, _healtyBodyParts.Count);

        damagedBodyParts.Add(_healtyBodyParts[_bodyPartToDamageIndex]);
    }
    public void ApplyInfection()
    {
        List<BodyPart> _bodyPartsToInfect = new() 
        {
            BodyPart.RightHand, BodyPart.LeftHand, 
            BodyPart.RightLeg, BodyPart.LeftLeg
        };
        int _bodyPartToInfectIndex = Random.Range(0, _bodyPartsToInfect.Count);

        infectedBodyPart = _bodyPartsToInfect[_bodyPartToInfectIndex];
    }
    public void MoveInfection()
    {
        if(infectedBodyPart != BodyPart.Torso & infectedBodyPart != BodyPart.Head)
        {
            infectedBodyPart = BodyPart.Torso;
        }
        else if(infectedBodyPart == BodyPart.Torso)
        {
            infectedBodyPart = BodyPart.Head;
        }
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