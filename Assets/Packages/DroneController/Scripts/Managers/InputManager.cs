using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneController
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        [Header("General Settings")]
        [SerializeField] private bool useVRInput = false;
        [SerializeField] private float ScaleFactor = 1.0f;
        [Header("Normal Input Actions")]
        [SerializeField] private InputActionAsset _inputActionAsset = default;
        [SerializeField] private InputActionReference _inputPitch = default;
        [SerializeField] private InputActionReference _inputRoll = default;
        [SerializeField] private InputActionReference _inputYaw = default;
        [SerializeField] private InputActionReference _inputThrottle = default;

        [Header("VR Input Actions")]
        [SerializeField] private InputActionAsset _inputActionAsset_vr = default;
        [SerializeField] private InputActionReference _inputLeftThumb = default;
        [SerializeField] private InputActionReference _inputRightThumb = default;



        [SerializeField] private float _pitchInput = default;
        [SerializeField] private float _rollInput = default;
        [SerializeField] private float _yawInput = default;
        [SerializeField] private float _throttleInput = default;

        public float PitchInput { get { return _pitchInput; } }
        public float RollInput { get { return _rollInput; } }
        public float YawInput { get { return _yawInput; } }
        public float ThrottleInput { get { return _throttleInput; } }

        private void Awake()
        {
            if (InputManager.Instance == null)
            {
                Instance = this;
            }
            else if (InputManager.Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if (useVRInput == true)
            {
                EnableVRInput();
            }
            else
            {
                EnableNormalInput();
            }
        }

        private void OnDisable()
        {
            if (useVRInput)
            {
                DisableVRInput();
            }
            else
            {
                DisableNormalInput();
            }
        }

        public void ToggleInputMode(bool vrEnabled)
        {
            useVRInput = vrEnabled;

            // Re-register the correct input actions
            OnDisable();
            OnEnable();
        }

        private void EnableNormalInput()
        {
            _inputActionAsset.Enable();

            _inputPitch.action.canceled += OnPitchInputChanged;
            _inputPitch.action.performed += OnPitchInputChanged;
            _inputPitch.action.started += OnPitchInputChanged;

            _inputRoll.action.canceled += OnRollInputChanged;
            _inputRoll.action.performed += OnRollInputChanged;
            _inputRoll.action.started += OnRollInputChanged;

            _inputYaw.action.canceled += OnYawInputChanged;
            _inputYaw.action.performed += OnYawInputChanged;
            _inputYaw.action.started += OnYawInputChanged;

            _inputThrottle.action.canceled += OnThrottleInputChanged;
            _inputThrottle.action.performed += OnThrottleInputChanged;
            _inputThrottle.action.started += OnThrottleInputChanged;
        }

        private void DisableNormalInput()
        {
            _inputPitch.action.canceled -= OnPitchInputChanged;
            _inputPitch.action.performed -= OnPitchInputChanged;
            _inputPitch.action.started -= OnPitchInputChanged;

            _inputRoll.action.canceled -= OnRollInputChanged;
            _inputRoll.action.performed -= OnRollInputChanged;
            _inputRoll.action.started -= OnRollInputChanged;

            _inputYaw.action.canceled -= OnYawInputChanged;
            _inputYaw.action.performed -= OnYawInputChanged;
            _inputYaw.action.started -= OnYawInputChanged;

            _inputThrottle.action.canceled -= OnThrottleInputChanged;
            _inputThrottle.action.performed -= OnThrottleInputChanged;
            _inputThrottle.action.started -= OnThrottleInputChanged;

            _inputActionAsset.Disable();
        }

        private void EnableVRInput()
        {
            _inputActionAsset_vr.Enable();

            //_inputLeftThumb.action.performed += OnPitchInputChanged;
            //_inputLeftThumb.action.performed += OnRollInputChanged;
            //_inputRightThumb.action.performed += OnYawInputChanged;
            //_inputRightThumb.action.performed += OnThrottleInputChanged;

            _inputLeftThumb.action.canceled += OnPitchInputChanged;
            _inputLeftThumb.action.performed += OnPitchInputChanged;
            _inputLeftThumb.action.started += OnPitchInputChanged;

            _inputLeftThumb.action.canceled += OnRollInputChanged;
            _inputLeftThumb.action.performed += OnRollInputChanged;
            _inputLeftThumb.action.started += OnRollInputChanged;

            _inputRightThumb.action.canceled += OnYawInputChanged;
            _inputRightThumb.action.performed += OnYawInputChanged;
            _inputRightThumb.action.started += OnYawInputChanged;

            _inputRightThumb.action.canceled += OnThrottleInputChanged;
            _inputRightThumb.action.performed += OnThrottleInputChanged;
            _inputRightThumb.action.started += OnThrottleInputChanged;
        }

        private void DisableVRInput()
        {
            //_inputLeftThumb.action.performed -= OnPitchInputChanged;
            //_inputLeftThumb.action.performed -= OnRollInputChanged;
            //_inputRightThumb.action.performed -= OnYawInputChanged;
            //_inputRightThumb.action.performed -= OnThrottleInputChanged;

            _inputLeftThumb.action.canceled -= OnPitchInputChanged;
            _inputLeftThumb.action.performed -= OnPitchInputChanged;
            _inputLeftThumb.action.started -= OnPitchInputChanged;

            _inputLeftThumb.action.canceled -= OnRollInputChanged;
            _inputLeftThumb.action.performed -= OnRollInputChanged;
            _inputLeftThumb.action.started -= OnRollInputChanged;

            _inputRightThumb.action.canceled -= OnYawInputChanged;
            _inputRightThumb.action.performed -= OnYawInputChanged;
            _inputRightThumb.action.started -= OnYawInputChanged;

            _inputRightThumb.action.canceled -= OnThrottleInputChanged;
            _inputRightThumb.action.performed -= OnThrottleInputChanged;
            _inputRightThumb.action.started -= OnThrottleInputChanged;

            _inputActionAsset_vr.Disable();
        }

        private void OnPitchInputChanged(InputAction.CallbackContext eventData)
        {
            if (useVRInput == false)
            {
                _pitchInput = eventData.ReadValue<float>()/ScaleFactor;
            }
            else
            {
                Vector2 PitchInputVr = eventData.ReadValue<Vector2>()/ScaleFactor;
                _pitchInput = PitchInputVr.y;
            }
            
        }

        private void OnRollInputChanged(InputAction.CallbackContext eventData)
        {
            if (useVRInput == false)
            {
                _rollInput = eventData.ReadValue<float>() / ScaleFactor;
            }
            else
            {
                Vector2 RollInputVr = eventData.ReadValue<Vector2>() / ScaleFactor;
                _rollInput = RollInputVr.x;
            }
            
        }

        private void OnYawInputChanged(InputAction.CallbackContext eventData)
        {
            if (useVRInput == false)
            {
                _yawInput = eventData.ReadValue<float>() / ScaleFactor;
            }
            else
            {
                Vector2 YawInputVr = eventData.ReadValue<Vector2>() / ScaleFactor;
                _yawInput = YawInputVr.x;
            }
        }

        private void OnThrottleInputChanged(InputAction.CallbackContext eventData)
        {
            if (useVRInput == false)
            {
                _throttleInput = eventData.ReadValue<float>() / ScaleFactor;
            }
            else
            {
                Vector2 ThrottleInputVr = eventData.ReadValue<Vector2>() / ScaleFactor;
                _throttleInput = ThrottleInputVr.y;
            }
        }

        public bool IsInputIdle()
        {
            return Mathf.Approximately(_pitchInput, 0f) &&
                   Mathf.Approximately(_rollInput, 0f) &&
                   Mathf.Approximately(_throttleInput, 0f);
        }
    }
}

