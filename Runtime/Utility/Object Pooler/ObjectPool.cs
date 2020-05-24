using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameLokal.Utility
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        #region Public Field
        
        public T objectToPool;
        public bool autoExpand = true;
        [HideIf("autoExpand")]
        public int maxInstances = 10;
        
        #endregion
        
        #region Private Field

        private List<T> _pools = new List<T>();
        
        #endregion

        #region MonoBehaviour Callbacks
        
        private void Start()
        {
            if (!autoExpand)
            {
                InitPool();
            }
        }
        
        #endregion
        
        #region Virtual Method
        
        protected virtual T CreateInstance()
        {
            var instance = Instantiate(objectToPool, transform);
            _pools.Add(instance);
            return instance;
        }

        protected virtual void OnBeforeRent(T instance)
        {
            if (!instance) return;
            
            instance.gameObject.SetActive(true);
        }

        protected virtual void OnBeforeReturn(T instance)
        {
            if (!instance) return;
            
            instance.gameObject.SetActive(false);
        }

        protected virtual void OnClear(T instance)
        {
            if (instance == null) return;
            
            Destroy(instance);
        }
        
        #endregion
        
        #region Private Method

        private void InitPool()
        {
            for (int i = 0; i < maxInstances; i++)
            {
                var instance = CreateInstance();
                OnBeforeRent(instance);
                Return(instance);
            }
        }
        
        #endregion

        #region Public Method

        public T Rent()
        {
            var instance = _pools.FirstOrDefault(pool => !pool.gameObject.activeInHierarchy);

            if (instance == null)
            {
                if (autoExpand)
                {
                    instance = CreateInstance();
                }
                else
                {
                    instance = _pools[0];
                    _pools.Remove(instance);
                    _pools.Add(instance);
                }
            }
            
            OnBeforeRent(instance);
            return instance;
        }

        public void Return(T instance)
        {
            if (instance == null) return;
            
            OnBeforeReturn(instance);
        }
        
        public void Clear(bool callOnBeforeRent = false)
        {
            if (_pools == null) return;
            foreach (var pool in _pools)
            {
                if (callOnBeforeRent)
                {
                    OnBeforeRent(pool);
                }

                OnClear(pool);
            }
        }
        
        #endregion
    }
}