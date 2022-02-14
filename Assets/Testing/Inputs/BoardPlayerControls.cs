// GENERATED AUTOMATICALLY FROM 'Assets/Testing/Inputs/BoardPlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BoardPlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BoardPlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BoardPlayerControls"",
    ""maps"": [
        {
            ""name"": ""Dice"",
            ""id"": ""a8b0cdc8-b719-40e5-b27b-6e62be13901c"",
            ""actions"": [
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""035d3aa0-898a-48dc-b75e-56036974e5c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""438b70d9-247f-4052-ba2f-5244744cbacc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard/Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""6b026fd0-04c6-4883-8418-44148503f8e8"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""d9570fd4-2539-4ce7-82ef-4a67e250ce69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""902b2e23-c934-48a9-9107-f9a4fbebb936"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""ad762a7e-cdd4-4e20-8a72-b7a687dd900d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""86128ad9-11cf-4d5a-87e0-8c5b8d54c1d0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard/Mouse"",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a67cb75a-4c2b-4934-a43f-4a1611b7173e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard/Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96dcaaae-c5c7-44be-9425-27da77e330f6"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard/Mouse"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard/Mouse"",
            ""bindingGroup"": ""Keyboard/Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Dice
        m_Dice = asset.FindActionMap("Dice", throwIfNotFound: true);
        m_Dice_Throw = m_Dice.FindAction("Throw", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Toggle = m_Inventory.FindAction("Toggle", throwIfNotFound: true);
        m_Inventory_Select = m_Inventory.FindAction("Select", throwIfNotFound: true);
        m_Inventory_Use = m_Inventory.FindAction("Use", throwIfNotFound: true);
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

    // Dice
    private readonly InputActionMap m_Dice;
    private IDiceActions m_DiceActionsCallbackInterface;
    private readonly InputAction m_Dice_Throw;
    public struct DiceActions
    {
        private @BoardPlayerControls m_Wrapper;
        public DiceActions(@BoardPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Throw => m_Wrapper.m_Dice_Throw;
        public InputActionMap Get() { return m_Wrapper.m_Dice; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DiceActions set) { return set.Get(); }
        public void SetCallbacks(IDiceActions instance)
        {
            if (m_Wrapper.m_DiceActionsCallbackInterface != null)
            {
                @Throw.started -= m_Wrapper.m_DiceActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_DiceActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_DiceActionsCallbackInterface.OnThrow;
            }
            m_Wrapper.m_DiceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
            }
        }
    }
    public DiceActions @Dice => new DiceActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_Toggle;
    private readonly InputAction m_Inventory_Select;
    private readonly InputAction m_Inventory_Use;
    public struct InventoryActions
    {
        private @BoardPlayerControls m_Wrapper;
        public InventoryActions(@BoardPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_Inventory_Toggle;
        public InputAction @Select => m_Wrapper.m_Inventory_Select;
        public InputAction @Use => m_Wrapper.m_Inventory_Use;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @Toggle.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToggle;
                @Toggle.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToggle;
                @Toggle.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToggle;
                @Select.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelect;
                @Use.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUse;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Toggle.started += instance.OnToggle;
                @Toggle.performed += instance.OnToggle;
                @Toggle.canceled += instance.OnToggle;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard/Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IDiceActions
    {
        void OnThrow(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnToggle(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
    }
}
