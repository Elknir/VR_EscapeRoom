using UnityEngine;
public class PotionBehaviour : GrabableObjects
{
    public GameObject top, bot;
    protected bool isActive = false;
    public GameObject potionParticleObject;
    private ParticleSystem potionParticleSystem;
    public Ingredients ingredient;

    protected virtual void Awake()
    {
        potionParticleSystem = potionParticleObject.GetComponent<ParticleSystem>();
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


    public override void DropItem()
    {
        base.DropItem();
        ParticleEnable(false);
    }
    

    protected virtual void ParticleEnable(bool enable)
    {
        if (enable)
        {
            isActive = true;
            potionParticleSystem.Play();
            DisablePlug(false);
        }
        else
        {
            isActive = false;
            potionParticleSystem.Stop();
            potionParticleSystem.Clear();
            DisablePlug(true);
        }
    }


    protected virtual void DisablePlug(bool enable)
    {
        transform.GetChild(0).gameObject.SetActive(enable);

    }
}


