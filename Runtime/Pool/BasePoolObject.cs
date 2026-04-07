using System;
using UnityEngine;

namespace PhikozzLibrary
{
    public abstract class BasePoolObject : MonoBehaviour
    {
        public virtual void OnCreate()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnDespawn()
        {
            gameObject.SetActive(false);
        }

        public void OnDestroy()
        {
        }
    }
}