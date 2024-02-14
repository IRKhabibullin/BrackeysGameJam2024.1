using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SO_BodyPart", menuName = "ShipMember/BodyPart")]
public class SO_BodyPart : ScriptableObject
{
    public SpriteRenderer healthy;
    public SpriteRenderer damaged;
    public SpriteRenderer xRay;
    public SpriteRenderer xRayInfected;
}
