using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHands : MonoBehaviour
{
    [SerializeField] private InputActionProperty _pinchAction;
    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        // Allow to read the controllers value 
        float triggerValue = _pinchAction.action.ReadValue<float>();
        float gripValue = _gripAction.action.ReadValue<float>();

        // We set the values for the animator
        _animator.SetFloat("Trigger", triggerValue);
        _animator.SetFloat("Grip", gripValue);
    }
}
