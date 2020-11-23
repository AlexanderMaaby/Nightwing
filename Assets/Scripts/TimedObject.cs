using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObject : MonoBehaviour
{

    private bool isRewinding = false;

    List<PointInTime> pointsInTime;

    public float recordTime = 10f;

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewinding();
        if (Input.GetKeyUp(KeyCode.Return))
            StopRewinding();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind(); 
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime temp = pointsInTime[0];
            transform.position = temp.position;
            transform.rotation = temp.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewinding();
        }
        
    }

    void Record ()
    {
        //Checking if we have more points in time saved than we would get during the amount of time stated in the variable recordTime.
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    //These two are set to public for now so they can be accessed from anywhere. Could come in handy if I want to make a time reseting spell later. For now they could be private.
    public void StartRewinding()
    {
        isRewinding = true;
        rigidbody.isKinematic = true;
    }

    public void StopRewinding()
    {
        isRewinding = false;
        rigidbody.isKinematic = false;
    }
}
