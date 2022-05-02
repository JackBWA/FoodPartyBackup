// GENERATED AUTOMATICALLY FROM 'Assets/TeamElementsAssets/Inputs/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Pause"",
            ""id"": ""63053ca6-1fef-4b1d-8e7d-e796fa402132"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""6b83c639-73f7-4904-ba52-6559ccef5808"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""lalalalala"",
                    ""type"": ""Button"",
                    ""id"": ""97c3a4c0-2421-454d-bed7-ae424843bbc1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d41da2f9-7e95-4b3e-80fe-969acb5886f6"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42e75928-412b-4f1d-a7f5-ea1b6f1a8fee"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6c5c1e4-ca99-4d80-9147-339a5e9bfbfe"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lalalalala"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_Toggle = m_Pause.FindAction("Toggle", throwIfNotFound: true);
        m_Pause_lalalalala = m_Pause.FindAction("lalalalala", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Pause
    private readonly InputActionMap m_Pause;
    private IPauseActions m_PauseActionsCallbackInterface;
    private readonly InputAction m_Pause_Toggle;
    private readonly InputAction m_Pause_lalalalala;
    public struct PauseActions
    {
        private @PlayerActions m_Wrapper;
        public PauseActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_Pause_Toggle;
        public InputAction @lalalalala => m_Wrapper.m_Pause_lalalalala;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterface != null)
            {
                @Toggle.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnToggle;
                @Toggle.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnToggle;
                @Toggle.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnToggle;
                @lalalalala.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnLalalalala;
                @lalalalala.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnLalalalala;
                @lalalalala.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnLalalalala;
            }
            m_Wrapper.m_PauseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Toggle.started += instance.OnToggle;
                @Toggle.performed += instance.OnToggle;
                @Toggle.canceled += instance.OnToggle;
                @lalalalala.started += instance.OnLalalalala;
                @lalalalala.performed += instance.OnLalalalala;
                @lalalalala.canceled += instance.OnLalalalala;
            }
        }
    }
    public PauseActions @Pause => new PauseActions(this);
    public interface IPauseActions
    {
        void OnToggle(InputAction.CallbackContext context);
        void OnLalalalala(InputAction.CallbackContext context);
    }
}
