# Design Log #0001: Book Quantities and Multi-Criteria Sorting

## Background
The Online Bookstore project manages orders containing a list of books. Currently, the system supports alphabetical sorting by book title using QuickSort, but it lacks support for book quantities and sorting by other criteria like author name, as mentioned in the project scenario.

## Problem
1. The project scenario requires "a list of books with their **quantities**," which is currently missing from the `Book` class.
2. The scenario suggests sorting by "book title, **author name**." The current implementation only supports Title-based sorting.
3. The order processing currently only supports a single sorting criterion.

## Questions and Answers
**Q: Where should the `Quantity` property be added?**
A: In `BookStore/Book.cs` as a `public int Quantity { get; set; }`.

**Q: How should sorting by Author be implemented?**
A: Enhance `AlgorithmHelper.QuickSortBooks` to accept a `Comparison<Book>` or a string parameter to determine the sorting criterion.

**Q: Should sorting by ID in Case 4 be moved to AlgorithmHelper?**
A: Yes, for consistency and reuse.

## Design
### Data Structure Changes
- **Book.cs**:
  ```csharp
  public class Book {
      public string Title { get; set; }
      public string Author { get; set; }
      public int Quantity { get; set; } // New property
  }
  ```

### Algorithm Changes
- **AlgorithmHelper.cs**:
  - Update `QuickSortBooks` to take a sorting criterion.
  - Add `QuickSortOrdersByID` for Case 4 (Binary Search requirement).

### UI/Process Changes
- **Program.cs**:
  - Update Case 1 (Place New Order) to prompt for quantity for each book.
  - Update Case 2 (Confirm & Process) to allow the user to choose between sorting by Title or Author.

## Implementation Plan
### Phase 1: Data Model Update
- [ ] Add `Quantity` property to `Book.cs`.

### Phase 2: Algorithm Enhancement
- [ ] Refactor `QuickSortBooks` in `AlgorithmHelper.cs` to support dynamic criteria.
- [ ] Add sorting for `Order` by ID to `AlgorithmHelper.cs`.

### Phase 3: Program Logic Update
- [ ] Update `Program.cs` Case 1 to collect quantity.
- [ ] Update `Program.cs` Case 2 to prompt for sorting criteria.
- [ ] Update `Program.cs` Case 4 to use the new sorting method from `AlgorithmHelper`.

## Examples
### ✅ Good Pattern: Collecting all required data
```csharp
Console.Write("Quantity: ");
int qty = int.Parse(Console.ReadLine());
o.BookList.Add(new Book { Title = title, Author = author, Quantity = qty });
```

### ❌ Bad Pattern: Hardcoded sorting criteria
```csharp
// Avoid this:
AlgorithmHelper.QuickSortBooksByTitle(list);
AlgorithmHelper.QuickSortBooksByAuthor(list);
```

## Trade-offs
- **Simplicity vs. Generality**: Using a string parameter ("title", "author") for sorting is simple for this CLI application, though a delegate approach would be more powerful in a larger system.
- **Validation**: Adding quantities introduces a need for input validation (e.g., quantity > 0), which slightly increases code complexity.

---
## Implementation Results
- **Phase 1 (Data Model)**: ✅ Successfully added `Quantity` to `Book.cs`.
- **Phase 2 (Algorithm)**: ✅ Updated `AlgorithmHelper.cs` with dynamic `QuickSortBooks` and new `QuickSortOrders`.
- **Phase 3 (UI)**: ✅ Updated `Program.cs` to handle quantity input and sorting selection.

### Deviations from original design
- **Binary Search Sorting**: Instead of using the built-in `List.Sort()` in Case 4, I implemented a custom `QuickSortOrders` in `AlgorithmHelper` to remain consistent with the requirement of using custom data structures and algorithms.
- **Empty State Handling**: Added basic checks for empty queues or history stacks to prevent runtime errors (e.g., in Case 2 and 3).

### Summary of Deviations
The implementation followed the plan closely, with the primary improvement being the replacement of the built-in `List.Sort` with a custom `QuickSortOrders` method to better align with the Academic/DSA scenario's intent.

