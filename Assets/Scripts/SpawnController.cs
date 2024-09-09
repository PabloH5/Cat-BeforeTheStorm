using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private PigeonSo _pigeonSo;

        [SerializeField] private GameObject[] leftSpawn;
        [SerializeField] private GameObject[] rightSpawn;

        [SerializeField] private Transform pigeonParent;

        public bool isReady = false;
        public float cdSpawn=1;

        public bool stopAll = false;

        
        void Start()
        {
            foreach (var pigeonList in _pigeonSo._pigeons)
            {
                pigeonList._prefab.GetComponent<PigeonMovement>().speed = pigeonList._speed;
            }
        }

        void Update()
        {
            if (!stopAll)
            {
                if (isReady)
                {
                    StartCoroutine(SpawnPigeon(cdSpawn));
                }
            }
            else
            {
                Debug.Log("STOP ALL");
            }
            
            
        }

        private IEnumerator SpawnPigeon(float time)
        {
            isReady = false;
            GetRandomPigeon(Random.Range(0,_pigeonSo._pigeons.Count));
            yield return new WaitForSeconds(time);
            isReady = true;
            
        }
        private void GetRandomPigeon(int num)
        {
            if (num <= _pigeonSo._pigeons.Count)
            {
                if (num % 2 == 0)
                {
                    Vector3 spawn= rightSpawn[GetRandomPosition(rightSpawn)].transform.position;
                    GameObject p = Instantiate(_pigeonSo._pigeons[num]._prefab, spawn, Quaternion.identity, pigeonParent);

                }
                else
                {
                    Vector3 spawn= leftSpawn[GetRandomPosition(leftSpawn)].transform.position;
                    GameObject p = Instantiate(_pigeonSo._pigeons[num]._prefab, spawn, Quaternion.identity, pigeonParent);

                }
            }
            else
            {
                Debug.LogError("The Scriptable Object don't have that amount");
            }
        }

        private int GetRandomPosition(GameObject[] pos)
        {
            return Random.Range(0, pos.Length);
        }
    }
}

