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

            // Check if the object is tagged Trash
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
                    Debug.LogError("Trash needs a Rigidbody.");
                    heldTrash = null;
                    return;
                }

                // Disable physics while holding
                heldRb.isKinematic = true;
                heldRb.useGravity = false;

                // Attach to HoldPoint
                heldTrash.transform.SetParent(holdPoint);

                heldTrash.transform.localPosition = Vector3.zero;
                heldTrash.transform.localRotation = Quaternion.identity;

                Debug.Log("Trash picked up!");
            }
            else
            {
                Debug.Log(
                    "Object is not tagged Trash. Hit object: " +
                    hit.collider.gameObject.name
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
            // Check if the object is a recycling bin
            if (hit.collider.CompareTag("RecyclingBin") ||
                hit.collider.transform.root.CompareTag("RecyclingBin"))
            {
                // Save the trash reference before destroying it
                GameObject recycledTrash = heldTrash;

                // Remove from player's hand
                recycledTrash.transform.SetParent(null);

                // Disable physics
                if (heldRb != null)
                {
                    heldRb.isKinematic = true;
                    heldRb.useGravity = false;
                }

                // Move trash into the bin
                recycledTrash.transform.position = hit.collider.bounds.center;
                recycledTrash.transform.rotation = Quaternion.identity;

                Debug.Log("Trash placed in recycling bin!");

                // ADD 10 SCORE
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddScore(10);
                }

                // FIND TRASH COUNTER
                TrashCounter counter =
                    FindFirstObjectByType<TrashCounter>();

                if (counter != null)
                {
                    // Reduce counter ONCE
                    counter.RemoveTrash(recycledTrash);
                }
                else
                {
                    Debug.LogError(
                        "TrashCounter was not found in the scene!"
                    );
                }

                // Remove the recycled trash
                Destroy(recycledTrash);

                // Clear held item
                heldTrash = null;
                heldRb = null;
            }
            else
            {
                Debug.Log(
                    "Look directly at the RecyclingBin. Hit object: " +
                    hit.collider.gameObject.name
                );
            }
        }
    }
}