using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BatonMovement : MonoBehaviour
{
    Rigidbody body;
    public SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean conduct;
    public Transform camera;
    public Light displayLight;

    // Update is called once per frame
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
        float velZ = -trackedObj.GetVelocity().z * modZ;
        
        float linearVelocity = velX + velZ;
        if(conduct.state) {
            if(linearVelocity > 1) {
                Conductor.instance.AdvanceBeat(0);
                displayLight.color = Color.blue;
            }
            else if(linearVelocity < -1) {
                Conductor.instance.AdvanceBeat(1);
                displayLight.color = Color.green;
            }
        }
    }
}
