using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExtenListView.ItemLoader;

namespace ExtenListView
{
    public abstract class BaseListView<TItem> : MonoBehaviour where TItem : MonoBehaviour
    {
        [SerializeField] protected TItem itemPrefab;

        [SerializeField] protected Transform parent;

        [SerializeField] protected List<TItem> itemCollection = new List<TItem>();

        protected IItemLoader<TItem> itemLoader = new InstantiatedItemLoader<TItem>();

        public TItem ItemPrefab
        {
            get => itemPrefab;
            set => itemPrefab = value;
        }

        public Transform Parent
        {
            get => parent;
            set => parent = value;
        }

        public List<TItem> ItemCollection => itemCollection;

        public IItemLoader<TItem> ItemLoader
        {
            get => itemLoader;
            set => itemLoader = value;
        }

        public event Action<TItem> OnItemAdded;

        public event Action<TItem> OnItemRemoved;

        public virtual TItem Add()
        {
            TItem spawnItem = itemLoader.SpawnItem(itemPrefab);
            OnItemAdded?.Invoke(spawnItem);
            spawnItem.transform.SetParent(parent, false);

            itemCollection.Add(spawnItem);
            return spawnItem;
        }


        public virtual void RemoveAt(int index)
        {
            if (index >= itemCollection.Count)
            {
                return;
            }

            TItem item = itemCollection[index];
            OnItemRemoved?.Invoke(item);
            itemCollection.RemoveAt(index);
            itemLoader.DeSpawnItem(item);
        }

        public virtual void Remove(TItem instance)
        {
            int index = itemCollection.IndexOf(instance);
            RemoveAt(index);
        }
    }

    public class BaseListView<TItem, TData> : BaseListView<TItem>
        where TItem : BaseItemView<TData>
    {
        [SerializeField] protected Dictionary<TData, TItem> dataRecords = new Dictionary<TData, TItem>();

        public Dictionary<TData, TItem> DataRecords => dataRecords;

        public event Action<TData> OnDataAdded;

        public event Action<TData> OnDataRemoved;

        public override TItem Add()
        {
            throw new Exception($"Consider using the generic version of Add method");
        }

        public override void Remove(TItem instance)
        {
            throw new Exception($"Consider using the generic version of Remove method");
        }

        public virtual TItem Add(TData data)
        {
            TItem item = base.Add();
            dataRecords.Add(data, item);
            OnDataAdded?.Invoke(data);
            item.Bind(data);
            return item;
        }

        public virtual void Remove(TData data)
        {
            if (dataRecords.ContainsKey(data))
            {
                base.Remove(dataRecords[data]);
                dataRecords.Remove(data);
                OnDataRemoved?.Invoke(data);
            }
        }

        public override void RemoveAt(int index)
        {
            TData data = dataRecords.Keys.ElementAt(index);
            dataRecords.Remove(data);
            base.RemoveAt(index);
        }
    }
}
