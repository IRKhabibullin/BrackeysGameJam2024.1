using UnityEngine;
using UnityEngine.UI;

public class XRayMovementSystem : MonoBehaviour
{
    [SerializeField] int xRayMovementSpeed = 20;
    Image xRayArea;
    Vector3[] areaCorners = new Vector3[4]; 
    void OnEnable()
    {
        ShipEventsBus.MoveXRay += MoveXRay;
        xRayArea = GameObject.Find("Art").GetComponent<Image>();
        GetFieldBorders();
    }
    void OnDisable()
    {
        ShipEventsBus.MoveXRay -= MoveXRay;
    }
    void MoveXRay(Vector3 movementDirection)
    {
        if(IsScannerInBorders())
        {
            movementDirection.Normalize();
            gameObject.transform.Translate(movementDirection * xRayMovementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, 0.1f  * Time.deltaTime);
        }
    }
    bool IsScannerInBorders()
    {
        if (transform.position.x > areaCorners[3].x - 1f||
            transform.position.x <  areaCorners[1].x + 1f||
            transform.position.y > areaCorners[1].y - 1f||
            transform.position.y < areaCorners[3].y + 1f)
        {
            return false;
        }
        return true;
    }
    void GetFieldBorders()
    {
        var rectTransform = xRayArea.GetComponent<RectTransform>();
        rectTransform.GetWorldCorners(areaCorners); // [botLeft, topLeft, topRight, botRight]
    }
}