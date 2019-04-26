using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    public List<Instrument> symphony;
    bool[] inGaze;
    Camera cam;
    int layer_mask;
    
    void Start()
    {
        inGaze = new bool[symphony.Count];
        cam = Camera.main;
        layer_mask = LayerMask.GetMask("Instruments");
    }

    void Update()
    {
        for(int i = 0; i < symphony.Count; ++i) {
            inGaze[i] = false;
        }
        Ray ray = new Ray(cam.transform.position, cam.transform.rotation * Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {
            if(hit.collider.GetComponent<Instrument>() != null) {
                int indexInGaze = symphony.IndexOf(hit.collider.GetComponent<Instrument>());
                inGaze[indexInGaze] = true;
            }
        }
        else
        {

        }
        
        for(int i = 0; i < symphony.Count; ++i) {
            if(inGaze[i]) {
                symphony[i].GazeEnter();
            }
            else {
                symphony[i].GazeExit();
            }
        }
    }
}
