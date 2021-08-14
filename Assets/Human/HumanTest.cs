using UnityEngine;

public class HumanTest : MonoBehaviour
{
    public Vector3[] waypoints;
    
    public float     speed = 3f;
    public bool      isLoop = true;

    int              wayPointIndex = 0;
    Vector3          curPos;
    public Animator animtor;
                     
    void Start()
    {
        //waypoints = new Vector3[3];
        speed = Random.Range(0.5f, 1f);
        animtor = GetComponent<Animator>();
        animtor.SetInteger("Human", Random.Range(0, 6));
    }

    void FixedUpdate()
    {
        curPos = transform.localPosition;

        if (wayPointIndex < waypoints.Length)
        {
            var step = speed * Time.deltaTime;
            var v1 = Vector3.MoveTowards(curPos, waypoints[wayPointIndex], step).normalized;
            transform.localPosition = Vector3.MoveTowards(curPos, waypoints[wayPointIndex], step);

            if (v1.x < -1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (Vector3.Distance(waypoints[wayPointIndex], curPos) == 0f)
                wayPointIndex++;
        }
        else if (isLoop)
        {
            wayPointIndex = 0;
            speed = Random.Range(0.5f, 1f);
            animtor.SetInteger("Human", Random.Range(0, 6));
            transform.localPosition = waypoints[wayPointIndex];
        }
    }
}
