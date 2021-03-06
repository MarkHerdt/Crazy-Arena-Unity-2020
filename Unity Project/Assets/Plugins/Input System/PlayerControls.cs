// GENERATED AUTOMATICALLY FROM 'Assets/Plugins/Input System/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""f840efd3-0628-420f-a381-521012eb72b8"",
            ""actions"": [
                {
                    ""name"": ""DPadLeftDown"",
                    ""type"": ""Button"",
                    ""id"": ""9ac74498-37b3-4bf2-ad97-be4da5c57af3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DPadLeftUp"",
                    ""type"": ""Button"",
                    ""id"": ""c3f39295-7b2e-4a2e-bcbe-fc0512197186"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DPadRightDown"",
                    ""type"": ""Button"",
                    ""id"": ""822c120e-aa06-476b-8b24-3f46fb2e2cc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DPadRightUp"",
                    ""type"": ""Button"",
                    ""id"": ""3f53afeb-4380-4297-9c1d-35fbba1c42fe"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DPadUp"",
                    ""type"": ""Button"",
                    ""id"": ""f4f8aa6c-0032-427a-9ec8-4133738c20c0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DPadDown"",
                    ""type"": ""Button"",
                    ""id"": ""0b65d65a-8435-46b5-82e7-5eb56456e92b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""7ec7e078-2224-44e0-a2d1-93ba61a3199e"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""7d8715b2-06ea-4d37-b596-cbb211d33ccc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""e9853b81-fe9f-4f5c-94ad-f033d6e4cb48"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Ultimate"",
                    ""type"": ""Button"",
                    ""id"": ""47938fc7-2cee-405e-bf8e-da35e455456c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d824b3e2-0838-4ea8-af03-6bf8c89b78a3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""ceb674ba-9013-4104-b11b-a5eec32f677e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a53a645f-c23b-4d2d-bc0b-690c1e64a4b1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e3eef951-cfdf-48a9-8399-b6000695b9dd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ae843dbf-1441-409d-963a-1a373efab2bf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b307b4d8-f42e-4c9a-bbf6-292080901c4a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3193b812-484f-4b65-8f29-37171463215c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""959ead32-7704-4899-9bcc-ffad04e59f3f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df734537-9126-4573-8640-4c5e9fc44e04"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadRightDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8020d77-cf79-481c-9240-fdb9826529f8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadRightDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10791e05-8dec-4b7b-8860-c6aafc8f8e1f"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadLeftDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9c231f6-4341-4a3b-b7cf-2c9f012fce7c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadLeftDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cecbe4b2-bcc4-40a8-b424-9ebb1d4848b8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadLeftUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68d83b7d-52c4-44a0-a22f-11cca6ced46f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadRightUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27fb9dcf-a3c0-40d9-b2fd-75fd72966585"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aeddf408-113b-4c2b-a6b5-7ad3e69249dc"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60c0bfba-e343-4aa4-b019-182b64e82829"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5b5ee47-dbce-4679-a399-ccc6421c75ad"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc9b9010-c598-4707-9bd0-e48bfcc37de2"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b30d90f-090f-4ce5-a8c3-db8c2eb61216"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd2677a0-1e75-4972-8609-58016f819d40"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adb674ef-d0c0-48ef-83a6-ab1645f84889"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_DPadLeftDown = m_Controls.FindAction("DPadLeftDown", throwIfNotFound: true);
        m_Controls_DPadLeftUp = m_Controls.FindAction("DPadLeftUp", throwIfNotFound: true);
        m_Controls_DPadRightDown = m_Controls.FindAction("DPadRightDown", throwIfNotFound: true);
        m_Controls_DPadRightUp = m_Controls.FindAction("DPadRightUp", throwIfNotFound: true);
        m_Controls_DPadUp = m_Controls.FindAction("DPadUp", throwIfNotFound: true);
        m_Controls_DPadDown = m_Controls.FindAction("DPadDown", throwIfNotFound: true);
        m_Controls_Movement = m_Controls.FindAction("Movement", throwIfNotFound: true);
        m_Controls_Primary = m_Controls.FindAction("Primary", throwIfNotFound: true);
        m_Controls_Secondary = m_Controls.FindAction("Secondary", throwIfNotFound: true);
        m_Controls_Ultimate = m_Controls.FindAction("Ultimate", throwIfNotFound: true);
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

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_DPadLeftDown;
    private readonly InputAction m_Controls_DPadLeftUp;
    private readonly InputAction m_Controls_DPadRightDown;
    private readonly InputAction m_Controls_DPadRightUp;
    private readonly InputAction m_Controls_DPadUp;
    private readonly InputAction m_Controls_DPadDown;
    private readonly InputAction m_Controls_Movement;
    private readonly InputAction m_Controls_Primary;
    private readonly InputAction m_Controls_Secondary;
    private readonly InputAction m_Controls_Ultimate;
    public struct ControlsActions
    {
        private @PlayerControls m_Wrapper;
        public ControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DPadLeftDown => m_Wrapper.m_Controls_DPadLeftDown;
        public InputAction @DPadLeftUp => m_Wrapper.m_Controls_DPadLeftUp;
        public InputAction @DPadRightDown => m_Wrapper.m_Controls_DPadRightDown;
        public InputAction @DPadRightUp => m_Wrapper.m_Controls_DPadRightUp;
        public InputAction @DPadUp => m_Wrapper.m_Controls_DPadUp;
        public InputAction @DPadDown => m_Wrapper.m_Controls_DPadDown;
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Primary => m_Wrapper.m_Controls_Primary;
        public InputAction @Secondary => m_Wrapper.m_Controls_Secondary;
        public InputAction @Ultimate => m_Wrapper.m_Controls_Ultimate;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @DPadLeftDown.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadLeftDown;
                @DPadLeftDown.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadLeftDown;
                @DPadLeftDown.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadLeftDown;
                @DPadLeftUp.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadLeftUp;
                @DPadLeftUp.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadLeftUp;
                @DPadLeftUp.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadLeftUp;
                @DPadRightDown.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadRightDown;
                @DPadRightDown.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadRightDown;
                @DPadRightDown.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadRightDown;
                @DPadRightUp.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadRightUp;
                @DPadRightUp.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadRightUp;
                @DPadRightUp.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadRightUp;
                @DPadUp.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadUp;
                @DPadUp.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadUp;
                @DPadUp.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadUp;
                @DPadDown.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadDown;
                @DPadDown.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadDown;
                @DPadDown.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDPadDown;
                @Movement.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Primary.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnPrimary;
                @Secondary.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSecondary;
                @Secondary.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSecondary;
                @Secondary.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSecondary;
                @Ultimate.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnUltimate;
                @Ultimate.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnUltimate;
                @Ultimate.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnUltimate;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DPadLeftDown.started += instance.OnDPadLeftDown;
                @DPadLeftDown.performed += instance.OnDPadLeftDown;
                @DPadLeftDown.canceled += instance.OnDPadLeftDown;
                @DPadLeftUp.started += instance.OnDPadLeftUp;
                @DPadLeftUp.performed += instance.OnDPadLeftUp;
                @DPadLeftUp.canceled += instance.OnDPadLeftUp;
                @DPadRightDown.started += instance.OnDPadRightDown;
                @DPadRightDown.performed += instance.OnDPadRightDown;
                @DPadRightDown.canceled += instance.OnDPadRightDown;
                @DPadRightUp.started += instance.OnDPadRightUp;
                @DPadRightUp.performed += instance.OnDPadRightUp;
                @DPadRightUp.canceled += instance.OnDPadRightUp;
                @DPadUp.started += instance.OnDPadUp;
                @DPadUp.performed += instance.OnDPadUp;
                @DPadUp.canceled += instance.OnDPadUp;
                @DPadDown.started += instance.OnDPadDown;
                @DPadDown.performed += instance.OnDPadDown;
                @DPadDown.canceled += instance.OnDPadDown;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
                @Secondary.started += instance.OnSecondary;
                @Secondary.performed += instance.OnSecondary;
                @Secondary.canceled += instance.OnSecondary;
                @Ultimate.started += instance.OnUltimate;
                @Ultimate.performed += instance.OnUltimate;
                @Ultimate.canceled += instance.OnUltimate;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);
    public interface IControlsActions
    {
        void OnDPadLeftDown(InputAction.CallbackContext context);
        void OnDPadLeftUp(InputAction.CallbackContext context);
        void OnDPadRightDown(InputAction.CallbackContext context);
        void OnDPadRightUp(InputAction.CallbackContext context);
        void OnDPadUp(InputAction.CallbackContext context);
        void OnDPadDown(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnUltimate(InputAction.CallbackContext context);
    }
}
