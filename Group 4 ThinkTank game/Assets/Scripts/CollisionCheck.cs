using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public float distanceThreshold = 0.5f; // 物体之间的最大允许距离
    private List<GameObject> objectsInZone = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (!objectsInZone.Contains(other.gameObject))
        {
            objectsInZone.Add(other.gameObject);
            Debug.Log(objectsInZone);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (objectsInZone.Contains(other.gameObject))
        {
            objectsInZone.Remove(other.gameObject);
        }
    }

    void Update()
    {
        if (objectsInZone.Count < 2) return; // 至少需要两个物体才进行检测

        bool allCloseEnough = true;

        // 遍历所有物体，两两计算距离
        for (int i = 0; i < objectsInZone.Count; i++)
        {
            for (int j = i + 1; j < objectsInZone.Count; j++)
            {
                float distance = Vector3.Distance(objectsInZone[i].transform.position, objectsInZone[j].transform.position);
                if (distance > distanceThreshold)
                {
                    allCloseEnough = false;
                    break;
                }
            }
            if (!allCloseEnough) break;
        }

        if (allCloseEnough)
        {
            Debug.Log("Part finished");
        }
    }
}
