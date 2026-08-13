using UnityEngine;

public class RecyclingBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Debug.Log("Trash entered the bin!");

            Destroy(other.gameObject);
        }
    }
}