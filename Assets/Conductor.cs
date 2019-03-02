using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Conductor : MonoBehaviour
{
    public Transform baton;
    public AudioMixer mixer;
    public Song song;
    public AudioSource[] sources;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangePitch", 2.0f, 0.3f);
        sources = song.GetInstruments();
    }

    // Update is called once per frame
    void Update()
    {     

    }

    void ChangePitch() {
        float pitch = baton.position.y;
        foreach(AudioSource instrument in sources) {
            instrument.pitch = pitch;
        }
        mixer.SetFloat("PitchShift", 1/pitch);
    }
}
