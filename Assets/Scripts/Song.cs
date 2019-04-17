using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public string songName;
    public int bpm;
    public int topTimeSignature = 2;
    public AudioSource[] instruments;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource[] GetInstruments() {
        return instruments;
    }

    public void Play() {
        foreach(AudioSource part in instruments) {
            part.Play();
        }
    }
}
