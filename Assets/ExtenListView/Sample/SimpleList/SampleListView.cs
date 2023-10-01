using System.Collections;
using System.Collections.Generic;
using ExtenListView;
using UnityEngine;

public class SampleListView : BaseListView<MonoBehaviour>
{
    // Start is called before the first frame update
    void Start()
    {
        OnItemAdded += OnItemAddedHanlder;
        OnItemRemoved += OnItemRemovedHandler;
        for (int i = 0; i < 5; i++)
        {
            Add();
        }
        Debug.Log($"Item count {itemCollection.Count}");
        Remove(itemCollection[0]);
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
