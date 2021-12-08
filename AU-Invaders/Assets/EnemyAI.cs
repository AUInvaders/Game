using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    public List<Transform> points;
    //the int value for next points index
    public int nextID = 0;
    //the value of that applies to ID for changing
    int idChangeValue = 1;
    public float speed = 40;

    private void Reset()
    {
        Init();
    }
    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        GameObject root = new GameObject(name + "_Root");

        root.transform.position = transform.position;

        transform.SetParent(root.transform);

        GameObject waypoints = new GameObject("Waypoints");

        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);


    }

    private void Update()
    {
        MoveToNextPoint();
    }
    void MoveToNextPoint()
    {
        Transform goalpoint = points[nextID];


        transform.position = Vector2.MoveTowards(transform.position, goalpoint.position, speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, goalpoint.position)<1f)
        {
            if (nextID == points.Count - 1)
            {
                idChangeValue = -1;
            }
            if (nextID == 0)
            {
                idChangeValue = 1;
            }
            nextID += idChangeValue;
        }
    }
}
