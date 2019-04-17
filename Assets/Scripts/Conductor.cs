using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;


public class Conductor : MonoBehaviour
{
    public static Conductor instance = null; //singleton instance
    public Baton baton;
    public AudioMixer mixer;
    public Song song;
    public TextMeshProUGUI bpmText, debugText;
    public AudioSource metronome;
    int currentIndex = 0, currentbpm;
    float lastBeatTime, currentDifference;
    bool started = false;
    float speed = 1;
    List<Beat> beats;
    const int beatBuffer = 5;
    AudioSource[] sources;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }
    void Start() {
        beats = new List<Beat>();
        sources = song.GetInstruments();
        currentDifference = Time.time - lastBeatTime; // the current unresolved waiting
        currentbpm = CalculateBPM();
        speed = ((float) currentbpm) / ((float) song.bpm); //we need a float speed
        bpmText.text = ""+currentbpm;
    }

    void BeginSong() {
        song.Play();
        InvokeRepeating(nameof(Metronome), 0, 60/(float) song.bpm);
        InvokeRepeating(nameof(ChangePitch), 1, 0.1f);
    }

    void Update()
    {
        currentDifference = Time.time - lastBeatTime; // the current unresolved waiting
    }

    public bool AdvanceBeat(int index) { //returns if it is the correct beat needed next
        if(!started && index == 0) {
            currentbpm = song.bpm;
            Debug.Log("Starting song!");
            started = true;
            BeginSong();
            currentIndex = index;
            lastBeatTime = Time.time;
            return true;
        }
        if(index == currentIndex + 1 || (index == 0 && currentIndex == song.topTimeSignature - 1)) {
            float timeStamp = Time.time;
            float newDifference = timeStamp - lastBeatTime;
            Beat newBeat = new Beat(timeStamp, newDifference);
            lastBeatTime = Time.time;
            currentIndex = index;
            beats.Add(newBeat);
            return true;
        }
        return false;
    }

    int CalculateBPM() {
        float avg = 0;
        string queue = "";
        if(beats.Count > beatBuffer) {
            beats.RemoveAt(beats.Count-1); //"dequeues" the oldest beat
        }
        for(int i = 0; i < beats.Count; ++i) {
            if(Time.time - beats[i].timeStamp > 2) {
                beats.RemoveAt(i);
            }

        }

        foreach(Beat beat in beats) {
            avg +=  beat.timeDifference;
            queue += beat.timeDifference + "\n";
        }
        if(beats.Count > 0) {
            if(currentDifference > (avg/beats.Count)) { //if it's big enough to count
                avg+=currentDifference;
                queue+=currentDifference + "\n";
                avg/=(beats.Count+1);
            }
            else { //otherwise only use the queue
                avg/=beats.Count;
            }
        }
        else {
            avg = currentDifference;
        }

        queue+="= "+avg + "\n";
        debugText.text = queue;

        int bpm = Mathf.RoundToInt(60*(1/avg));
        return bpm;
    }

    void Metronome() {
        metronome.Play();
    }

    void ChangePitch() {
        currentbpm = CalculateBPM();
        speed = ((float) currentbpm) / ((float) song.bpm); //we need a float speed
        bpmText.text = ""+currentbpm;
        
        foreach(AudioSource instrument in sources) {
            instrument.pitch = speed;
        }
        mixer.SetFloat("PitchShift", 1/speed);
    }

    public int GetTopTimeSignature() {
        return song.topTimeSignature;
    }
}
