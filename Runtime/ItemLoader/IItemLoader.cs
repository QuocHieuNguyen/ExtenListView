using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtenListView.ItemLoader
{
    public interface IItemLoader<T> 
    {
        public T SpawnItem(T prefab);

        public T DeSpawnItem(T instance);
    }
}

