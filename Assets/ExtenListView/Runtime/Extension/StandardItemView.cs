using System;
using System.Collections;
using System.Collections.Generic;
using ExtenListView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExtenListView.Extension
{
    public class StandardItemView<TData> : SelectableItemView<TData>
    {
        [SerializeField] protected Button _button;

        public Button Button => _button;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(OnButtonClickedHandler);
        }

        protected void OnButtonClickedHandler()
        {
            InvokeOnItemSelected();
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClickedHandler);
        }
    }
}

