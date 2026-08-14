using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    public Camera playerCamera;
    public Transform holdPoint;
    public float pickupDistance = 3f;

    private GameObject heldItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Trash"))
            {
                heldItem = hit.collider.gameObject;

                Rigidbody rb = heldItem.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.isKinematic = true;

                heldItem.transform.SetParent(holdPoint);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.transform.localRotation = Quaternion.identity;

                Debug.Log("Picked up trash!");
            }
        }
    }
}