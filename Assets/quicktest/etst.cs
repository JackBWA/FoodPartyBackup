// GENERATED AUTOMATICALLY FROM 'Assets/quicktest/etst.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Etst : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Etst()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""etst"",
    ""maps"": [
        {
            ""name"": ""test"",
            ""id"": ""ce093a39-892c-4b58-a34f-a231c6ad4f33"",
            ""actions"": [
                {
                    ""name"": ""a"",
                    ""type"": ""Button"",
                    ""id"": ""e165f4d5-ca90-41b6-abd9-9e2bde753321"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8488e72f-15d0-4d5d-8e05-b27b82a9ba98"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""a"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // test
        m_test = asset.FindActionMap("test", throwIfNotFound: true);
        m_test_a = m_test.FindAction("a", throwIfNotFound: true);
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

    // test
    private readonly InputActionMap m_test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_test_a;
    public struct TestActions
    {
        private @Etst m_Wrapper;
        public TestActions(@Etst wrapper) { m_Wrapper = wrapper; }
        public InputAction @a => m_Wrapper.m_test_a;
        public InputActionMap Get() { return m_Wrapper.m_test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @a.started -= m_Wrapper.m_TestActionsCallbackInterface.OnA;
                @a.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnA;
                @a.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnA;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @a.started += instance.OnA;
                @a.performed += instance.OnA;
                @a.canceled += instance.OnA;
            }
        }
    }
    public TestActions @test => new TestActions(this);
    public interface ITestActions
    {
        void OnA(InputAction.CallbackContext context);
    }
}
