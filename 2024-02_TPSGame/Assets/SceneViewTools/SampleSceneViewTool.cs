using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneViewTool : MonoBehaviour
{
    public float sphereRadius = 3;
    public float degrees = 90;
    public Vector3[] waypoints;

    void Start()
    {
        // List
        List<GameObject> objectList = new List<GameObject>();
        objectList.Add(gameObject);
        objectList.Add(new GameObject());
        objectList.Add(new GameObject());
        objectList.RemoveAt(2);
        objectList.Remove(gameObject);
        if (objectList.Contains(gameObject)) Debug.Log("hi");
        objectList.Clear();

        // Stack - LIFO - Last In, First Out
        Stack<Color> colorStack = new Stack<Color>();
        colorStack.Push(Color.blue);
        colorStack.Push(Color.red);
        colorStack.Push(Color.yellow);
        Color resultPeek = colorStack.Peek(); // returns yellow
        Color resultPop = colorStack.Pop(); // returns yellow and removes it
        resultPeek = colorStack.Peek(); // returns red
        colorStack.Clear();

        // Queue - FIFO - First In, First Out
        Queue<Color> colorQueue = new Queue<Color>();
        colorQueue.Enqueue(Color.blue);
        colorQueue.Enqueue(Color.yellow);
        colorQueue.Peek(); // returns blue
        colorQueue.Dequeue(); // returns blue and removes it
        colorQueue.Peek(); // returns yellow
        colorQueue.Clear();
    }

#if UNITY_EDITOR
    public void UnusedOnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, sphereRadius);

        Gizmos.color = Color.blue;
        if (waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Vector3 start = waypoints[i];
                Vector3 end = waypoints[(i+1)%waypoints.Length];
                Gizmos.DrawCube(start, Vector3.one * 0.3f);
                Gizmos.DrawLine(start, end);
            }
        }
    }
#endif
}
