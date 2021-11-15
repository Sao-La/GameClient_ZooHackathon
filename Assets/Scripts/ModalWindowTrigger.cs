using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModalWindowTrigger : MonoBehaviour
{
    public string title;
    public Sprite sprite;
    public string message;
    public string confirmMessage = "Okay";
    public string declineMessage = "Cancel";
    public bool triggerOnEnable;

    public UnityEvent onContinueCallback = null;
    public UnityEvent onCancelCallback = null;

    private void Start()
    {
        if (!triggerOnEnable) return;

        print("Called");

        Action continueCallback = null;
        Action cancelCallback = null;

        if (onContinueCallback.GetPersistentEventCount() > 0)
        {
            continueCallback = onContinueCallback.Invoke;
        }
        if (onCancelCallback.GetPersistentEventCount() > 0)
        {
            cancelCallback = onCancelCallback.Invoke;
        }

        ModalWindowPanel.Instance.ShowModal(title, sprite, message, confirmMessage, declineMessage, continueCallback, cancelCallback);
    }

    public void OnEnable()
    {
        /*
        if (!triggerOnEnable) return;

        print("Called");

        Action continueCallback = null;
        Action cancelCallback = null;

        if (onContinueCallback.GetPersistentEventCount() > 0)
        {
            continueCallback = onContinueCallback.Invoke;
        }
        if (onCancelCallback.GetPersistentEventCount() > 0)
        {
            cancelCallback = onCancelCallback.Invoke;
        }

        ModalWindowPanel.Instance.ShowModal(title, sprite, message, confirmMessage, declineMessage, continueCallback, cancelCallback);
        */
    }
}
