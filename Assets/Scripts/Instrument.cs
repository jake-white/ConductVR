using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public Material selected, unselected;
    bool inGaze;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = unselected;        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GazeEnter() {
        if(!inGaze) {
            GetComponent<MeshRenderer>().material = selected;
        }
        inGaze = true;
    }

    public void GazeExit() {
        if(inGaze) {
            GetComponent<MeshRenderer>().material = unselected;
        }
        inGaze = false;
    }
}
