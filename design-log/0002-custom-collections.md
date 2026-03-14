# Design Log #0002: Replacing Built-in Collections with Custom Structures

## Background
The current project uses `System.Collections.Generic.List<T>` for managing books within an order and for storing processed orders. In a Data Structures and Algorithms (DSA) coursework context, relying on built-in collections often defeats the purpose of the assignment.

## Problem
1. `Order.cs` uses `List<Book>`.
2. `Program.cs` uses `List<Order>` for `processedOrders`.
3. `AlgorithmHelper.cs` methods accept `List<T>`, making them dependent on built-in types.

## Questions and Answers
**Q: Which custom structure should replace `List<T>`?**
A: A generic `MyArrayList<T>` (dynamic array) is the best fit because it provides $O(1)$ indexing, which is required for the current QuickSort and Binary Search implementations.

**Q: Should we use a Linked List instead?**
A: While possible, a Linked List would make QuickSort $O(n^2)$ due to $O(n)$ element access. A dynamic array maintains $O(n \log n)$ performance for QuickSort.

## Design
### New Data Structure: `MyArrayList<T>`
```csharp
public class MyArrayList<T> {
    private T[] _items;
    private int _count;
    public void Add(T item) { /* Resize if needed, then add */ }
    public T this[int index] { get; set; }
    public int Count => _count;
    public T[] ToArray() { /* Return copy of active elements */ }
}
```

### Affected Files
- **BookStore/MyArrayList.cs**: New file for the generic dynamic array.
- **BookStore/Order.cs**: Change `List<Book>` to `MyArrayList<Book>`.
- **BookStore/AlgorithmHelper.cs**: Change parameters from `List<T>` to `MyArrayList<T>`.
- **BookStore/Program.cs**: Change `List<Order>` to `MyArrayList<Order>`.

## Implementation Plan
### Phase 1: Custom Collection
- [ ] Create `MyArrayList.cs` with generic support, `Add`, `RemoveAt`, and indexing.

### Phase 2: Refactoring Models & Algorithms
- [ ] Update `Order.cs` to use `MyArrayList<Book>`.
- [ ] Update `AlgorithmHelper.cs` signatures to accept `MyArrayList<T>`.

### Phase 3: Program Integration
- [ ] Update `Program.cs` to use `MyArrayList` for all collections.
- [ ] Ensure all loops and logic (like `processedOrders.Remove`) work with the new structure.

## Examples
### ✅ Good Pattern: Using custom indexer
```csharp
MyArrayList<Book> books = new MyArrayList<Book>();
books.Add(new Book());
var b = books[0]; 
```

### ❌ Bad Pattern: Mixing built-in and custom
```csharp
// Avoid this:
public void Process(MyArrayList<Book> list) {
    List<Book> temp = list.ToList(); // Don't convert back to built-in
}
```

## Trade-offs
- **Manual Memory Management**: We must handle array resizing manually.
- **Type Safety**: Using Generics (`<T>`) ensures type safety while remaining "custom."

---
## Implementation Results
- **Phase 1 (Custom Collection)**: ✅ Created `MyArrayList<T>` with dynamic resizing, indexing, and `Remove` capabilities.
- **Phase 2 (Models & Algorithms)**: ✅ Refactored `Order.cs` and `AlgorithmHelper.cs` to use `MyArrayList` instead of `List`.
- **Phase 3 (Integration)**: ✅ Updated `Program.cs` to use the custom dynamic array for all list-like operations.

### Deviations from original design
- **Equals Check**: In `MyArrayList.Remove`, I used `Equals(item1, item2)` to ensure compatibility with object references when removing orders from the `processedOrders` collection.
- **Clear for GC**: Added `_items[_count] = default(T)` in `RemoveAt` to prevent memory leaks by clearing references to removed objects.

### Summary of Deviations
The implementation followed the design plan perfectly. The project is now 100% free of built-in `System.Collections.Generic` collection types for its core logic, relying entirely on custom `MyStack`, `MyOrderQueue`, and `MyArrayList`.

