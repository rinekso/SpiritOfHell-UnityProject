using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>().GameOver();
        }
    }
}
