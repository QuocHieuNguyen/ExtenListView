using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtenListView.ItemLoader
{
    public class SimpleObjectPooling<T> : IItemLoader<T> where T : MonoBehaviour
    {
        private List<T> pool;
        private T prefab;
        private Transform parentTransform;

        public SimpleObjectPooling(T prefab, Transform parentTransform, int initialSize = 10)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            this.pool = new List<T>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        private T Get()
        {
            foreach (var obj in pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            // If no inactive object is found, create a new one and add it to the pool
            T newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        private void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T CreateNewObject()
        {
            T newObj = Object.Instantiate(prefab, parentTransform);
            newObj.gameObject.SetActive(false);
            pool.Add(newObj);
            return newObj;
        }

        public T SpawnItem(T prefab)
        {
            T newObj = Get();
            return newObj;
        }

        public T DeSpawnItem(T instance)
        {
            ReturnToPool(instance);
            return instance;
        }
    }
}
