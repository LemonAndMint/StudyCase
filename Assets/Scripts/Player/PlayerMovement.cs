using UnityEngine;

namespace Player
{
    
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(PlayerBrain))]
    public class PlayerMovement : MonoBehaviour
    {
        private const float _FORCEMULTI = 10;

        public Rigidbody playerrb;
        public Transform playerTrans;
        public PlayerBrain playerBrain;

        private void Start() {
            
            if(playerrb == null && TryGetComponent( out Rigidbody pr ))
                playerrb = pr;

            if(playerTrans == null && TryGetComponent( out Transform pt ))
                playerTrans = pt;

            if(playerBrain == null && TryGetComponent( out PlayerBrain pb ))
                playerBrain = pb;

        }

        public void MoveSide(float positionDiff){

            //Dont want the player to lay down and start rocketing itself to different directions.
            playerrb.MovePosition(playerrb.position + Vector3.right * positionDiff * playerBrain.Data.SideMoveSpeed * Time.deltaTime);

        }
        public void Jump(){

            //Dont want the player to lay down and start rocketing itself to sides.
            playerrb.AddForce(Vector3.up * playerBrain.Data.JumpStrength * _FORCEMULTI * Time.deltaTime, ForceMode.Impulse);

        }
    }

}

