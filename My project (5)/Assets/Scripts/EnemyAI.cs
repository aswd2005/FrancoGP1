using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> points;
    public int nextId;
    private int idChangeValue;
    public float speed = 2;
    public Transform Player;
    Vector2 target;
    void Update()
    {
       

        if (Vector2.Distance(transform.position, Player.position) < 5f)
        {
            Debug.Log("HELLO");
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }
        else
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextId];
        if(goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            if (nextId == points.Count - 1)
            {
                idChangeValue = -1;
            }
            if (nextId == 0)
            {
                idChangeValue = +1;
            }
            nextId += idChangeValue;
        }
    }

}
