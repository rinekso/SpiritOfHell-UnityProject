using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFinishScript : MonoBehaviour
{
    public GameObject panel_finish;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            other.GetComponent<PlayerScript>().play = false;
            goingToPlayer(other.gameObject);
        }
    }
    void goingToPlayer(GameObject player){
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<FollowScript>().target = player;
        transform.localScale = new Vector3(3,3,3);

        GetComponent<AudioSource>().Play();

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>().Finish(player,gameObject);
    }
}
