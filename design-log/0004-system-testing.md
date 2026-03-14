# Design Log 0004: System-wide Automated Testing

## Background
The bookstore system is built entirely from scratch using custom data structures (ArrayList, Queue, Stack) and manual algorithms (QuickSort, Binary Search). Every component must be verified for correctness to ensure the system is reliable without relying on built-in framework features.

## Problem
Manual testing through the CLI (Program.cs) is slow, incomplete, and difficult to repeat after changes. We need an automated way to verify:
1. Custom collection integrity (Edge cases like empty collections, resizing).
2. Sorting correctness (Title vs Author, empty lists).
3. Search accuracy (Found vs Not Found).

## Questions and Answers
- **Q:** Can we use NUnit/xUnit?
- **A:** No. We must follow the "no built-in/third-party" rule as much as possible. We will build a simple internal `TestRunner`.
- **Q:** How will we run it?
- **A:** We will add a hidden "6. Run Diagnostics/Tests" option in `Program.cs` or create a separate `TestRunner.cs` class.

## Design
We will implement a `TestRunner` class that executes assertions and prints success/failure reports.

### Test Structure
- `Assert(bool condition, string message)`: Prints ✅/❌.
- `RunAllTests()`: Orchestrates tests for all modules.

### Modules to Test
1. **MyArrayList**: `Add`, `Remove`, `Resize`.
2. **MyOrderQueue**: `Enqueue`, `Dequeue`, `Find`, `Count`.
3. **MyStack**: `Push`, `Pop`, `Count`.
4. **AlgorithmHelper**: `ManualStringCompare`, `QuickSort`, `BinarySearch`.

## Implementation Plan
1. **Phase 1: Setup**: Create `TestRunner.cs` in the `BookStore` project.
2. **Phase 2: Collection Tests**: Write tests for ArrayList, Queue, and Stack.
3. **Phase 3: Algorithm Tests**: Write tests for Sorting and Search.
4. **Phase 4: Integration**: Add a diagnostic trigger in `Program.cs`.

## Examples
### ✅ Good: Testing Boundary Conditions
```csharp
queue.Enqueue(o1);
queue.Dequeue();
Assert(queue.Count == 0, "Queue should be empty after dequeue");
```

### ❌ Bad: Assuming built-in behavior
```csharp
Assert(list.Contains(o), "Contains method doesn't exist yet");
```

## Trade-offs
- **Pros**: Zero dependencies, extremely fast, verifies "no built-in" constraint.
- **Cons**: Manual assertion reporting (no fancy UI), slightly more code to maintain.
