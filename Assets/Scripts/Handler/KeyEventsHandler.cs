using ImmersivePiano;
using Oculus.Interaction;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;
using System.Collections;

/// <summary>
/// Handles every interactions with the Piano Key
/// </summary>
public class KeyEventsHandler : MonoBehaviour
{
    //Config
    [Tooltip("The IInteractableView (Interactable) component to wrap.")]
    [SerializeField, Interface(typeof(IInteractableView))]
    private UnityEngine.Object _interactableView;
    private IInteractableView InteractableView;

    [Tooltip("Raised when an Interactor selects the Interactable.")]
    [SerializeField]
    private UnityEvent _whenSelect;

    [Tooltip("Raised when an Interactor unselects the Interactable.")]
    [SerializeField]
    private UnityEvent _whenUnselect;

    [Tooltip("Keep raising while being interacted.")]
    [SerializeField]
    private float _pressedTime;
    private bool _pressed = false;

    public UnityEvent WhenSelect => _whenSelect;
    public UnityEvent WhenUnselect => _whenUnselect;

    public bool _started = false;

    protected virtual void Awake()
    {
        InteractableView = _interactableView as IInteractableView;
        if (InteractableView == null)
        {
            Debug.LogError("Missing IInteractableView on child GameObject");
        }

        if (gameObject.TryGetComponent(out IPressable ipressable))
        {
            WhenSelect.AddListener(ipressable.Press);
        }

    }
    protected virtual void Start()
    {
        this.BeginStart(ref _started); //turn start into false so OnEnable and OnDisable methods can be skipped
        this.AssertField(InteractableView, nameof(InteractableView));
        this.EndStart(ref _started);
    }

    protected virtual void OnEnable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged += HandleStateChanged;
        }
    }

    private void HandleStateChanged(InteractableStateChangeArgs args)
    {
        switch (args.NewState)
        {
            case InteractableState.Normal:
                break;
            case InteractableState.Hover:
                if (args.PreviousState == InteractableState.Select)
                {
                    _whenUnselect.Invoke();
                }
                break;
            case InteractableState.Select:
                _whenSelect.Invoke();
                break;
        }
    }

    protected virtual void OnDisable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged -= HandleStateChanged;
        }
    }

    public void InjectKeyEventsHandler(IInteractableView interactableView)
    {
        _interactableView = interactableView as UnityEngine.Object;
        InteractableView = interactableView;
    }
    public void InjectAllKeyEventsHandler(IInteractableView interactableView)
    {
        InjectKeyEventsHandler(interactableView);
    }

}
