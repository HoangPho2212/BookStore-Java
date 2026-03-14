# Design Log #0005: Detailed Order Tracking and Transparency

## Background
Currently, tracking an order status (Case 4) only shows a short string like "Status: Sorted by title & Confirmed". This doesn't provide enough transparency to the user that the sorting actually occurred or what books are in the order.

## Problem
1. Tracking an order should show the full details of the order (Customer, Address, etc.).
2. The user should be able to see the list of books and confirm they are sorted correctly as requested.

## Questions and Answers
**Q: Where should the display logic go?**
A: To keep `Program.cs` clean, we will add a `PrintOrderDetails()` method to the `Order` class.

**Q: Should we show the status first?**
A: Yes, the status is the primary information, followed by the order breakdown.

## Design
### Data Structure Changes
- **Order.cs**: Add a method to print the order's contents.

### UI/Process Changes
- **Program.cs**: Update Case 4 to call the new display method after finding the order.

## Implementation Plan
### Phase 1: Order Class Update
- [ ] Add `PrintOrderDetails()` to `Order.cs` that iterates through `BookList`.

### Phase 2: Program Logic Update
- [ ] Update Case 4 in `Program.cs` to use `PrintOrderDetails()` for both pending and processed orders.

## Examples
### ✅ Good Pattern: Detailed output
```text
>>> Status: Sorted by title & Confirmed
Order Details:
  Customer: John Doe
  Address: 123 Main St
  Books (Sorted):
    - Algorithms (Author: Smith, Qty: 1)
    - C++ (Author: Jones, Qty: 2)
```

## Trade-offs
- **Pros**: Better user experience, proves the sorting algorithm works.
- **Cons**: More text output in the CLI.
