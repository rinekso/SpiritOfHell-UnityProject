using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject target;
    public bool follow = false;
    public float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        if(target && follow){
            // Define a target position above and behind the target transform
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
