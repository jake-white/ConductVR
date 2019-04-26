using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public GameObject selector;
    bool inGaze;
    
    void Start()
    {
        selector.SetActive(false);
    }

    public void GazeEnter() {
        if(!inGaze) {
            selector.SetActive(true);
            GetComponent<AudioSource>().volume = 1;
        }
        inGaze = true;
    }

    public void GazeExit() {
        if(inGaze) {
            selector.SetActive(false);
            GetComponent<AudioSource>().volume = 0.5f;
        }
        inGaze = false;
    }
}
