using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Level
{
    public class ObjectPooler : MonoBehaviour
    {
        private List<GameObject> _prefbsToIns;
        private ObjectPool<GameObject> _groundPool;
        public ObjectPool<GameObject> GroundPool{ get=> _groundPool; }

        public void Init(List<GameObject> PrefbsToIns) {
            
            _prefbsToIns = PrefbsToIns;
            _groundPool = new ObjectPool<GameObject>(_onCreate, _onTake, _onReturn, null, true, 10, 100);
            
        }

        public GameObject GetObject(){

            return _groundPool.Get();

        }

        public void ReleaseObject(GameObject objectToRelease){

            _groundPool.Release(objectToRelease);

        }

        private GameObject _onCreate(){

            GameObject groundPrefb = Instantiate(_prefbsToIns[Random.Range(0, _prefbsToIns.Count)], Vector3.zero, Quaternion.identity);

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
