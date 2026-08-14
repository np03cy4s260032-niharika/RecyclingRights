using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class InteractionPrompt : MonoBehaviour
{
    public Camera playerCamera;
    public TextMeshProUGUI interactionText;
    public Transform holdPoint;

    public float interactionDistance = 4f;

    void Update()
    {
        // Hide text by default
        interactionText.text = "";

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if player is holding trash
            bool isHoldingTrash = holdPoint.childCount > 0;

            // NOT HOLDING TRASH
            if (!isHoldingTrash)
            {
                if (hit.collider.CompareTag("Trash") ||
                    hit.collider.transform.root.CompareTag("Trash"))
                {
                    // Get the main trash object
                    string trashName = hit.collider.transform.root.name;

                    // Remove duplicate numbers such as (1), (2), (3)
                    trashName = Regex.Replace(trashName, @"\s*\(\d+\)$", "");

                    // Remove extra spaces
                    trashName = trashName.Trim();

                    // Make the name lowercase
                    trashName = trashName.ToLower();

                    interactionText.text =
                        "Press E to carry " + trashName;
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