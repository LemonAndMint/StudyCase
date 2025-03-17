using System;
using System.Collections;
using Data;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerCollisionManager))]
    public class PlayerBrain : MonoBehaviour
    {
        private PlayerInput _input;
        //public PlayerInput Input;
        private PlayerMovement _movement;
        //public PlayerMovement Movement;
        private PlayerCollisionManager _collisionManager;
        public PlayerCollisionManager CollisionManager{ get => _collisionManager; }

        [SerializeField]
        private PlayerDataSO _data;
        public PlayerDataSO Data{ get => _data; }

        private bool _isJumped = false;

        private void Start() {
            
            if(_input == null && TryGetComponent( out PlayerInput pi ))
                _input = pi;

            if(_movement == null && TryGetComponent( out PlayerMovement pm ))
                _movement = pm;

            if(_collisionManager == null && TryGetComponent( out PlayerCollisionManager cm ))
                _collisionManager = cm;

            if(_data == null)
                new ArgumentNullException("Data is not implemented");

            _input.OnJump += Jump;
            _input.OnSideMovement += _movement.MoveSide;

        }

        public void MoveSide(){

            new NotImplementedException("Dont need rn, use it from playerMovement");

        }

        public void Jump(){

            if(!_isJumped && _collisionManager.Contacted){

                StartCoroutine(_jumpCooldown());

            }

        }

        private IEnumerator _jumpCooldown(){

            _movement.Jump();
            _isJumped = true;

            yield return new WaitForSeconds(_data.JumpCooldown);

            _isJumped = false;

        }

    }

}

