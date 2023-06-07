using UnityEngine;


public class PotionBehaviour : MonoBehaviour
{
    public GameObject top, bot;
    protected bool isHeld = false;
    protected bool isActive = false;
    public GameObject potionParticleObject;
    private ParticleSystem potionParticleSystem;

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
    
    
    public void PotionHeld()
    {
        isHeld = true;
    }
    
    public void PotionDrop()
    {
        isHeld = false;
        ParticleEnable(false);
    }

    protected void ParticleEnable(bool enable)
    {
        if (enable)
        {
            isActive = true;
            potionParticleSystem.Play();
            Debug.Log("Activer les particules");
        }
        else
        {
            isActive = false;
            potionParticleSystem.Stop();
            potionParticleSystem.Clear(); 
            Debug.Log("Desactiver les particules");

        }
    }
}


