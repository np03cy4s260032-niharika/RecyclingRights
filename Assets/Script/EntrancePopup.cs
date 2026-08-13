using UnityEngine;

public class EntrancePopup : MonoBehaviour
{
    public GameObject popupPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupPanel.SetActive(false);
        }
    }
}