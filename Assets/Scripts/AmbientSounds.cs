using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{

    private List<Transform> childTransforms;
    private float timer;

    [SerializeField] private float lowTimerLimit = 25;
    [SerializeField] private float highTimerLimit = 50;
    private float currentTimerLimit;

    private List<string> sounds;

    private void Start()
    {
        timer = 0;
        currentTimerLimit = Random.Range(lowTimerLimit, highTimerLimit);

        childTransforms = new List<Transform>();
        sounds = new List<string>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childTransforms.Add(transform.GetChild(i));
        }

        sounds.Add("event:/Environment/Ambiance/Env_Amb_PianoChord");
        sounds.Add("event:/Environment/Ambiance/Env_Amb_Stinger");
        sounds.Add("event:/Environment/Interior/Env_Int_FastCreakingWood");
        sounds.Add("event:/Environment/Interior/Env_Int_HeavyCreakingWood");
        sounds.Add("event:/Environment/Interior/Env_Int_LightCreakingWood");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > currentTimerLimit)
        {
            int randT = Random.Range(0, childTransforms.Count);
            int randS = Random.Range(0, sounds.Count);
            FMODUnity.RuntimeManager.PlayOneShot(sounds[randS], childTransforms[randT].position);
            timer = 0;
            currentTimerLimit = Random.Range(lowTimerLimit, highTimerLimit);
        }
    }
}
