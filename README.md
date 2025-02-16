# Azure Table Storage Service Wrapper
Azure Table Storage Service Wrapper is a .Net Standard 2.0 library that provides CRUD operations for Azure Table Storage (NoSQL).

<br/>

## :rocket: Getting Started

Add a reference to AzureTableStorageService.dll to your project.

<br/>

## :hammer: Usage

### Define an entity

Define an entity that inherits from TableEntityBase and add your custom fields.

```c#
public class ToDos : AzureTableStorageService.Models.TableEntityBase
{
  public ToDos() : base()
  {
    PartitionKey = nameof(ToDos);
  }

  public DateTime? DueDate { get; set; }
  public string? Title { get; set; }
}
```

### Initialize an instance of AzureTableStorageService for the entity

```c#
AzureTableStorageService<ToDos> azureTableStorageService = 
  new AzureTableStorageService<ToDos>(connectionString: "<Azure Storage Account Connection String>",
    tableName: nameof(ToDos));
```

### Add a new entity

```c#

ToDos myToDo = new ToDos {
  DueDate = DateTime.Now.AddDays(3),
  Title = "Hello world!"
}

await tableStorageService.UpsertEntityAsync(entity: myToDo);
```

### Get entities

The filter parameter is optional.  Omitting it will return all entities.

```c#
IEnumerable<ToDos> myToDoList = await tableStorageService.GetEntities(filter: "Title eq 'Hello world!'");
```

### Get a single entity

```c#
ToDos myToDo = await tableStorageService.GetEntityAsync(
  partitionKey: nameof(ToDos),
  rowKey: "EFAD72D9-8BC8-42B0-8A95-5B8AEF616F94"
);
```

### Update an entity

```c#
myToDo.DueDate = DateTime.Now.AddDays(7);

await tableStorageService.UpsertEntityAsync(entity: myToDo);
```

### Delete an entity

```c#
await tableStorageService.DeleteEntityAsync(
  partitionKey: nameof(ToDos),
  rowKey: "EFAD72D9-8BC8-42B0-8A95-5B8AEF616F94"
);
```
