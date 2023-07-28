using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionBehaviour : GrabableObjects
{
    public GameObject top, bot;
    protected bool isActive = false;
    public GameObject potionParticleObject;
    private ParticleSystem potionParticleSystem;
    public Ingredients ingredient;

    private FMOD.Studio.EventInstance fmodEvent;

    
    protected virtual void Awake()
    {
        base.Awake();
        potionParticleSystem = potionParticleObject.GetComponent<ParticleSystem>();

        fmodEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Manipulation/Potion/Jar/Man_Jar_Pouring");
    }

    private void Update()
    {
        if (isHeld)
        {
            PotionLogic();
        }
    }

    protected virtual void PotionLogic()
    {
        if (top.transform.position.y < bot.transform.position.y && !isActive)
        {
            ParticleEnable(true);
                
        }else if (top.transform.position.y > bot.transform.position.y && isActive)
        {
            ParticleEnable(false);
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Manipulation/Potion/Jar/Man_Jar_PickUp");
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        ParticleEnable(false);
    }
    

    protected virtual void ParticleEnable(bool enable)
    {
        if (enable)
        {
            isActive = true;
            potionParticleSystem.Play();
            DisablePlug(false);
            fmodEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            fmodEvent.start();
        }
        else
        {
            isActive = false;
            potionParticleSystem.Stop();
            potionParticleSystem.Clear();
            DisablePlug(true);
            fmodEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }


    protected virtual void DisablePlug(bool enable)
    {
        transform.GetChild(0).gameObject.SetActive(enable);

    }
}


