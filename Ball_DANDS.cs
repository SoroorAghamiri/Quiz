using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class Ball_DANDS : MonoBehaviour {

    Drag_and_Shot dns;
    public GameObject Dir;
    public GameObject Blocker;
    public string gotbutton;
    GameObject newdir;


    public float xdirection = 0;
    public float ydirection = 0;
    public float speed =500;
    private bool move = false;
    public bool getTouch = true;
    
    void OnEnable () {
        dns = GameObject.Find("DragAndShot").GetComponent<Drag_and_Shot>();
        Debug.Log("in start");
        Create();
	}
    
    public void Create()
    {
        transform.position = Drag_and_Shot.ballpos;
        xdirection = UnityEngine.Random.value;
        ydirection = UnityEngine.Random.value;
        if(ydirection < 0.6)
        {
            ydirection = ydirection + 0.6f;
        }
        int way = UnityEngine.Random.Range(0, 2);
        if (way == 0)
        {
                xdirection = -xdirection;
        }
        var rotDir = Dir.transform.rotation;
        float tmp = ydirection;
        //Debug.Log(ydirection);
        rotDir.z = Mathf.Atan2(ydirection,xdirection) * Mathf.Rad2Deg;
        //Debug.Log("rotdir.z = " + rotDir.z);
        newdir = Instantiate(Dir, new Vector3(transform.position.x, transform.position.y , -1), Quaternion.Euler(rotDir.x, rotDir.y, rotDir.z));
        
       // Debug.Log("xdirection : " + xdirection + " ydirection : " + ydirection);

       
    }
    void Update () {
        
        if (Input.touchCount > 0 && getTouch)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
            {
                move = true;
                getTouch = false;
                Instantiate(Blocker, new Vector3(0 , -3.8f , 0) , Quaternion.identity);
            }
        }
        if (move == true && getTouch == false)
        {
            GameObject arrow = GameObject.FindGameObjectWithTag("Arrow");
            Destroy(arrow);
            
            Vector2 position = transform.position;
            float offset = speed * Time.deltaTime;
           // Debug.Log("xdirection : " + xdirection + " ydirectionnnn : " + ydirection);

            transform.Translate(offset *xdirection, offset *ydirection, 0);
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Obstacle")
        {
            dns.point++;
            dns.shoot();
            GameObject obs = GameObject.FindGameObjectWithTag("Obstacle");
            Destroy(obs);
            GameObject block = GameObject.FindGameObjectWithTag("Tiles");
            Destroy(block);
            Destroy(gameObject);
            Debug.Log("Hit the obstacle");
        }
        else if(other.tag == "Wall")
        {
            switch (other.gameObject.name)
            {
                case "Up":
                    Debug.Log("Hit the wall above");
                    ydirection = -ydirection;
                    break;
                case "Down":
                    //Game Over
                    if (dns.faultfree == true)
                    {
                        Time.timeScale = 0;
                        dns.EndPanel.SetActive(true);
                    }
                    else if (dns.record == true)
                    {
                        dns.shoot();
                        GameObject obs = GameObject.FindGameObjectWithTag("Obstacle");
                        Destroy(obs);
                        GameObject block = GameObject.FindGameObjectWithTag("Tiles");
                        Destroy(block);
                        Destroy(gameObject);
                    }
                    Debug.Log("Hit the wall below");
                    break;
                case "Right":
                    Debug.Log("Hit the wall on the right");
                    xdirection = -xdirection;
                    break;
                case "Left":
                    Debug.Log("Hit the wall on the left");
                    xdirection = -xdirection;
                    break;
            }
        }
    }
}
