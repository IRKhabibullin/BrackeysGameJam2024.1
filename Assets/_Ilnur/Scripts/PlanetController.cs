using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private List<ShipMember> shipMembers;
    [SerializeField][Range(0, 100)] private int chanceToTakeDamage = 50;
    [SerializeField][Range(0, 100)] private int chanceToTakeInfection = 50;
    [SerializeField][Range(0, 100)] private int chanceToMoveInfection = 50;
    [SerializeField][Range(0, 100)] private int chanceToModifyMedParamsForClean = 50;
    [SerializeField][Range(0, 100)] private int chanceToModifyMedParamsForDamaged = 50;
    [SerializeField][Range(0, 100)] private int chanceToModifyMedParamsForInfected = 50;
    private void AddShipMember(ShipMember shipMember)
    {
        shipMembers.Add(shipMember);
    }

    private void SendShipMemberBack()
    {
        if (shipMembers.Count <= 0)
        {
            return;
        }
        var shipMemberIndex = Random.Range(0, shipMembers.Count);
        var shipMemberToReturn = shipMembers[shipMemberIndex];
        shipMembers.RemoveAt(shipMemberIndex);
        
        ModifyShipMember(shipMemberToReturn);

        PlanetEventsBus.ShipMemberSentBack?.Invoke(shipMemberToReturn);
    }

    void ModifyShipMember(ShipMember shipMember)
    {
        if(ShouldDamageShipMember())
        {
            shipMember.ApplyDamage();
        }

        if(shipMember.IsInfected)
        {
            if(ShouldMoveInfection())
            {    
                shipMember.MoveInfection();
            }
        }
        else if(ShouldInfectShipMember() & shipMember.IsDamaged)
        {
            shipMember.ApplyInfection();
        }
        
        if(ShouldModifyMedParams(shipMember))
        {
            shipMember.ModifyMedParams();
        }
        else
        {
            shipMember.SetDefaultMedParams();
        }

        shipMember.RedrawBodyParts();
    }
    bool ShouldModifyMedParams(ShipMember shipMember)
    {
        int chance = UnityEngine.Random.Range(0, 100);
        if(shipMember.IsInfected)
        {
            return chance <= chanceToModifyMedParamsForInfected;
        }
        else if(shipMember.IsDamaged)
        {
            return chance <= chanceToModifyMedParamsForDamaged;
        }
        else
        {
            return chance <= chanceToModifyMedParamsForClean;
        }
    }
    bool ShouldDamageShipMember()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        return chance <= chanceToTakeDamage;
    }
    bool ShouldInfectShipMember()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        return chance <= chanceToTakeInfection;
    }
    bool ShouldMoveInfection()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        return chance <= chanceToMoveInfection;
    }
    private void OnEnable()
    {
        PlanetEventsBus.ShipMemberGoingGathering += AddShipMember;
        PlanetEventsBus.ShipMemberComingToShip += SendShipMemberBack;
    }

    private void OnDisable()
    {
        PlanetEventsBus.ShipMemberGoingGathering -= AddShipMember;
        PlanetEventsBus.ShipMemberComingToShip -= SendShipMemberBack;
    }
}
