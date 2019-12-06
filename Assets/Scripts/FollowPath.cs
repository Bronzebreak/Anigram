using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour
{
    public enum MovementType  //Type of Movement
    {
        MoveTowards,
        LerpTowards
    }

    #region Variables
    public MovementType Type = MovementType.MoveTowards; // Movement type used
    public MovementPath MyPath; // Reference to Movement Path Used
    private float Speed1 ; // Speed object is moving
    public float TimeTotal1 = 0.0f; // Time it takes to complete the loop
    public float TimeTotal2 = 0.0f; // Time it takes to complete the loop
    float totalDistance1 = 0.0f;
    float totalDistance2 = 0.0f;
    public float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point
    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint
    #endregion

    // (Unity Named Methods)
    #region Main Methods
           
    public void Awake()
    {
        for (int points =0; points < 12; points++ )
        {
            float distance = Vector3.Distance(MyPath.PathSequence[points].position, MyPath.PathSequence[points + 1].position);//gets the distance between two points next5 to each other in array.
            totalDistance1 = totalDistance1 + distance;// adds the distance between two points to total distance 
        }

        for (int points =12; points < MyPath.PathSequence.Length-1; points++ )
        {
            float distance = Vector3.Distance(MyPath.PathSequence[points].position, MyPath.PathSequence[points + 1].position);//gets the distance between two points next5 to each other in array.
            totalDistance2 = totalDistance2 + distance;// adds the distance between two points to total distance 
        }
    }
     
    public void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        Follow();
    }

    public void Follow()
    {
        //Make sure there is a path assigned
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }

        //Sets up a reference to an instance of the coroutine GetNextPathPoint
        pointInPath = MyPath.GetNextPathPoint();
        Debug.Log(pointInPath.Current);

        //Get the next point in the path to move to (Gets the Default 1st value)
        pointInPath.MoveNext();
        Debug.Log(pointInPath.Current);

        //Make sure there is a point to move to
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; //Exit Start() if there is no point to move to
        }

        //Set the position of this object to the position of our starting point
        transform.position = pointInPath.Current.position;
    }

    //Update is called by Unity every frame
    public void Update()
    {
        //Validate there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null)
        {
            return; //Exit if no path is found
        }

        if (Type == MovementType.MoveTowards) //If you are using MoveTowards movement type
        {
            if (MyPath.movingTo < 12 && MyPath.movingTo > -1)
                {
                transform.position = Vector3.MoveTowards(transform.position,
                                    pointInPath.Current.position,
                                    Time.deltaTime * (totalDistance1 / TimeTotal1));

                }
                //Move to the next point in path using MoveTowards
            else{
                                transform.position = Vector3.MoveTowards(transform.position,
                                    pointInPath.Current.position,
                                    Time.deltaTime * (totalDistance2 / TimeTotal2));
            }
        }
        else if (Type == MovementType.LerpTowards) //If you are using LerpTowards movement type
        {
            //Move towards the next point in path using Lerp
            transform.position = Vector3.Lerp(transform.position,
                                                pointInPath.Current.position,
                                                Time.deltaTime * (totalDistance2 / TimeTotal1));
        }

        //Check to see if you are close enough to the next point to start moving to the following one
        //Using Pythagorean Theorem
        //per unity suaring a number is faster than the square root of a number
        //Using .sqrMagnitude 
        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
     }
    #endregion //Main Methods
}
