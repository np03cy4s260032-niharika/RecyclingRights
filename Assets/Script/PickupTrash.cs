using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PickupTrash : MonoBehaviour
{
    public float pickupDistance = 3f;
    public Transform holdPoint;
    public TMP_Text pickupText;

    private GameObject heldObject;

    void Update()
    {
        // Show or hide "Press E to Pick Up"
        if (heldObject == null)
        {
            Camera cam = Camera.main;

            if (cam != null)
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupDistance) &&
                    hit.collider.CompareTag("Trash"))
                {
                    pickupText.gameObject.SetActive(true);
                }
                else
                {
                    pickupText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false);
        }

        // Press E to pick up or drop
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickup()
    {
        Camera cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("No Main Camera found");
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Trash"))
            {
                heldObject = hit.collider.gameObject;

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }

                heldObject.transform.SetParent(holdPoint);

                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                Debug.Log("Picked up: " + heldObject.name);
            }
            else
            {
                Debug.Log("Object is not tagged Trash.");
            }
        }
        else
        {
            Debug.Log("Nothing was detected.");
        }
    }

    void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        heldObject.transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Debug.Log("Dropped: " + heldObject.name);

        heldObject = null;
    }
}