# Extensible List View for Unity UGUI

This package allows you to create a simple, reusable, extensible list view logic in Unity by using generic appoarch. 

This will help you saving time from adding duplicated logic of binding data to a Grid, Horizontal, Vertical List View, especially in some UI intensive application.

# Install

 Open Package Manager and add git URL `https://github.com/QuocHieuNguyen/ExtenListView.git#com.quochieu.extenlistview` for latest version.

# How To Use
## Basic Usage

You can view this example in the folder Simple List.

In your Unity scene, after creating a simple UGUI Scroll List, create a class deprived from BaseListView<TItem>. Specify the type of game object you want to spawn as a item, in this example, the type MonoBehavior is specified, you can freely add your own logic in this class.

Add that class to a game object, then drag and drop the Item Prefab and the Parent Transform (most of the time is the Content game object).

Call the Add method to spawn item in the Scroll List. 
## Simple Binding Data

You can view this example in the folder Simple Binding Data.

Usually, you want to display data in a Grid View, in this case, following these steps:
- Create a class for encapsulating the data, for example SampleData.cs
- Create a class deprived from BaseItemView<TData>, for example SampleDataView.cs the type parameter TData is the data class you just created. This is the class you want to add as Component to the Item Prefab that you will spawn from.
- Specify the way you will display the bound data in this class.
- Create a class deprived from BaseListView<TItem, TData>, for example SampleDataListView, the type parameter TItem is the Item Component class, and the type parameter TData is the data class.
- Add that class to a game object, then drag and drop the Item Prefab and the Parent Transform (most of the time is the Content game object).
- Call the Add method and provide the SampleData instance to spawn item in the Scroll List, those items are also bound with data.

## Example of Seperation of Data & UI Logic

You can view this example in the folder Tier Architecture Example.
## Event Handling

## How to change the way items are spawned ?

 <!-- ROADMAP -->
# Roadmap

- [x] Add Selectable List View
- [ ] Add Recycle List View
- [ ] Add Unit Test
