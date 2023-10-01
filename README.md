# Extensible List View for Unity UGUI

This package allows you to create a simple, reusable, extensible list view logic in Unity by using generic appoarch. 

This will help you saving time from adding duplicated logic of binding data to a Grid, Horizontal, Vertical List View, especially in some UI intensive application.

# Install

 Open Package Manager and add git URL `https://github.com/QuocHieuNguyen/ExtenListView.git#com.quochieu.extenlistview` for latest version.

# How To Use
## Basic Usage

You can view this example in the folder Simple List.

In your Unity scene, after creating a simple UGUI Scroll List, create a class deprived from `BaseListView<TItem>`. Specify the type of game object you want to spawn as a item, in this example, the type `MonoBehavior` is specified, you can freely add your own logic in this class.
```csharp
public class SampleListView : BaseListView<MonoBehaviour>
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Add();
        }
    }

}
```
Add that class to a game object, then drag and drop the Item Prefab and the Parent Transform (most of the time is the Content game object).

Call the `Add` method to spawn item in the Scroll List. 
## Simple Binding Data

You can view this example in the folder Simple Binding Data.

Usually, you want to display data in a Grid View, in this case, following these steps:
- Create a class for encapsulating the data, for example `SampleData.cs`
```csharp
[System.Serializable]
public class SampleData
{
    public int index;
}
```
- Create a class deprived from `BaseItemView<TData>`, for example `SampleDataView.cs` the type parameter `TData` is the data class you just created. This is the class you want to add as Component to the Item Prefab that you will spawn from.
```csharp
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
```
- Specify the way you will display the bound data in this class.
- Create a class deprived from `BaseListView<TItem, TData>`, for example `SampleDataListView.cs`, the type parameter `TItem` is the Item Component class, and the type parameter `TData` is the data class.

```csharp
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
        Add(sampleData1);
        Add(sampleData2);
        Debug.Log($"Item count {itemCollection.Count}");
        Remove(sampleData1);
    }
}
```
- Add that class to a game object, then drag and drop the Item Prefab and the Parent Transform (most of the time is the Content game object).
- Call the `Add` method and provide the `SampleData` instance to spawn item in the Scroll List, those items are also bound with data.

## Example of Seperation of Data & UI Logic

You can view this example in the folder Tier Architecture Example.
## Event Handling

You can subscribe to the Add/Remove Item or their data
 ```csharp
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
    }

    private void OnItemRemovedHandler(MonoBehaviour obj)
    {
        Debug.Log($"object {obj.gameObject.name} has been removed");
    }

    private void OnItemAddedHanlder(MonoBehaviour obj)
    {
        Debug.Log($"object {obj.gameObject.name} has been added");
    }
}
```
```csharp
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
```
## How to change the way items are spawned ?

You can customize the way items are spawned by creating a class implementing the `IItemLoader.cs` interface.

```csharp
public interface IItemLoader<T> 
{
    public T SpawnItem(T prefab);

    public T DeSpawnItem(T instance);
}
```
 <!-- ROADMAP -->
# Roadmap

- [x] Add Selectable List View
- [ ] Add Recycle List View
- [ ] Add Unit Test
