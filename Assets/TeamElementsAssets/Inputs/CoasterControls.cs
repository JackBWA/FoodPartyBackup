// GENERATED AUTOMATICALLY FROM 'Assets/TeamElementsAssets/Inputs/CoasterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CoasterControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CoasterControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CoasterControls"",
    ""maps"": [
        {
            ""name"": ""Shop"",
            ""id"": ""6a9e1a74-9996-4c0f-bc7e-8f60034f9536"",
            ""actions"": [
                {
                    ""name"": ""Buy"",
                    ""type"": ""Button"",
                    ""id"": ""a28722ae-a974-4708-ac2c-e88c6c44e9e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sell"",
                    ""type"": ""Button"",
                    ""id"": ""3b14a092-15fb-491c-8901-2ae24ea8ffda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IncreaseAmount"",
                    ""type"": ""Button"",
                    ""id"": ""bbb00fa8-81ac-48bd-a689-59f437119309"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DecreaseAmount"",
                    ""type"": ""Button"",
                    ""id"": ""62b0304e-ca03-432c-a423-7e6b3c54d64b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b07fe583-44e2-4191-98d7-1fa4aa9404ed"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Buy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea6c1627-ba77-4bd7-9c96-f035a19609ec"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4659847e-6e51-4a4c-a238-4cbfc87d6700"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseAmount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbeac59f-0176-4961-b7ae-d607d60d4260"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DecreaseAmount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Shop
        m_Shop = asset.FindActionMap("Shop", throwIfNotFound: true);
        m_Shop_Buy = m_Shop.FindAction("Buy", throwIfNotFound: true);
        m_Shop_Sell = m_Shop.FindAction("Sell", throwIfNotFound: true);
        m_Shop_IncreaseAmount = m_Shop.FindAction("IncreaseAmount", throwIfNotFound: true);
        m_Shop_DecreaseAmount = m_Shop.FindAction("DecreaseAmount", throwIfNotFound: true);
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

    // Shop
    private readonly InputActionMap m_Shop;
    private IShopActions m_ShopActionsCallbackInterface;
    private readonly InputAction m_Shop_Buy;
    private readonly InputAction m_Shop_Sell;
    private readonly InputAction m_Shop_IncreaseAmount;
    private readonly InputAction m_Shop_DecreaseAmount;
    public struct ShopActions
    {
        private @CoasterControls m_Wrapper;
        public ShopActions(@CoasterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Buy => m_Wrapper.m_Shop_Buy;
        public InputAction @Sell => m_Wrapper.m_Shop_Sell;
        public InputAction @IncreaseAmount => m_Wrapper.m_Shop_IncreaseAmount;
        public InputAction @DecreaseAmount => m_Wrapper.m_Shop_DecreaseAmount;
        public InputActionMap Get() { return m_Wrapper.m_Shop; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShopActions set) { return set.Get(); }
        public void SetCallbacks(IShopActions instance)
        {
            if (m_Wrapper.m_ShopActionsCallbackInterface != null)
            {
                @Buy.started -= m_Wrapper.m_ShopActionsCallbackInterface.OnBuy;
                @Buy.performed -= m_Wrapper.m_ShopActionsCallbackInterface.OnBuy;
                @Buy.canceled -= m_Wrapper.m_ShopActionsCallbackInterface.OnBuy;
                @Sell.started -= m_Wrapper.m_ShopActionsCallbackInterface.OnSell;
                @Sell.performed -= m_Wrapper.m_ShopActionsCallbackInterface.OnSell;
                @Sell.canceled -= m_Wrapper.m_ShopActionsCallbackInterface.OnSell;
                @IncreaseAmount.started -= m_Wrapper.m_ShopActionsCallbackInterface.OnIncreaseAmount;
                @IncreaseAmount.performed -= m_Wrapper.m_ShopActionsCallbackInterface.OnIncreaseAmount;
                @IncreaseAmount.canceled -= m_Wrapper.m_ShopActionsCallbackInterface.OnIncreaseAmount;
                @DecreaseAmount.started -= m_Wrapper.m_ShopActionsCallbackInterface.OnDecreaseAmount;
                @DecreaseAmount.performed -= m_Wrapper.m_ShopActionsCallbackInterface.OnDecreaseAmount;
                @DecreaseAmount.canceled -= m_Wrapper.m_ShopActionsCallbackInterface.OnDecreaseAmount;
            }
            m_Wrapper.m_ShopActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Buy.started += instance.OnBuy;
                @Buy.performed += instance.OnBuy;
                @Buy.canceled += instance.OnBuy;
                @Sell.started += instance.OnSell;
                @Sell.performed += instance.OnSell;
                @Sell.canceled += instance.OnSell;
                @IncreaseAmount.started += instance.OnIncreaseAmount;
                @IncreaseAmount.performed += instance.OnIncreaseAmount;
                @IncreaseAmount.canceled += instance.OnIncreaseAmount;
                @DecreaseAmount.started += instance.OnDecreaseAmount;
                @DecreaseAmount.performed += instance.OnDecreaseAmount;
                @DecreaseAmount.canceled += instance.OnDecreaseAmount;
            }
        }
    }
    public ShopActions @Shop => new ShopActions(this);
    public interface IShopActions
    {
        void OnBuy(InputAction.CallbackContext context);
        void OnSell(InputAction.CallbackContext context);
        void OnIncreaseAmount(InputAction.CallbackContext context);
        void OnDecreaseAmount(InputAction.CallbackContext context);
    }
}
