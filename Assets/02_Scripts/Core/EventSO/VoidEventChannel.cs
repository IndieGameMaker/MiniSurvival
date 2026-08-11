using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Mini Survival/VoidEventChannel")]
public class VoidEventChannel : ScriptableObject
{
    // Action
    private Action _listeners;
    
    public void Raise() => _listeners?.Invoke();
    public void Register(Action listener) => _listeners += listener;
    public void Unregister(Action listener) => _listeners -= listener;

    private void OnDisable() => _listeners = null;
}
