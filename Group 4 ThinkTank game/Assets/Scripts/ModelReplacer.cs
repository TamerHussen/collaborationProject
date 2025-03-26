using UnityEngine;

public class ModelReplacer : MonoBehaviour
{
    // The model that will be shown when conditions are met
    public GameObject newModel;

    // The original model to be hidden
    public GameObject originalModel;

    // The GameObjects to check if they are deleted
    public GameObject[] objectsToCheck;

    // Update is called once per frame
    void Update()
    {
        // Check if all the objects in objectsToCheck array are deleted
        if (AreObjectsDeleted())
        {
            ToggleModels(true);
        }
        else
        {
            ToggleModels(false);
        }
    }

    // Check if all the objects in the objectsToCheck array are deleted
    private bool AreObjectsDeleted()
    {
        foreach (GameObject obj in objectsToCheck)
        {
            if (obj != null)  // If any object still exists, return false
            {
                return false;
            }
        }
        return true;  // If none of the objects are found, return true (they are deleted)
    }

    // Hide the original model and show the new model
    private void ToggleModels(bool state)
    {
        if (originalModel != null)
        {
            originalModel.SetActive(!state); // Hide original when state is true
        }

        if (newModel != null)
        {
            newModel.SetActive(state); // Show new model when state is true
        }
    }
}
