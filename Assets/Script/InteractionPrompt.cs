using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public Camera playerCamera;
    public TextMeshProUGUI interactionText;
    public Transform holdPoint;

    public float interactionDistance = 4f;

    void Update()
    {
        interactionText.text = "";

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if the player is currently holding trash
            bool isHoldingTrash = holdPoint.childCount > 0;

            // NOT HOLDING TRASH
            if (!isHoldingTrash)
            {
                if (hit.collider.CompareTag("Trash") ||
                    hit.collider.transform.root.CompareTag("Trash"))
                {
                    interactionText.text = "Press E to carry bottle";
                }
            }

            // HOLDING TRASH
            else
            {
                if (hit.collider.CompareTag("RecyclingBin") ||
                    hit.collider.transform.root.CompareTag("RecyclingBin"))
                {
                    interactionText.text = "Press E to throw bottle";
                }
            }
        }
    }
}