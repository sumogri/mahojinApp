using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisableEvents : MonoBehaviour {
    [SerializeField] private UnityEvent onDisable;

    private void OnDisable()
    {
        onDisable.Invoke();
    }
}
