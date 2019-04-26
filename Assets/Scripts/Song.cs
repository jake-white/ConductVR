using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public string songName;
    public int bpm;
    public int topTimeSignature = 2;
    public AudioSource[] instruments;

    public AudioSource[] GetInstruments() {
        return instruments;
    }

    public void Play() {
        foreach(AudioSource part in instruments) {
            part.GetComponent<Animator>().Play("Play");
            part.Play();
        }
    }
}
