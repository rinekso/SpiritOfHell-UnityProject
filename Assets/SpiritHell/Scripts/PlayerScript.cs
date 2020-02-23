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

    GraphicRaycaster raycaster;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
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
                //Set up the new Pointer Event
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                pointerData.position = Input.mousePosition;
                bool ui = false;
                EventSystem.current.RaycastAll(pointerData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.layer == 5){
                        ui = true;
                    }
                }

                if(!ui){
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
        deg = deg/10;
        speed = speed*2;
    }
}
