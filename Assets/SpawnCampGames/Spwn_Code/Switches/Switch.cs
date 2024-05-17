using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SPWN;

public class Switch : MonoBehaviour, IInteractable
{
    public UnityEvent OnSwitchUp;
    public UnityEvent OnSwitchDown;

    [SerializeField]
    SwitchType switchType;

    [SerializeField]
    bool hasPower = true;
    bool hasActivated;

    public float timeTilReset = 2.5f;
    float switchTime;

    private void Awake()
    {
        OnSwitchUp.AddListener(Activate);
        OnSwitchDown.AddListener(DeActivate);
    }

    private void OnDisable()
    {
        switchTime = 0;
        hasActivated = false;

        OnSwitchUp.RemoveListener(Activate);
        OnSwitchDown.RemoveListener(DeActivate);
    }

    public void Activate() => hasActivated = true;
    public void DeActivate() => hasActivated = false;

    public void Update()
    {
        if(hasActivated && switchType == SwitchType.Repeatitive)
        {
            switchTime += Time.deltaTime;

            if(switchTime >= timeTilReset)
            {
                OnSwitchDown?.Invoke();
            }
        }
    }

    public void Interact()
    {
        if(hasPower)
        {
            switch(switchType)
            {
                case SwitchType.Single:
                if(!hasActivated)
                {
                    OnSwitchUp?.Invoke();
                }
                break;

                case SwitchType.Repeatitive:
                //check if timer is reset
                if(!hasActivated)
                {
                    OnSwitchUp?.Invoke();
                    switchTime = 0;
                }
                break;

                case SwitchType.Toggle:
                if(!hasActivated)
                {
                    OnSwitchUp?.Invoke();
                }
                else
                {
                    OnSwitchDown?.Invoke();
                }
                break;

                case SwitchType.None:
                Debug.Log("Switch is Non-Functional");
                break;
            }
        }
    }
}
