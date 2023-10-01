using System.Collections;
using System.Collections.Generic;
using ExtenListView;
using UnityEngine;
using UnityEngine.UI;

public class SampleDataView : BaseItemView<SampleData>
{
    [SerializeField] private Text text;
    public override void Bind(SampleData data)
    {
        base.Bind(data);
        text.text = data.index.ToString();
        Debug.Log($"data {data.index} has been bound");
    }
}
