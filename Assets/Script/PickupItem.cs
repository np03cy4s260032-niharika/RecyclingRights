using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PickupItem : MonoBehaviour
{
    public GameObject pickupText;

    private bool playerNearby = false;

    private void Start()
    {
        pickupText.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Pickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            pickupText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            pickupText.SetActive(false);
        }
    }

    private void Pickup()
    {
        pickupText.SetActive(false);

        Debug.Log("Item picked up!");

        Destroy(gameObject);
    }
}
