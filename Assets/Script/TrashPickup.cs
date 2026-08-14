using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    public Camera playerCamera;
    public Transform holdPoint;

    public float pickupDistance = 4f;
    public float throwForce = 8f;

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
                ThrowTrash();
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

            // Check the object and its parents for the Trash tag
            if (trashTransform.CompareTag("Trash") ||
                trashTransform.root.CompareTag("Trash"))
            {
                heldTrash = trashTransform.gameObject;

                // If the collider is on a child object,
                // use the Rigidbody from the parent
                heldRb = heldTrash.GetComponent<Rigidbody>();

                if (heldRb == null)
                {
                    heldRb = heldTrash.GetComponentInParent<Rigidbody>();
                }

                if (heldRb == null)
                {
                    Debug.Log("Bottle needs a Rigidbody.");
                    heldTrash = null;
                    return;
                }

                heldRb.isKinematic = true;
                heldRb.useGravity = false;

                heldTrash.transform.SetParent(holdPoint);
                heldTrash.transform.localPosition = Vector3.zero;
                heldTrash.transform.localRotation = Quaternion.identity;

                Debug.Log("Trash picked up!");
            }
            else
            {
                Debug.Log("Object is not tagged Trash. Hit object was: " 
                    + hit.collider.gameObject.name);
            }
        }
    }

    void ThrowTrash()
    {
        heldTrash.transform.SetParent(null);

        heldRb.isKinematic = false;
        heldRb.useGravity = true;

        heldRb.AddForce(
            playerCamera.transform.forward * throwForce,
            ForceMode.Impulse
        );

        Debug.Log("Trash thrown!");

        heldTrash = null;
        heldRb = null;
    }
}