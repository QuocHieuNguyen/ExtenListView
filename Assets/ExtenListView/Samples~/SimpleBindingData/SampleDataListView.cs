using System.Collections;
using System.Collections.Generic;
using ExtenListView;
using UnityEngine;

public class SampleDataListView : BaseListView<SampleDataView, SampleData>
{
    // Start is called before the first frame update
    void Start()
    {
        SampleData sampleData1 = new SampleData()
        {
            index = 1
        };
        SampleData sampleData2 = new SampleData()
        {
            index = 2
        };
        OnItemAdded += OnItemAddedHanlder;
        OnItemRemoved += OnItemRemovedHandler;
        Add(sampleData1);
        Add(sampleData2);
        Debug.Log($"Item count {itemCollection.Count}");
        Remove(sampleData1);
//        Remove(itemCollection[0]);
        RemoveAt(0);
    }

    private void OnItemRemovedHandler(MonoBehaviour obj)
    {
        Debug.Log($"object {obj.gameObject.name} has been removed");
    }

    private void OnItemAddedHanlder(MonoBehaviour obj)
    {
        Debug.Log($"object {obj.gameObject.name} has been added");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
