using System.Collections;
using System.Collections.Generic;
using GD.MinMaxSlider;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private List<ShipMember> shipMembers;
    [SerializeField][MinMaxSlider(0, 20)] private Vector2Int sendBackDelay;

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
        
        // todo apply changes
        
        PlanetEventsBus.ShipMemberSentBack?.Invoke(shipMemberToReturn);
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
