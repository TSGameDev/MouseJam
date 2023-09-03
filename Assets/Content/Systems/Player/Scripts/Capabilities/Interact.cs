using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public delegate void OnInteract(GameObject _Player, Currency _PlayerCurreny);
    public OnInteract Interaction;

    [SerializeField] private InputManagerBase controls;
    private Currency _Curreny;

    private void Awake()
    {
        _Curreny = GetComponent<Currency>();
    }

    private void Update()
    {
        if(controls.RetrieveInteraction())
        {
            Interaction.Invoke(gameObject, _Curreny);
        }
    }
}
