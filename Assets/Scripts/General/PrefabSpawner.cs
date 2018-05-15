using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Prefabs
{
    public class PrefabSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private Transform _spawnParent;

        private GameObject _spawnInstance;

        public void Spawn()
        {
            if (_spawnParent == null)
            {
                _spawnParent = this.transform;
            }

            _spawnInstance = GameObject.Instantiate(_prefab, _spawnParent);
        }

        public void Despawn()
        {
            if (_spawnInstance != null)
            {
                Destroy(_spawnInstance);
            }
        }
    }
}