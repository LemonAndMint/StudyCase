using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Level
{
    [RequireComponent(typeof(GameManager))]
    public class ObjectPooler : MonoBehaviour
    {
        public GameManager gameManager;
        private ObjectPool<GameObject> _groundPool;
        public ObjectPool<GameObject> GroundPool{ get=> _groundPool; }

        private void Start() {
            
            _groundPool = new ObjectPool<GameObject>(_onCreate, _onTake, _onReturn, null, true, 10, 100);

        }

        private GameObject _onCreate(){

            GameObject groundPrefb = Instantiate(gameManager.Data.planePrefbs[Random.Range(0, gameManager.Data.planePrefbs.Count)], Vector3.zero, Quaternion.identity);

            return groundPrefb;

        }

        private void _onTake(GameObject groundPrefb){

            groundPrefb.gameObject.SetActive(true);

        }

        private void _onReturn(GameObject groundPrefb){

            groundPrefb.gameObject.SetActive(false);

        }

    }

}
