using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            Debug.Log("asd");
            other.transform.position = Vector3.zero;
        }
    }
}
