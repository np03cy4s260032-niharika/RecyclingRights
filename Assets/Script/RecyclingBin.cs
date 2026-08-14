using UnityEngine;
using UnityEngine.InputSystem;

public class RecyclingBin : MonoBehaviour
{
    public string acceptedTag = "Trash";

    private GameObject nearbyTrash;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(acceptedTag))
        {
            nearbyTrash = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == nearbyTrash)
        {
            nearbyTrash = null;
        }
    }

    private void Update()
    {
        if (nearbyTrash != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            RecycleItem();
        }
    }

    private void RecycleItem()
    {
        Destroy(nearbyTrash);

        nearbyTrash = null;

        Debug.Log("♻️ Trash recycled!");
    }
}