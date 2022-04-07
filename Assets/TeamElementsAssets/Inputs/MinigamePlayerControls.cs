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
        },
        {
            ""name"": ""FoodCatcher"",
            ""id"": ""cbe91236-d045-4fa8-adcd-88e67c51458d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""553cb790-bfc0-4c9d-ad58-e5d6b94107a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a55712d6-6005-4692-ae0b-00db8a5b17ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""423f673b-5b11-401b-b4b4-4cfca0edfa44"",
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
                    ""id"": ""39e2064d-51f6-42a8-825e-65e87d0dff39"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""a1c639d9-cdbd-4b58-9b03-7d44e248b1c0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""39a11661-cdba-4431-b1bc-4afbb1b90be1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""KingOfTheHill"",
            ""id"": ""9a4f8e1d-07c2-4389-a417-7d16134046d5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""cea55013-6cda-4819-95be-7592183e23a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d6b4fd75-668d-48f8-a242-d2172c68af97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Punch"",
                    ""type"": ""Button"",
                    ""id"": ""0997090d-a5f0-4802-abd6-0b054fd14074"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""Value"",
                    ""id"": ""b1df60fd-a4fb-4bd4-8032-0fa9d540dadf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""84342f54-ec05-4848-b1ed-26f3aae4108f"",
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
                    ""id"": ""565eb357-6904-48e5-bfb6-50f0fc4554ab"",
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
                    ""id"": ""a05ad639-2185-4705-9e65-4eba841f83a5"",
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
                    ""id"": ""a5fd4092-d03e-42a2-be7d-03eb03abf028"",
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
                    ""id"": ""438886ad-b3db-486a-96be-c734455004a4"",
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
                    ""id"": ""044f3165-fb52-4daa-a609-dcb0b8998ceb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""72a0f122-a060-4e58-b153-1ebf779246a6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Punch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b106fc2-3c1f-40ab-a4b7-75a134dc5501"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RaceToTheTop"",
            ""id"": ""0fab18ce-5f6a-46b5-b9ee-3d037f8474cd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""335d8486-43bc-444f-9986-10931acf846c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a582aae4-0c43-441a-9c2e-c7a033691f2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""449865c2-a34c-4065-bac3-fbe6ba56a156"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""31118e17-54ce-4902-a2d6-de7761fa8449"",
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
                    ""id"": ""200cc56c-195e-4b96-914c-1fbdb9d68d1f"",
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
                    ""id"": ""389697c1-aaf9-4ff9-b766-11bfa792db53"",
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
                    ""id"": ""c1b088d2-a3ad-444e-9190-4a333b57264b"",
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
                    ""id"": ""9c3cb1f6-0053-4a64-8381-2ad4816ef9f8"",
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
                    ""id"": ""610b25af-0508-40d4-9669-c36bf3ff664d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8786a1e7-6fc4-4506-874a-b20d584c2cc1"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        // FoodCatcher
        m_FoodCatcher = asset.FindActionMap("FoodCatcher", throwIfNotFound: true);
        m_FoodCatcher_Move = m_FoodCatcher.FindAction("Move", throwIfNotFound: true);
        m_FoodCatcher_Jump = m_FoodCatcher.FindAction("Jump", throwIfNotFound: true);
        // KingOfTheHill
        m_KingOfTheHill = asset.FindActionMap("KingOfTheHill", throwIfNotFound: true);
        m_KingOfTheHill_Move = m_KingOfTheHill.FindAction("Move", throwIfNotFound: true);
        m_KingOfTheHill_Jump = m_KingOfTheHill.FindAction("Jump", throwIfNotFound: true);
        m_KingOfTheHill_Punch = m_KingOfTheHill.FindAction("Punch", throwIfNotFound: true);
        m_KingOfTheHill_MouseLook = m_KingOfTheHill.FindAction("MouseLook", throwIfNotFound: true);
        // RaceToTheTop
        m_RaceToTheTop = asset.FindActionMap("RaceToTheTop", throwIfNotFound: true);
        m_RaceToTheTop_Move = m_RaceToTheTop.FindAction("Move", throwIfNotFound: true);
        m_RaceToTheTop_Jump = m_RaceToTheTop.FindAction("Jump", throwIfNotFound: true);
        m_RaceToTheTop_Dodge = m_RaceToTheTop.FindAction("Dodge", throwIfNotFound: true);
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

    // FoodCatcher
    private readonly InputActionMap m_FoodCatcher;
    private IFoodCatcherActions m_FoodCatcherActionsCallbackInterface;
    private readonly InputAction m_FoodCatcher_Move;
    private readonly InputAction m_FoodCatcher_Jump;
    public struct FoodCatcherActions
    {
        private @MinigamePlayerControls m_Wrapper;
        public FoodCatcherActions(@MinigamePlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_FoodCatcher_Move;
        public InputAction @Jump => m_Wrapper.m_FoodCatcher_Jump;
        public InputActionMap Get() { return m_Wrapper.m_FoodCatcher; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FoodCatcherActions set) { return set.Get(); }
        public void SetCallbacks(IFoodCatcherActions instance)
        {
            if (m_Wrapper.m_FoodCatcherActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FoodCatcherActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FoodCatcherActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FoodCatcherActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_FoodCatcherActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_FoodCatcherActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_FoodCatcherActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_FoodCatcherActionsCallbackInterface = instance;
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
    public FoodCatcherActions @FoodCatcher => new FoodCatcherActions(this);

    // KingOfTheHill
    private readonly InputActionMap m_KingOfTheHill;
    private IKingOfTheHillActions m_KingOfTheHillActionsCallbackInterface;
    private readonly InputAction m_KingOfTheHill_Move;
    private readonly InputAction m_KingOfTheHill_Jump;
    private readonly InputAction m_KingOfTheHill_Punch;
    private readonly InputAction m_KingOfTheHill_MouseLook;
    public struct KingOfTheHillActions
    {
        private @MinigamePlayerControls m_Wrapper;
        public KingOfTheHillActions(@MinigamePlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_KingOfTheHill_Move;
        public InputAction @Jump => m_Wrapper.m_KingOfTheHill_Jump;
        public InputAction @Punch => m_Wrapper.m_KingOfTheHill_Punch;
        public InputAction @MouseLook => m_Wrapper.m_KingOfTheHill_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_KingOfTheHill; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KingOfTheHillActions set) { return set.Get(); }
        public void SetCallbacks(IKingOfTheHillActions instance)
        {
            if (m_Wrapper.m_KingOfTheHillActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnJump;
                @Punch.started -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnPunch;
                @Punch.performed -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnPunch;
                @Punch.canceled -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnPunch;
                @MouseLook.started -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_KingOfTheHillActionsCallbackInterface.OnMouseLook;
            }
            m_Wrapper.m_KingOfTheHillActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Punch.started += instance.OnPunch;
                @Punch.performed += instance.OnPunch;
                @Punch.canceled += instance.OnPunch;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
            }
        }
    }
    public KingOfTheHillActions @KingOfTheHill => new KingOfTheHillActions(this);

    // RaceToTheTop
    private readonly InputActionMap m_RaceToTheTop;
    private IRaceToTheTopActions m_RaceToTheTopActionsCallbackInterface;
    private readonly InputAction m_RaceToTheTop_Move;
    private readonly InputAction m_RaceToTheTop_Jump;
    private readonly InputAction m_RaceToTheTop_Dodge;
    public struct RaceToTheTopActions
    {
        private @MinigamePlayerControls m_Wrapper;
        public RaceToTheTopActions(@MinigamePlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_RaceToTheTop_Move;
        public InputAction @Jump => m_Wrapper.m_RaceToTheTop_Jump;
        public InputAction @Dodge => m_Wrapper.m_RaceToTheTop_Dodge;
        public InputActionMap Get() { return m_Wrapper.m_RaceToTheTop; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RaceToTheTopActions set) { return set.Get(); }
        public void SetCallbacks(IRaceToTheTopActions instance)
        {
            if (m_Wrapper.m_RaceToTheTopActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnJump;
                @Dodge.started -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_RaceToTheTopActionsCallbackInterface.OnDodge;
            }
            m_Wrapper.m_RaceToTheTopActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
            }
        }
    }
    public RaceToTheTopActions @RaceToTheTop => new RaceToTheTopActions(this);
    public interface IDontGetBurntActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IFoodCatcherActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IKingOfTheHillActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPunch(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
    }
    public interface IRaceToTheTopActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
    }
}
