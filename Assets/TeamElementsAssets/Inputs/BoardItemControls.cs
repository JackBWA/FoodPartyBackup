// GENERATED AUTOMATICALLY FROM 'Assets/TeamElementsAssets/Inputs/BoardItemControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BoardItemControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BoardItemControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BoardItemControls"",
    ""maps"": [
        {
            ""name"": ""RottenTomato"",
            ""id"": ""af65e696-dfba-4b3b-ad91-88cceb68dc71"",
            ""actions"": [
                {
                    ""name"": ""Charge"",
                    ""type"": ""Button"",
                    ""id"": ""367a238a-8d40-4836-93d0-61bc8d573175"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2f3f35c1-bdf1-42ff-8acf-080e76552bce"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // RottenTomato
        m_RottenTomato = asset.FindActionMap("RottenTomato", throwIfNotFound: true);
        m_RottenTomato_Charge = m_RottenTomato.FindAction("Charge", throwIfNotFound: true);
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

    // RottenTomato
    private readonly InputActionMap m_RottenTomato;
    private IRottenTomatoActions m_RottenTomatoActionsCallbackInterface;
    private readonly InputAction m_RottenTomato_Charge;
    public struct RottenTomatoActions
    {
        private @BoardItemControls m_Wrapper;
        public RottenTomatoActions(@BoardItemControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Charge => m_Wrapper.m_RottenTomato_Charge;
        public InputActionMap Get() { return m_Wrapper.m_RottenTomato; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RottenTomatoActions set) { return set.Get(); }
        public void SetCallbacks(IRottenTomatoActions instance)
        {
            if (m_Wrapper.m_RottenTomatoActionsCallbackInterface != null)
            {
                @Charge.started -= m_Wrapper.m_RottenTomatoActionsCallbackInterface.OnCharge;
                @Charge.performed -= m_Wrapper.m_RottenTomatoActionsCallbackInterface.OnCharge;
                @Charge.canceled -= m_Wrapper.m_RottenTomatoActionsCallbackInterface.OnCharge;
            }
            m_Wrapper.m_RottenTomatoActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Charge.started += instance.OnCharge;
                @Charge.performed += instance.OnCharge;
                @Charge.canceled += instance.OnCharge;
            }
        }
    }
    public RottenTomatoActions @RottenTomato => new RottenTomatoActions(this);
    public interface IRottenTomatoActions
    {
        void OnCharge(InputAction.CallbackContext context);
    }
}
