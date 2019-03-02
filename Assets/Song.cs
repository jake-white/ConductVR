using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public string name;
    public int bpm;    
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
}
