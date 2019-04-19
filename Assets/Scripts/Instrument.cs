using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public GameObject selector;
    bool inGaze;
    // Start is called before the first frame update
    void Start()
    {
        selector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
