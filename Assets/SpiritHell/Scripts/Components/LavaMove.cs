using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMove : MonoBehaviour
{
    public float speed;
    public void StopMove(){
        speed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up*Time.deltaTime*speed;
    }
}
