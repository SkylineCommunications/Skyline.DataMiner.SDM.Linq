# Skyline.DataMiner.SDM.Linq

LINQ support for SDM (Standard Data Model) repositories in DataMiner.

This package lets you query `IQueryableRepository<T>` implementations using familiar LINQ syntax. Expressions are translated into DataMiner filter and sorting queries, so you can write type-safe, readable query logic in C#.

## Installation

```powershell
Install-Package Skyline.DataMiner.SDM.Linq
```

## Quick start

```csharp
using System.Linq;
using Skyline.DataMiner.SDM;
using Skyline.DataMiner.SDM.Linq;

public class MyRepository : IQueryableRepository<MyObject>
{
	public IQueryable<MyObject> Query()
	{
		return new RepositoryQuery<MyObject>(
			new RepositoryQueryProvider<MyObject>(this));
	}
}
```

```csharp
var active = repository.Query()
	.Where(x => x.IsActive)
	.OrderBy(x => x.Name)
	.Take(10)
	.ToArray();
```

## Supported LINQ operations

- `Where` (including `&&`, `||`, `!`)
- `OrderBy`, `OrderByDescending`, `ThenBy`, `ThenByDescending`
- `Take`, `Skip`
- `First`, `FirstOrDefault`, `Single`, `SingleOrDefault`
- `Count`, `Any`

## Notes

- Best suited for filter, sort, and paging scenarios over SDM repositories.
- Complex projections and join/grouping patterns are not the primary target.
