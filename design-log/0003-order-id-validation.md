# Design Log #0003: Order ID Uniqueness Validation

## Background
The current order processing system allows multiple orders to be created with the same `OrderID`. This causes issues with order tracking (Binary Search) and breaks the logical requirement of unique identifiers for records.

## Problem
1. Case 1 in `Program.cs` takes an `int id` but does not verify if it's already in use.
2. Orders can exist in three states: `pendingQueue` (Queue), `processedOrders` (ArrayList), or `historyStack` (Stack).
3. We need a reliable way to check for duplicates across all active collections.

## Questions and Answers
**Q: Where should the uniqueness check be performed?**
A: Inside Case 1 of `Program.cs`, before the order is actually enqueued.

**Q: Should we add search methods to `MyArrayList` and `MyOrderQueue`?**
A: Yes, adding a `Contains(int orderID)` method to these custom structures is the most "DSA-like" approach.

**Q: What about the `historyStack`?**
A: Orders in the history stack are also in the `processedOrders` list (as per current Case 2 logic), so checking the list and the queue covers all bases.

## Design
### Collection Enhancements
- **MyArrayList<T>**: Add `bool ContainsOrderID(int id)` specifically for Order types, or a more generic `bool Any(Predicate<T> match)`. Since this is a basic DSA project, a specific helper or a simple loop is better.
- **MyOrderQueue**: Add `bool ContainsOrderID(int id)` that traverses the linked nodes.

### Logic Update
- **Program.cs**:
  ```csharp
  if (ExistsInSystem(id)) {
      Console.WriteLine("Error: Order ID already exists.");
      break;
  }
  ```

## Implementation Plan
### Phase 1: Search Capabilities
- [ ] Add `Exists(int id)` to `MyArrayList<Order>`.
- [ ] Add `Exists(int id)` to `MyOrderQueue`.

### Phase 2: Validation Integration
- [ ] Update Case 1 in `Program.cs` to use these checks before finalizing an order.

## Examples
### ✅ Good Pattern: Pre-validation
```csharp
if (pendingQueue.Exists(id) || processedOrders.Exists(id)) {
    Console.WriteLine("Duplicate ID!");
}
```

## Trade-offs
- **Performance**: Searching the queue/list is $O(n)$. While slower than a Hash Set ($O(1)$), it is appropriate for a basic DSA assignment where Hash Maps might not have been introduced yet.

---
## Implementation Results
- **Phase 1 (Search Capabilities)**: ✅ Added generic `Exists(Predicate<T>)` to `MyArrayList<T>` and `Exists(int id)` to `MyOrderQueue`.
- **Phase 2 (Validation)**: ✅ Integrated uniqueness check in `Program.cs` Case 1. The system now prevents adding duplicate IDs and provides an error message.

### Deviations from original design
- None. The implementation followed the design plan exactly.

### Summary of Deviations
The project now correctly validates Order ID uniqueness across all active data structures (Queue and ArrayList), ensuring the integrity of the tracking system (Binary Search).

