# Design Log #0006 - Add Books Functionality and Inventory Management

## Background
The bookstore needs a way to manage its actual stock (Inventory) rather than just processing arbitrary orders. This involves adding new books to the shop and searching for availability.

## Problem
*   Lack of a central `inventory` system to track books.
*   "Add Books" needs to handle existing stock (incrementing quantity) vs. new titles.
*   "Search Book" is required for staff to verify availability before confirming orders.
*   **Constraint**: No built-in collections or LINQ allowed.

## Questions and Answers
*   **Q: How do we handle the "10 initial books"?**
    *   A: We will initialize the `inventory` list in `Program.cs` with 10 sample books upon startup.
*   **Q: How should we search for books in inventory?**
    *   A: We will implement a linear search in `MyArrayList<T>` and potentially a `BinarySearchBooks` in `AlgorithmHelper` for faster lookup by Title.
*   **Q: What defines a "duplicate" book in inventory?**
    *   A: A book with the exact same Title and Author (case-insensitive manual check).

## Design

### 1. Enhanced `MyArrayList<T>`
Add a `Find` method to retrieve items:
```csharp
public T Find(Predicate<T> match) {
    for (int i = 0; i < _count; i++) {
        if (match(_items[i])) return _items[i];
    }
    return default(T);
}
```

### 2. Inventory Management Logic
*   `inventory`: `MyArrayList<Book>`
*   `AddBook(title, author, qty)`:
    1. Search `inventory` for Title + Author.
    2. If found, `existingBook.Quantity += qty`.
    3. If not found, `inventory.Add(new Book { ... })`.

### 3. Search Logic
*   `SearchBook(title)`: Returns the book details or "Not Found".

## Implementation Plan

### Phase 1: Custom Collection Update
*   [ ] Add `Find` method to `MyArrayList<T>` in `MyArrayList.cs`.

### Phase 2: Inventory Initialization
*   [ ] In `Program.cs`, create `static MyArrayList<Book> inventory`.
*   [ ] Add a method `InitializeInventory()` with 10 hardcoded books.

### Phase 3: Inventory UI & Logic
*   [ ] Add Menu Option "6. Manage Inventory (Add/Search)".
*   [ ] Implement "Add Book" with duplication check.
*   [ ] Implement "Search Book" by Title.

### Phase 4: Order Integration (Future)
*   [ ] Update "Place Order" to deduct from inventory (optional for this task, but planned).

## Examples

### ✅ Good: Merging Stock
Add ("The Hobbit", "Tolkien", 5) to inventory already containing ("The Hobbit", "Tolkien", 2) -> Result: 7 copies.

### ❌ Bad: Redundant Entry
Add ("The Hobbit", "Tolkien", 5) creates a second entry in the list instead of updating the first.

## Trade-offs
*   **Linear Search for Add**: We use $O(N)$ linear search to find duplicates. Since N is small (~10-50), this is efficient enough and simpler than maintaining a sorted list for Binary Search during every "Add" operation.

## Implementation Results

### Completed Phases
- **Phase 1**: Added `Find(Predicate<T> match)` to `MyArrayList<T>`.
- **Phase 2**: Created `InitializeInventory` in `Program.cs` with 10 default books.
- **Phase 3**: Added Menu Option 5 "Manage Inventory" with sub-options for Add, Search, and View All.
- **Phase 4**: Integrated inventory check into "Place Order" (Choice 1) to warn staff if books are missing or out of stock.

### Deviations
- Added "View All Inventory" (Choice 3 in sub-menu) to make it easier for staff to see the full list.
- Integrated inventory warning directly into the order placement loop to fulfill the "search before confirm" requirement more proactively.

### Validation
- Manual verification of "Add" logic: Successfully increments quantity for existing Title+Author pairs.
- Manual verification of "Search" logic: Correctly finds books by Title.
- Inventory warnings trigger correctly during "Place New Order" when a non-existent title is entered.
