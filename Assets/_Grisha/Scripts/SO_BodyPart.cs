using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SO_BodyPart", menuName = "ShipMember/BodyPart")]
public class SO_BodyPart : ScriptableObject
{
    public Sprite healthy;
    public Sprite damaged;
    public Sprite xRay;
    public Sprite xRayInfected;
    public Sprite GetSpriteByState(string stateName)
    {
        Dictionary<string, Sprite> _states = new() {
            {"healthy", healthy}, {"damaged", damaged}, {"xRay", xRay}, {"xRayInfected", xRayInfected}
        };
        return _states[stateName];
    }
}
