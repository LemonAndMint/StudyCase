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
        private bool _willJump;

        private void Start() {
            
            if(joystick == null){

                Debug.LogError("No joystick provided");

            }

        }

        private void Update() {

            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began){

                    _willJump = true;

                }

            }

        }

        void FixedUpdate()
        {
            _joystickCurrHori = joystick.Horizontal;


            if(_joystickCurrHori != 0){

                OnSideMovement?.Invoke(_joystickCurrHori);

            }

            //OnJump contains physic based methods and Input.GetTouch needs to be inside "Update". Otherwise Touch state doesnt read properly. 
            if(_willJump){

                OnJump?.Invoke();
                _willJump = false;

            }
            
        }
    }

}
