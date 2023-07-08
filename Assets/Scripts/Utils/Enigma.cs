using System;
using UnityEngine;
using UnityEngine.Events;

public class EnigmaEvent : UnityEvent<EnigmaEnum, Action>
{
}

public abstract class Enigma : MonoBehaviour

{
    public EnigmaEnum currentEnigma;
    private GameManager gameManager;
    private EnigmaEvent enigmaEvent;

    private Action validateCallBack;

    private void Awake()
    {
        gameManager = transform.parent.GetComponent<GameManager>();
        if (enigmaEvent == null) enigmaEvent = new EnigmaEvent();
        enigmaEvent.AddListener(gameManager.ReciveSignal);
    }

    private void SendToGameManager(EnigmaEnum targetEngima)
    {
        //Event pour pas pouvoir le trigger 2 fois
        
        //envoyer une action //faire le validate
        enigmaEvent.Invoke(targetEngima, () => {ValidEnigma();});
        enigmaEvent.RemoveAllListeners();
        
    }

    //Permet de voir si la condition est validée
    protected  void CheckEnigmaValidation(EnigmaEnum targetEngima)
    {
        if (EnigmaCondition())
        {
            SendToGameManager(targetEngima);
        }
    }

    //A changer dans les parents pour pouvoir changer les conditions de validation
    protected abstract bool EnigmaCondition();
    //Si l'enigme a été validé par le gameManager
    //Need to be public for the gameManager to access it
    public abstract void ValidEnigma();
}

    