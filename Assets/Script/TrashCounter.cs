
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TrashCounter : MonoBehaviour
{
    public TMP_Text counterText;

    private int totalTrash;
    private int remainingTrash;

    private HashSet<int> countedTrash = new HashSet<int>();

    void Start()
    {
        totalTrash = GameObject.FindGameObjectsWithTag("Trash").Length;
        remainingTrash = totalTrash;

        UpdateDisplay();
    }

    public void RemoveTrash(GameObject trash)
    {
        if (trash == null)
            return;

        int trashID = trash.GetInstanceID();

        // Prevent the same trash from being counted twice
        if (countedTrash.Contains(trashID))
            return;

        countedTrash.Add(trashID);

        if (remainingTrash > 0)
        {
            remainingTrash--;

            UpdateDisplay();

            Debug.Log("Trash remaining: " + remainingTrash);
        }
    }

    void UpdateDisplay()
    {
        if (counterText != null)
        {
            counterText.text =
                "Trash Remaining: " +
                remainingTrash +
                " / " +
                totalTrash;
        }
    }
}