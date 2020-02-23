using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform[] MovePoint;
    int nextMove = 0;
    int currentMove = 0;
    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(MovePoint[0].position, MovePoint[1].position);
        nextMove = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == MovePoint[nextMove].position){
            startTime = Time.time;

            currentMove = nextMove;
            if(nextMove == MovePoint.Length-1){
                nextMove = 0;
            }else{
                nextMove++;
            }

            // Calculate the journey length.
            journeyLength = Vector3.Distance(MovePoint[currentMove].position, MovePoint[nextMove].position);

        }

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;
        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;
        // Set our position as a fraction of the distance between the markers.

        transform.position = Vector3.Lerp(MovePoint[currentMove].position, MovePoint[nextMove].position, fractionOfJourney);        
    }
}
