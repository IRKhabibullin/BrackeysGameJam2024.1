using UnityEngine;

public class XRayMovementSystem : MonoBehaviour
{
    [SerializeField] int xRayMovementSpeed = 20;
    void OnEnable()
    {
        ShipEventsBus.MoveXRay += MoveXRay;
    }
    void OnDisable()
    {
        ShipEventsBus.MoveXRay -= MoveXRay;
    }
    void MoveXRay(Vector3 movementDirection)
    {
        movementDirection.Normalize();
        gameObject.transform.Translate(movementDirection * xRayMovementSpeed * Time.deltaTime);
    }
}