using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleController : MonoBehaviour
{
    [SerializeField] private ExampleListView exampleListView;
    // Start is called before the first frame update
    void Start()
    {
        // Set up data
        ExampleData exampleData1 = new ExampleData()
        {
            Index = 0
        };
        ExampleData exampleData2 = new ExampleData()
        {
            Index = 1
        };
        
        List<ExampleData> data = new List<ExampleData>()
        {
            exampleData1, exampleData2
        };
        
        // Bind data
        for (int i = 0; i < data.Count; i++)
        {
            exampleListView.Add(data[i]);
        }
        
        // Subscribe
        exampleListView.OnDataSelected += ExampleListViewOnOnDataSelected;
    }

    private void ExampleListViewOnOnDataSelected(ExampleData data)
    {
        ExampleItemView view = exampleListView.GetItemViewByData(data);
        exampleListView.ChangeColor(view);
        exampleListView.UpdateData(data, view);
    }
    
}
