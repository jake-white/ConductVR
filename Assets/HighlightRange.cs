using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightRange : MonoBehaviour
{
    bool inRange_left = false, inRange_right = false;

    public void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Hand_Left") {
            inRange_left = true;
        }
        else if(collider.tag == "Hand_Right") {
            inRange_right = true;
        }
    }
    
    public void OnTriggerExit(Collider collider) {
        if(collider.tag == "Hand_Left") {
            inRange_left = false;
        }
        else if(collider.tag == "Hand_Right") {
            inRange_right = false;
        }
    }

    public bool InRange_Left() {
        return inRange_left;
    }

    public bool InRange_Right() {
        return inRange_right;
    }
}
