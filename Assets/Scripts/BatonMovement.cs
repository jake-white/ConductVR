using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BatonMovement : MonoBehaviour
{
    Rigidbody body;
    public SteamVR_Behaviour_Pose trackedObj;
    public ControllerHighlight controller;
    public Transform camera;
    public Light displayLight;
    Color[] beatColors = {Color.blue, Color.green, Color.red, Color.yellow};

    void Update()
    {

        float rotY = Mathf.Deg2Rad * camera.rotation.eulerAngles.y;
        //unity will end up giving rotY between -180 and 180 for some reason, so we need to correct this.
        if(rotY < 0) {
            rotY += 360;
        }

        float modX = Mathf.Cos(rotY);
        float modZ = Mathf.Sin(rotY);


        float velX = trackedObj.GetVelocity().x * modX;
        float velY = trackedObj.GetVelocity().y;
        float velZ = -trackedObj.GetVelocity().z * modZ;
        
        float horizontalVelocity = velX + velZ;
        float verticalVelocity = velY;
        if(controller.IsHolding()) {
            //checks every time signature
            int detectedMotions = 0, detectedBeat = 0;
            if(Conductor.instance.GetTopTimeSignature() == 2) {
                if(horizontalVelocity > 1) {
                    detectedMotions++;
                    detectedBeat = 0;
                }
                else if(horizontalVelocity < -1) {
                    detectedMotions++;
                    detectedBeat = 1;
                }
            }
            else if(Conductor.instance.GetTopTimeSignature() == 4) {
                if(verticalVelocity < -1) {
                    detectedMotions++;
                    detectedBeat = 0;
                }
                else if(horizontalVelocity < -1) {
                    detectedMotions++;
                    detectedBeat = 1;
                }
                else if(horizontalVelocity > 1) {
                    detectedMotions++;
                    detectedBeat = 2;
                }
                else if(verticalVelocity > 1) {
                    detectedMotions++;
                    detectedBeat = 3;
                }
            }

            if(detectedMotions == 1) { //exactly 1 motion detected
                if(Conductor.instance.AdvanceBeat(detectedBeat)) {
                    displayLight.color = beatColors[detectedBeat];
                }
            }
        }
    }
}
