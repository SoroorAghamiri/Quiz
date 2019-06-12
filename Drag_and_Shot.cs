using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag_and_Shot : MonoBehaviour {

    public Ball_DANDS Ball;
    public Obstacle_DANDS obs;
    private Ball_DANDS Ballclone;
    private Obstacle_DANDS obsclone;


    public int point = 0;
    public int seconds = 0;
    public static Vector2 ballpos;
    public static Vector2 obsPos;
    public bool record = false;
    public bool faultfree = false;
    private bool timeEnabled;
    public int timeleft = 5;

    public GameObject StartPanel;
    public GameObject EndPanel;
    public UPersian.Components.RtlText pointsTxt;

    //Change to OnEnable
    void OnEnable()
    {
        faultfree = true;
        ballpos = new Vector2(-0.94f, -3);
        obsPos = new Vector2(0.88f, -3.81f);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        StartPanel.SetActive(true);
        StartCoroutine("PanelTimer");
    }
    public void shoot()
    {
            Ballclone = Instantiate(Ball);
            obsclone = Instantiate(obs);
            //Ball.Create();
            obs.transform.position = obsPos;
        
    }

    void Update()
    {
        pointsTxt.text = "امتیاز = " + point.ToString();
        
    }    

    public void OnGameFinished()
    {
        //see the resaults, close the game and go to mainScene
        GameObject ballclone = GameObject.FindGameObjectWithTag("Ball");
        Destroy(ballclone);
        GameObject obstacleclone = GameObject.FindGameObjectWithTag("Obstacle");
        Destroy(obstacleclone);
        GameObject arrowclone = GameObject.FindGameObjectWithTag("Arrow");
        Destroy(arrowclone);
        gameObject.SetActive(false);
    }

    IEnumerator PanelTimer()
    {
        yield return new WaitForSeconds(seconds);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        StartPanel.SetActive(false);
        EndPanel.SetActive(false);
        if(record == true)
        {
            StartCoroutine("LoseTime");
        }
        shoot();
    }
    IEnumerator LoseTime()
    {
        while (timeEnabled == true)
        {
            yield return new WaitForSeconds(1);
            timeleft--;
        }
    }
}
