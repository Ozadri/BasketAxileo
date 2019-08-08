using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public Text score;
    private int nbScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Panier(s) : " + nbScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        nbScore = nbScore + 1;
        score.text = "Panier(s) : " + nbScore;

        if (nbScore >= 10)
        {
            score.text = "TU AS GAGNE ! VIENT NOUS VOIR !";
        }
    }
}
