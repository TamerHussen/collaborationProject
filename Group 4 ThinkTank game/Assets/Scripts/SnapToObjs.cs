using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToObjs : MonoBehaviour
{
    public float snapDistance = 1.0f;  // 最大吸附距离
    public Collider snapArea;  // 定义吸附区域的碰撞体
    private List<GameObject> objectsInScene = new List<GameObject>();  // 场景中所有物体的列表

    void Start()
    {
        // 查找所有带有 "SnapObject" 标签的物体
        objectsInScene.Clear();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("SnapObject");

        foreach (GameObject obj in allObjects)
        {
            objectsInScene.Add(obj);
            Debug.Log("Object added to list: " + obj.name);  // 输出调试信息，查看哪些物体被添加到列表
        }
    }

    void Update()
    {
        if (objectsInScene.Count < 2) return;  // 至少需要两个物体才进行吸附检测

        CheckForNearbyObjects();
    }

    void CheckForNearbyObjects()
    {
        bool allCloseEnough = true;

        // 遍历物体列表并计算物体之间的距离
        for (int i = 0; i < objectsInScene.Count; i++)
        {
            for (int j = i + 1; j < objectsInScene.Count; j++)
            {
                GameObject objA = objectsInScene[i];
                GameObject objB = objectsInScene[j];

                // 确保物体都在吸附区域内
                if (snapArea.bounds.Contains(objA.transform.position) && snapArea.bounds.Contains(objB.transform.position))
                {
                    float distance = Vector3.Distance(objA.transform.position, objB.transform.position);
                    Debug.Log($"Distance between {objA.name} and {objB.name}: {distance}");

                    if (distance > snapDistance)
                    {
                        allCloseEnough = false;
                        break;
                    }
                }
            }

            if (!allCloseEnough) break;
        }

        if (allCloseEnough)
        {
            Debug.Log("所有物体都足够接近，可以进行吸附了。");
        }
    }
}
