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
            ""name"": ""General"",
            ""id"": ""9912fb09-0e76-449e-90f6-bce987528c11"",
            ""actions"": [
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""bbd6c90d-d8f8-4f69-b671-129687453031"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""50b46a01-e300-4fe2-a71b-0b6343003e34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f5459547-a4db-4aa1-8fec-462bb3e1a909"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83f1db02-e636-4d5e-80dd-078fe49ef126"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
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
                    ""path"": ""<Keyboard>/c"",
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
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_Cancel = m_General.FindAction("Cancel", throwIfNotFound: true);
        m_General_Use = m_General.FindAction("Use", throwIfNotFound: true);
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

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_Cancel;
    private readonly InputAction m_General_Use;
    public struct GeneralActions
    {
        private @BoardItemControls m_Wrapper;
        public GeneralActions(@BoardItemControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cancel => m_Wrapper.m_General_Cancel;
        public InputAction @Use => m_Wrapper.m_General_Use;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @Cancel.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancel;
                @Use.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnUse;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);

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
    public interface IGeneralActions
    {
        void OnCancel(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
    }
    public interface IRottenTomatoActions
    {
        void OnCharge(InputAction.CallbackContext context);
    }
}
