using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtenListView
{
    public class SelectableListView<TItem, TData> : BaseListView<TItem>
        where TItem : SelectableItemView<TData>
    {
        [SerializeField] protected Dictionary<TData, TItem> dataRecords = new Dictionary<TData, TItem>();

        public Dictionary<TData, TItem> DataRecords => dataRecords;

        public event Action<TData> OnDataAdded;

        public event Action<TData> OnDataRemoved;

        protected virtual void OnSelectableItemRemoved(TItem item)
        {
            base.OnItemRemoved -= OnSelectableItemRemoved;
            item.OnItemSelected -= OnItemSelectedHandler;
        }

        protected virtual void OnItemSelectedHandler(TData data)
        {

        }

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
            item.OnItemSelected += OnItemSelectedHandler;
            dataRecords.Add(data, item);
            OnDataAdded?.Invoke(data);
            item.Bind(data);
            return item;
        }

        public virtual void Remove(TData data)
        {
            if (dataRecords.ContainsKey(data))
            {
                OnItemRemoved += OnSelectableItemRemoved;
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

