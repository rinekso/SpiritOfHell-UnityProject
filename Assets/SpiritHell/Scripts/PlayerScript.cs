using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    bool speedUp = false;
    public float deg = 10;
    float tempDeg;
    float tempSpeed;
    Rigidbody2D m_Rigidbody;
    public GameObject direction;
    public bool isRight = true;
    public bool play;

    public static bool BlockedByUI;
    private EventTrigger eventTrigger;
    GraphicRaycaster raycaster;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        eventTrigger = GetComponent<EventTrigger>();
        if(eventTrigger != null)
        {
            EventTrigger.Entry enterUIEntry = new EventTrigger.Entry();
            // Pointer Enter
            enterUIEntry.eventID = EventTriggerType.PointerEnter;
            enterUIEntry.callback.AddListener((eventData) => { EnterUI(); });
            eventTrigger.triggers.Add(enterUIEntry);

            //Pointer Exit
            EventTrigger.Entry exitUIEntry = new EventTrigger.Entry();
            exitUIEntry.eventID = EventTriggerType.PointerExit;
            exitUIEntry.callback.AddListener((eventData) => { ExitUI(); });
            eventTrigger.triggers.Add(exitUIEntry);
        }
    }
    public void EnterUI()
    {
        BlockedByUI = true;
    }
    public void ExitUI()
    {
        BlockedByUI = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(play){
            Vector3 dir = (direction.transform.position - transform.position).normalized;
            transform.position += dir*Time.deltaTime*speed;
            if(isRight){
                transform.Rotate(new Vector3(0,0,-deg) * Time.deltaTime * speed, Space.World);
            }else{
                transform.Rotate(new Vector3(0,0,deg) * Time.deltaTime * speed, Space.World);
            }

            // controll touch
            if(Input.GetMouseButtonDown(0)){

                if(!BlockedByUI){
                    speedUpMove();
                    isRight = !isRight;
                }
            }
            if(Input.GetMouseButtonUp(0) && speedUp){
                normalMove();
            }
        }else{
            return;
        }
    }
    void normalMove(){
        speedUp = false;
        speed = tempSpeed;
        deg = tempDeg;
    }
    void speedUpMove(){
        speedUp = true;
        tempDeg = deg;
        tempSpeed = speed;
        deg = 0;
        speed = speed*2;
    }
}
