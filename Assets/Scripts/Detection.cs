using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Vuforia;

public class Detection : MonoBehaviour, ITrackableEventHandler
{
    public GameObject ballon;
    public float swipeDistanceThreshold = 50;
    public Text score;
    public GameObject gameplay;

    private bool ballonPretATirer = false;
    private GameObject ballonDeTir;
    private TrackableBehaviour mTrackableBehaviour;
    private Touch touch;
    private int currentScore;
    private bool detected;
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED)
        {
            detected = true;
            if (!ballonPretATirer)
            {
                ballonDeTir = Instantiate(ballon);
                ballonPretATirer = !ballonPretATirer;
            } else
            {
                ballonDeTir.SetActive(true);
            }
            
        }
        else
        {
            detected = false;
            if (ballonPretATirer)
            {
                ballonDeTir.SetActive(false);
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Trouve le logo Axileo";
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        touch = new Touch();

        detected = false;

    }

    private Vector2 startposition;
    private Vector2 endposition;
    
    // Update is called once per frame
    void Update()
    {

        currentScore = gameplay.GetComponent<gameplay>().nbScore;
        if (detected)
        {
            if (currentScore >= 10)
            {
                score.text = "TU AS GAGNE ! VIENT NOUS VOIR !";
            }
            else
            {
                score.text = "Panier(s) : " + currentScore;
            }
        }
        else
        {
            score.text = "Trouve le logo Axileo";
        }
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startposition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endposition = touch.position;
                    
                    if (analyseGesture(startposition, endposition))
                    {
                        Tir();
                    }
                    else
                    {
                        if (ballonPretATirer == false)
                        {
                            ballonDeTir = Instantiate(ballon);
                            ballonPretATirer = !ballonPretATirer;
                        }
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startposition = new Vector2(Screen.width/2,0);
            endposition = new Vector2(Screen.width / 2, Screen.height / 2);

            Tir();
        }
    }

    private bool analyseGesture(Vector2 start, Vector2 end)
    {
        if (Vector2.Distance(start, end) > swipeDistanceThreshold)
            return true;
        
        return false;
    }

    private void Tir()
    {
        Rigidbody rigBallon = ballonDeTir.GetComponent<Rigidbody>();
        rigBallon.useGravity = true;

        float x = (endposition.x - startposition.x) / (Screen.width / 4);
        float y = (endposition.y - startposition.y) / (Screen.height / 7);

        if (y > 2.7f)
        {
            y = 2.7f;
        }

        rigBallon.AddForce(new Vector3(x, y, 1.5f), ForceMode.Impulse);

        ballonDeTir = null;
        ballonPretATirer = !ballonPretATirer;
    }

}
