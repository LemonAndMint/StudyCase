using Data;
using Level;
using Player;
using UnityEngine;

namespace Game
{
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelDataSO _data;
        public LevelDataSO LevelData{ get => _data; }

        public PlayerBrain playerBrain;
        public GroundManager groundManager;


        private void Start() {

            playerBrain.CollisionManager.OnCriticalCollision += Lose;
            playerBrain.CollisionManager.OnCriticalCollision += groundManager.FlipStartedSwitch;

            groundManager.InitLevel();

        }

        public void Lose(){

            Time.timeScale = 0;

        }

        public void Restart(){

            Time.timeScale = 1;

        }

    }

}

