using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public Text score;
    public List<ParticleSystem> fireworks;
    public int nbScore = 0;
    public AudioClip musicClip;
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicClip;
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
        
        musicSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        nbScore++;
        score.text = "SCORE : " + nbScore;
        StartFireWorks();
    }
}
