using System.Collections;
using System.Collections.Generic;
using GD.MinMaxSlider;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private List<ShipMember> shipMembers;
    [SerializeField][MinMaxSlider(0, 20)] private Vector2Int sendBackDelay;
    [SerializeField][Range(0, 100)] private int chanceToTakeDamage = 50;
    [SerializeField][Range(0, 100)] private int chanceToTakeInfection = 50;
    [SerializeField][Range(0, 100)] private int chanceToMoveInfection = 50;
    private void AddShipMember(ShipMember shipMember)
    {
        shipMembers.Add(shipMember);
    }

    private void SendShipMemberBack()
    {
        if (shipMembers.Count < 0)
        {
            return;
        }
        StartCoroutine(SendShipMemberBackCoroutine());
    }

    private IEnumerator SendShipMemberBackCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(sendBackDelay.x, sendBackDelay.y));

        var shipMemberIndex = Random.Range(0, shipMembers.Count);
        var shipMemberToReturn = shipMembers[shipMemberIndex];
        shipMembers.RemoveAt(shipMemberIndex);
        
        ModifyShipMember(shipMemberToReturn);

        PlanetEventsBus.ShipMemberSentBack?.Invoke(shipMemberToReturn);
    }
    void ModifyShipMember(ShipMember shipMember)
    {
        if(SetDamageForShipMember())
        {
            shipMember.ApplyDamage();
        }

        if(shipMember.IsInfected & MoveInfectionForShipMember())
        {
           shipMember.MoveInfection();
        }
        else if(SetInfectionForShipMember())
        {
            shipMember.ApplyInfection();
        }
    }
    bool SetDamageForShipMember()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        return chance <= chanceToTakeDamage;
    }
    bool SetInfectionForShipMember()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        return chance <= chanceToTakeInfection;
    }
    bool MoveInfectionForShipMember()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        return chance <= chanceToMoveInfection;
    }
    private void OnEnable()
    {
        PlanetEventsBus.ShipMemberGoingGathering += AddShipMember;
        PlanetEventsBus.ShipMemberComingBack += SendShipMemberBack;
    }

    private void OnDisable()
    {
        PlanetEventsBus.ShipMemberGoingGathering -= AddShipMember;
        PlanetEventsBus.ShipMemberComingBack -= SendShipMemberBack;
    }
}
