using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    public Camera playerCamera;
    public Transform holdPoint;

    public float pickupDistance = 4f;

    private GameObject heldTrash;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash == null)
            {
                TryPickup();
            }
            else
            {
                RecyclingBin();
            }
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
            Transform trashTransform = hit.collider.transform;

            // Check if the object has the Trash tag
            if (trashTransform.CompareTag("Trash") ||
                trashTransform.root.CompareTag("Trash"))
            {
                heldTrash = trashTransform.gameObject;

                // Find Rigidbody
                heldRb = heldTrash.GetComponent<Rigidbody>();

                if (heldRb == null)
                {
                    heldRb = heldTrash.GetComponentInParent<Rigidbody>();
                }

                if (heldRb == null)
                {
                    Debug.Log("Trash needs a Rigidbody.");
                    heldTrash = null;
                    return;
                }

                // Turn off physics while holding
                heldRb.isKinematic = true;
                heldRb.useGravity = false;

                // Attach trash to HoldPoint
                heldTrash.transform.SetParent(holdPoint);

                heldTrash.transform.localPosition = Vector3.zero;
                heldTrash.transform.localRotation = Quaternion.identity;

                Debug.Log("Trash picked up!");
            }
            else
            {
                Debug.Log(
                    "Object is not tagged Trash. Hit object was: "
                    + hit.collider.gameObject.name
                );
            }
        }
    }

    void RecyclingBin()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            // Check if the object has the RecyclingBin tag
            if (hit.collider.CompareTag("RecyclingBin") ||
                hit.collider.transform.root.CompareTag("RecyclingBin"))
            {
                // Remove trash from player's hand
                heldTrash.transform.SetParent(null);

                // Stop physics
                heldRb.isKinematic = true;
                heldRb.useGravity = false;

                // Put trash at the recycling bin
                heldTrash.transform.position = hit.collider.bounds.center;

                // Reset rotation
                heldTrash.transform.rotation = Quaternion.identity;

                Debug.Log("Trash placed in recycling bin!");

                heldTrash = null;
                heldRb = null;
            }
            else
            {
                Debug.Log(
                    "Look directly at the RecyclingBin. Hit object was: "
                    + hit.collider.gameObject.name
                );
            }
        }
    }
}