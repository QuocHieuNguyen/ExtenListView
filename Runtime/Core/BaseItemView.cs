using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtenListView
{
    public abstract class BaseItemView<TData> : MonoBehaviour
    {
        private TData data;

        public TData Data => data;

        public event Action<TData> OnDataBound; 
        public virtual void Bind(TData data)
        {
            this.data = data;
            OnDataBound?.Invoke(data);
        }
    }
}

