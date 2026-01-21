# Skyline.DataMiner.SDM.Linq

## About

A LINQ provider for querying SDM (Standard Data Model) repositories using familiar C# LINQ syntax. This library translates LINQ expressions into DataMiner-compatible filter queries, enabling type-safe and intuitive data access.

## Installation

```bash
Install-Package Skyline.DataMiner.SDM.Linq
```

## Getting Started

Implement `IQueryableRepository<T>` to enable LINQ queries:

```csharp
using Skyline.DataMiner.SDM;
using Skyline.DataMiner.SDM.Linq;

public class MyRepository : IRepository<MyClass>, IQueryableRepository<MyClass>
{
    public IQueryable<MyClass> Query()
    {
        return new RepositoryQuery<MyClass>(
            new RepositoryQueryProvider<MyClass>(this));
    }
}
```

## Supported Operations

### Filtering (Where)

```csharp
// String operations: ==, !=, Contains, StartsWith, EndsWith
repository.Query().Where(x => x.Name.Contains("Alice"))

// Numeric comparisons: ==, !=, <, <=, >, >=
repository.Query().Where(x => x.Age > 20 && x.Score >= 85.5m)

// Boolean, DateTime, Enum, and nested properties
repository.Query().Where(x => x.IsActive && x.Status == Status.Active)
repository.Query().Where(x => x.SubObject.Name == "Test")

// Logical operators: &&, ||, !
repository.Query().Where(x => (x.Age > 20 && x.IsActive) || x.Status == Status.Admin)
```

### Sorting (OrderBy)

```csharp
repository.Query()
    .OrderBy(x => x.Status)
    .ThenByDescending(x => x.Age)
```

### Pagination (Take/Skip)

```csharp
repository.Query()
    .OrderBy(x => x.CreatedAt)
    .Skip(20)
    .Take(10)
```

### Retrieval Methods

```csharp
// Single records
repository.Query().First(x => x.Name == "Alice")
repository.Query().FirstOrDefault(x => x.Age > 100)
repository.Query().Single(x => x.Id == guid)

// Counting
repository.Query().Count(x => x.IsActive)
repository.Query().Any(x => x.IsActive)
```

## Supported Data Types

- **Numeric**: `int`, `long`, `float`, `double`, `decimal`, etc.
- **Text**: `string`
- **Date/Time**: `DateTime`, `TimeSpan`
- **Other**: `bool`, `Guid`, `enum`
- **Collections & nested objects**: `List<T>`, `ICollection<T>`, `SdmObject<T>`

## Limitations

- No complex projections (`Select` with new objects)
- No `GroupBy` or `Join` operations
- Use nested property access instead of joins

## Example

```csharp
public User[] GetActiveAdmins()
{
    return repository.Query()
        .Where(u => u.IsActive && u.Role == UserRole.Admin)
        .OrderBy(u => u.LastLogin)
        .ToArray();
}
```

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exists. In addition, you can leverage DataMiner Development Packages to build your own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.
