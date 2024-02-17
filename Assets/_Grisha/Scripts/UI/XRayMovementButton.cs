using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class XRayMovementsButton : MonoBehaviour
{
    public bool isMoving = false;
    public Vector3 direction = new Vector3();
    private Coroutine movementCoroutine;

    public void StartMovement()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCoroutine = StartCoroutine(Move());
        }
    }

    public void StopMovement()
    {
        isMoving = false;
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }

    public IEnumerator Move()
    {
        while (isMoving)
        {
            ShipEventsBus.MoveXRay?.Invoke(direction);
            yield return null;
        }
    }
}
