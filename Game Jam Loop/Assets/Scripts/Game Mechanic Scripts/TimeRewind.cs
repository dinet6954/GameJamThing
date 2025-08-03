using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    
    public bool IsRewinding = false;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (IsRewinding == true)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        IsRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        IsRewinding = false;
        rb.isKinematic = false;
    }
}
