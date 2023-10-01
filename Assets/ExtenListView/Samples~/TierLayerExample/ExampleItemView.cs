using System.Collections;
using System.Collections.Generic;
using ExtenListView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleItemView : SelectableItemView<ExampleData>
{
    [SerializeField] protected TextMeshProUGUI _labelText;

    [SerializeField] protected Button _button;

    public TextMeshProUGUI LabelText => _labelText;

    public Button Button => _button;

    protected virtual void Awake()
    {
        _button.onClick.AddListener(OnButtonClickedHandler);
    }

    public override void Bind(ExampleData data)
    {
        base.Bind(data);
        _labelText.text = data.Index.ToString();
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
