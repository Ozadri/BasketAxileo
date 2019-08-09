using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public Text score;
    public List<ParticleSystem> fireworks;
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

    private void StartFireWorks()
    {
        foreach (ParticleSystem particleSystem in fireworks)
        {
            particleSystem.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        nbScore++;
        score.text = "Panier(s) : " + nbScore;
        StartFireWorks();
        if (nbScore >= 10)
        {
            score.text = "TU AS GAGNE ! VIENT NOUS VOIR !";
        }
    }
}
