using System;
using UnityEngine;

namespace Player
{
    
    public class PlayerInput : MonoBehaviour
    {
        public delegate void SideInputEventHandler(float diffValue);
        public event SideInputEventHandler OnSideMovement;

        public delegate void JumpInputEventHandler();
        public event JumpInputEventHandler OnJump;

        public DynamicJoystick joystick;
        private float _joystickCurrHori;

        private void Start() {
            
            if(joystick == null){

                new ArgumentNullException("no joystick error");

            }

        }

        private void Update() {

            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);
                //Jump action
                if(touch.phase == TouchPhase.Began){

                    OnJump?.Invoke();

                }

            }

        }

        void FixedUpdate()
        {
            _joystickCurrHori = joystick.Horizontal;


            if(_joystickCurrHori != 0){

                OnSideMovement?.Invoke(_joystickCurrHori);

            }
            
        }
    }

}
