using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SO_BodyPart", menuName = "ShipMember/BodyPart")]
public class SO_BodyPart : ScriptableObject
{
    public Sprite healthy;
    public Sprite damaged;
    public Sprite xRay;
    public Sprite xRayInfected;
}
