using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtenListView.ItemLoader
{
    public class InstantiatedItemLoader<T> : IItemLoader<T>  where T : MonoBehaviour
    {
        public T SpawnItem(T prefab)
        {
            T item = UnityEngine.Object.Instantiate(prefab);
            return item;
        }

        public T DeSpawnItem(T instance)
        {
            UnityEngine.Object.Destroy(instance.gameObject);
            return null;
        }
    }
}

