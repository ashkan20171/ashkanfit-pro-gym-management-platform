# Stage 51 — Data Quality & GitHub Portfolio Release

- Admin-only, read-only data-quality center in the sidebar.
- Checks for missing references, invalid fees/expenses, duplicates, expired memberships, renewals within seven days, missing contact information, and inverted membership dates.
- CSV export with UTF-8 BOM for Excel and safe CSV escaping.
- Bilingual FA/EN WinForms with separate Designer file.
- New GitHub-ready English README, SECURITY.md, CONTRIBUTING.md and release notes.
- This release does **not** introduce true per-entity SQL repositories, SMS notifications, or complete database-level RBAC.
- Windows compilation and live SQL Server integration tests were not possible in this environment.
