using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    private Collider targetCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DaggerDetector")
        {
            targetCollider = other;
            Debug.Log("BLEEDING");
            ParticleEnable(other.transform.GetComponent<ParticleSystem>() , true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DaggerDetector")
        {
            ParticleEnable(other.transform.GetComponent<ParticleSystem>() , false);
        }
    }

    protected void ParticleEnable(ParticleSystem particleSystem, bool enable)
    {


        if (enable && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        else if (!enable && particleSystem.isPlaying)
        {
            particleSystem.Stop();
            particleSystem.Clear();
        } 
    }
}