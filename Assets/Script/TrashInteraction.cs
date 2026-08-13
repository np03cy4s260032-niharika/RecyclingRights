using UnityEngine;
using UnityEngine.InputSystem;

public class TrashInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public Transform holdPoint;
    public float throwForce = 8f;

    private GameObject heldTrash;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
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

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Trash"))
            {
                heldTrash = hit.collider.gameObject;

                Rigidbody rb = heldTrash.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                Collider col = heldTrash.GetComponent<Collider>();

                if (col != null)
                {
                    col.enabled = false;
                }

                heldTrash.transform.SetParent(holdPoint);
                heldTrash.transform.localPosition = Vector3.zero;
                heldTrash.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void ThrowTrash()
    {
        heldTrash.transform.SetParent(null);

        Collider col = heldTrash.GetComponent<Collider>();

        if (col != null)
        {
            col.enabled = true;
        }

        Rigidbody rb = heldTrash.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = playerCamera.transform.forward * throwForce;
        }

        heldTrash = null;
    }
}
