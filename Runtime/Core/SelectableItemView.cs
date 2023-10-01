using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtenListView
{
    public class SelectableItemView<TData> :  BaseItemView<TData>
    {
        public event Action<TData> OnItemSelected;

        public virtual void InvokeOnItemSelected()
        {
            OnItemSelected?.Invoke(Data);
        }
    }
}

