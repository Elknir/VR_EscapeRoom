using UnityEngine;
using UnityEngine.Events;

public class EnigmaEvent : UnityEvent<EnigmaEnum>
{
}

public abstract class Enigma : MonoBehaviour

{

    private GameManager gameManager;
    private EnigmaEvent enigmaEvent;

    private void Awake()
    {
        gameManager = transform.parent.GetComponent<GameManager>();
        if (enigmaEvent == null) enigmaEvent = new EnigmaEvent();
        enigmaEvent.AddListener(gameManager.ReciveSignal);
    }

    private void ValidEnigma(EnigmaEnum targetEngima)
    {
        //Event pour pas pouvoir le trigger 2 fois
        enigmaEvent.Invoke(targetEngima);
        enigmaEvent.RemoveAllListeners();
    }

    //Permet de voir si la condition est validée
    protected void CheckEnigmaValidation(EnigmaEnum targetEngima)
    {
        if (EnigmaCondition())
        {
            ValidEnigma(targetEngima);
        }
    }

    //A changer dans les parents pour pouvoir changer les conditions de validation
    protected abstract bool EnigmaCondition();
}

    