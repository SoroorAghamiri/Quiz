using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Obstacle_DANDS : MonoBehaviour
{

    Touch initTouch;
    public float xMoved;
    public float yMoved;
    public bool swipedLeft;

    bool swiped = false;
    Transform player;
    Transform prevPlayer;
    Rigidbody2D rb;
    public float speed = 100;
    public bool moveEn = true;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("in obstacle start");
    }
    


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && moveEn)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                    {
                        xMoved = touchpos.x - transform.position.x;
                        yMoved = touchpos.y - transform.position.y;
                        swiped = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if(swiped && touchpos.x < 1.51 && touchpos.x > -1.44)
                    {
                        touchpos.y = -3.81f;
                        transform.position = Vector3.Lerp(transform.position , touchpos, speed * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                    swiped = false;
                    break;
            }
        }
    }//End of Update

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Tiles")
        {
            Debug.Log("Caught tiles");
            moveEn = false;
        }
    }
}
