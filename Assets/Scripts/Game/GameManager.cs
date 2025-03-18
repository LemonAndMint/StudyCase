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
        public UIManager uiManager;

        public Vector3 playerStartPoint;

        private Transform _playerTrans;

        private void Start() {

            _playerTrans = playerBrain.transform;

            playerBrain.CollisionManager.OnCriticalCollision += Lose;
            playerBrain.CollisionManager.OnCriticalCollision += groundManager.FlipStartedSwitch;

            Time.timeScale = 0;
            groundManager.InitLevel();

            uiManager.ShowStart();

        }

        private void Update() {

            if(_playerTrans.position.y < -10f){

                Lose();

            }

        }

        public void Lose(){

            Time.timeScale = 0;
            uiManager.ShowLose();

        }

        public void RestartGame(){

            uiManager.HideLose();

            playerBrain.transform.position = playerStartPoint;
            groundManager.ClearLevel();

            groundManager.InitLevel();

            StartGame();

        }

        public void StartGame(){

            Time.timeScale = 1;
            uiManager.HideStart();

        }

    }

}

