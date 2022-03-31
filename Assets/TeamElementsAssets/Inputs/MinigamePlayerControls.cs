// GENERATED AUTOMATICALLY FROM 'Assets/TeamElementsAssets/Inputs/MinigamePlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MinigamePlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MinigamePlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MinigamePlayerControls"",
    ""maps"": [
        {
            ""name"": ""DontGetBurnt"",
            ""id"": ""a1ce6325-b9ff-4164-a21a-6df99e84c145"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""444bdc2f-16c0-4362-8cde-080b243ac1d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f826d704-cd91-4ea9-ad47-976d39040945"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fa931649-c44e-4de8-9f98-4bec116a5d0f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Axis"",
                    ""id"": ""442c86b1-fee9-46b5-b950-24ba86423703"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aba5e82e-d075-4e6f-b454-1d5412158bfe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fe0a42d5-4e81-46bd-ad55-4d7031745014"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4976644a-da59-4f91-81b8-c1c8530f2e5f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b2205b44-fbc1-4d98-87f8-2542c26a7c52"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DontGetBurnt
        m_DontGetBurnt = asset.FindActionMap("DontGetBurnt", throwIfNotFound: true);
        m_DontGetBurnt_Move = m_DontGetBurnt.FindAction("Move", throwIfNotFound: true);
        m_DontGetBurnt_Jump = m_DontGetBurnt.FindAction("Jump", throwIfNotFound: true);
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

    // DontGetBurnt
    private readonly InputActionMap m_DontGetBurnt;
    private IDontGetBurntActions m_DontGetBurntActionsCallbackInterface;
    private readonly InputAction m_DontGetBurnt_Move;
    private readonly InputAction m_DontGetBurnt_Jump;
    public struct DontGetBurntActions
    {
        private @MinigamePlayerControls m_Wrapper;
        public DontGetBurntActions(@MinigamePlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_DontGetBurnt_Move;
        public InputAction @Jump => m_Wrapper.m_DontGetBurnt_Jump;
        public InputActionMap Get() { return m_Wrapper.m_DontGetBurnt; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DontGetBurntActions set) { return set.Get(); }
        public void SetCallbacks(IDontGetBurntActions instance)
        {
            if (m_Wrapper.m_DontGetBurntActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_DontGetBurntActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DontGetBurntActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DontGetBurntActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_DontGetBurntActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_DontGetBurntActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_DontGetBurntActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_DontGetBurntActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public DontGetBurntActions @DontGetBurnt => new DontGetBurntActions(this);
    public interface IDontGetBurntActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
