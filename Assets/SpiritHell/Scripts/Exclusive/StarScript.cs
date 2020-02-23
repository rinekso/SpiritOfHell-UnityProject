using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public GameObject StarDieParticle;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>().AddStar();
            StartCoroutine(StarDie());
        }
    }
    IEnumerator StarDie(){
        GameObject particle = Instantiate(StarDieParticle);
        particle.transform.position = transform.position;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(particle);
        Destroy(gameObject);
        yield return true;
    }
}
