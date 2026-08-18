# Global Search Sprint Board

This backlog turns the global search plan into sprint-ready work for the ASP.NET Core MVC project.

## Sprint 1: Core Search Backbone

Goal: make search secure, fast enough, and ready for UI integration.

| ID | Ticket | Estimate | Depends on | Notes |
|---|---|---:|---|---|
| GS-1 | Define search contracts | 1 pt | - | Add `SearchRequest`, `SearchResultDto`, and any shared enums/models. |
| GS-2 | Inventory searchable entities and fields | 1 pt | - | Confirm the first-wave scope: customers, contacts, vendors, companies, products, invoices, quotes, orders, tickets, documents. |
| GS-3 | Implement `ISearchService` | 3 pts | GS-1, GS-2 | Central service that coordinates querying and result normalization. |
| GS-4 | Add permission filtering to search | 3 pts | GS-3 | Reuse existing auth and ownership rules. No unauthorized leakage. |
| GS-5 | Add search endpoint | 2 pts | GS-3, GS-4 | `GET /api/search` or `GET /search`, authenticated only. |
| GS-6 | Add database indexes for search fields | 3 pts | GS-2 | Focus on names, emails, phone numbers, document numbers, codes, titles. |
| GS-7 | Implement CRM entity search | 3 pts | GS-3, GS-4, GS-6 | Customers, contacts, vendors, companies. |
| GS-8 | Add ranking rules | 2 pts | GS-3 | Exact matches first, then prefix, then partial. |
| GS-9 | Add service tests | 3 pts | GS-3, GS-4, GS-8 | Cover ranking, access control, empty results, and multi-entity merging. |

Exit criteria for Sprint 1:

- Search returns real results from multiple entities.
- Unauthorized results do not appear.
- API is stable and test-covered enough for UI work.

## Sprint 2: UI, Usability, and Coverage

Goal: make search easy to use everywhere in the app.

| ID | Ticket | Estimate | Depends on | Notes |
|---|---|---:|---|---|
| GS-10 | Add header search component | 3 pts | GS-5 | Global search box in the shared layout. |
| GS-11 | Implement instant search with debounce | 3 pts | GS-10, GS-5 | Trigger search while typing; add loading and empty states. |
| GS-12 | Group results by entity type | 2 pts | GS-11 | Sections like Customers, Tickets, Products, Documents. |
| GS-13 | Add keyboard shortcuts and navigation | 2 pts | GS-10, GS-11 | `Ctrl/Cmd + K`, arrow keys, Enter, Escape. |
| GS-14 | Add quick actions from results | 2 pts | GS-11 | Open, view, edit, copy reference where appropriate. |
| GS-15 | Add recent searches and recent records | 2 pts | GS-10 | Per-user persistence for faster repeat navigation. |
| GS-16 | Add optional filters | 3 pts | GS-11 | Module, status, owner, company, date range. |
| GS-17 | Extend search to support and documents | 3 pts | GS-7 | Tickets, service requests, document titles, file names. |
| GS-18 | Add telemetry and usage logging | 2 pts | GS-5, GS-11 | Log query volume, zero-result searches, clicks, latency. |
| GS-19 | Mobile and error-state polish | 2 pts | GS-10, GS-11 | Ensure the dropdown works on smaller screens and handles failures gracefully. |

Exit criteria for Sprint 2:

- Users can search from anywhere in the app.
- Search feels responsive and predictable.
- The most-used records are covered.
- Usage data is available to plan the next expansion.

## Dependency Map

Critical path:

- `GS-1` and `GS-2` unlock most other work.
- `GS-3` is the core service layer.
- `GS-4` and `GS-5` depend on `GS-3`.
- `GS-10` depends on the endpoint being ready.
- `GS-11` and `GS-12` depend on the UI shell.
- `GS-13`, `GS-14`, and `GS-15` depend on the search UI existing.
- `GS-17` can be done after CRM search is stable.
- `GS-18` can start as soon as the endpoint is live.

## Recommended Implementation Order

1. Contracts and scope.
2. Search service and permissions.
3. Indexing and ranking.
4. API endpoint and tests.
5. Header UI and instant results.
6. Keyboard shortcuts and quick actions.
7. Recent items, filters, telemetry, and polish.
8. Expand to support and document search.
