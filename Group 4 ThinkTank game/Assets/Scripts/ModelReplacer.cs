using UnityEngine;

public class ModelReplacer : MonoBehaviour
{
    // The prefab that will replace the current model
    public GameObject newModelPrefab;

    // The GameObjects to check if they are deleted
    public GameObject[] objectsToCheck;

    // The target GameObject to replace its model when the conditions are met
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        // Check if all the objects in objectsToCheck array are deleted
        if (AreObjectsDeleted())
        {
            // Replace the model with the newPrefab
            ReplaceModel();
        }
    }

    // Check if all the objects in the objectsToCheck array are deleted
    private bool AreObjectsDeleted()
    {
        foreach (GameObject obj in objectsToCheck)
        {
            if (obj != null)  // If the object still exists, return false
            {
                return false;
            }
        }
        return true;  // If none of the objects are found, return true (they are deleted)
    }

    // Replace the model on the targetObject
    private void ReplaceModel()
    {
        if (targetObject != null && newModelPrefab != null)
        {
            // Destroy the old model if it exists (optional)
            if (targetObject.transform.childCount > 0)
            {
                foreach (Transform child in targetObject.transform)
                {
                    Destroy(child.gameObject);  // Destroy the old model
                }
            }

            // Instantiate the new prefab and set it as the child of the targetObject
            Instantiate(newModelPrefab, targetObject.transform.position, targetObject.transform.rotation, targetObject.transform);
        }
        else
        {
            Debug.LogWarning("Target Object or New Model Prefab is not assigned.");
        }
    }
}
