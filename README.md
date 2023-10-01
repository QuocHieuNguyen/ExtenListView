# Extensible List View for Unity UGUI

This package allows you to create a simple, reusable, extensible list view logic in Unity by using generic appoarch. 

This will help you saving time from adding duplicated logic of binding data to a Grid, Horizontal, Vertical List View, especially in some UI intensive application.

# Install

 Open Package Manager and add git URL `https://github.com/QuocHieuNguyen/ExtenListView.git#com.quochieu.extenlistview` for latest version.

 Add Sample package for sample code.

## Basic Usage

You can view this example in the folder Simple List.

In your Unity scene, after creating a simple UGUI Scroll List, create a class deprived of `BaseListView<TItem>`. Specify the type of game object you want to spawn as a item, in this example, the type `MonoBehavior` is specified, you can freely add your own logic in this class.
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
- Create a class deprived of `BaseItemView<TData>`, for example `SampleDataView.cs` the type parameter `TData` is the data class you just created. This is the class you want to add as Component to the Item Prefab that you will spawn from.
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
- Create a class deprived of `BaseListView<TItem, TData>`, for example `SampleDataListView.cs`, the type parameter `TItem` is the Item Component class, and the type parameter `TData` is the data class.

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

Seperation of Data Source, Logic Tier & UI Tier  is a way to build extensible, reusable and maintainable system.

In the example, we will create a scrollable list displaying a list of button, when a button is clicked, the data is that button is updated and the its color change.

- First, create a data class
```csharp
public class ExampleData
{
    public int Index;
}
```
- Then, create a class for binding the data & logic, exposing the event handling the click event.

```csharp
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
```
- After that, create a class deprived of `SelectableListView.cs`, add method `GetItemViewByData`, `ChangeColor` and `UpdateData`.

```csharp
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
        ...
    }

    public void ChangeColor(ExampleItemView itemView)
    {
        ...
    }

    public void UpdateData(ExampleData data, ExampleItemView view)
    {
        ...
    }
    
}
```
- The first type parameter `ExampleItemView` is the View part of the item, and the second type parameter `ExampleData` is the Data part.

- In the `ExampleController.cs` class, create fake data then implement the view logic.

```csharp
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
```
- Add the `ExampleController.cs` and `ExampleListView.cs` class to a game object, and the `ExampleItemView.cs` class to an Item Prefab.
- Then drag and drop the Item Prefab, the `ExampleListView` and the Parent Transform (most of the time is the Content game object) to the ExampleController component.

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
The current implementation of `ItemLoader` use the Unity Engine Instantiatiation.
```csharp
public abstract class BaseListView<TItem> : MonoBehaviour where TItem : MonoBehaviour
{
    ...
    protected IItemLoader<TItem> itemLoader = new InstantiatedItemLoader<TItem>();
    ...
}
```
You can customize the way items are spawned by creating a class implementing the `IItemLoader.cs` interface.

```csharp
public interface IItemLoader<T> 
{
    public T SpawnItem(T prefab);

    public T DeSpawnItem(T instance);
}
```
For example, the class `SimpleObjectPooling.cs` implementing that interface.

Set the `ItemLoader` property to the updated implemetation.

```csharp
exampleListView.ItemLoader = new SimpleObjectPooling();
```
 <!-- ROADMAP -->
# Roadmap

- [x] Add Selectable List View
- [ ] Add Recycle List View
- [ ] Add Unit Test
