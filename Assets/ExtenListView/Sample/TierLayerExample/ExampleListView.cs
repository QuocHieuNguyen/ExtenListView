using System;
using System.Collections;
using System.Collections.Generic;
using ExtenListView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleListView : SelectableListView<ExampleItemView, ExampleData>
{
    public event Action<ExampleData> OnDataSelected;

    protected override void OnItemSelectedHandler(ExampleData data)
    {
        base.OnItemSelectedHandler(data);
        OnDataSelected?.Invoke(data);
    }

    public ExampleItemView GetItemViewByData(ExampleData data)
    {
        if (dataRecords.ContainsKey(data))
        {
            return dataRecords[data];
        }
        else return null;
    }

    public void ChangeColor(ExampleItemView itemView)
    {
        for (int i = 0; i < itemCollection.Count; i++)
        {
            itemCollection[i].Button.GetComponent<Image>().color = Color.white;
        }
        itemView.Button.GetComponent<Image>().color = Color.blue;
    }

    public void UpdateData(ExampleData data, ExampleItemView view)
    {
        data.Index += 1;
        view.Bind(data);
        view.LabelText.text = data.Index.ToString();
    }
    
}
