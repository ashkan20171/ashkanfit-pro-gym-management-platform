# AshkanFit Pro — Gym Management Platform

**A bilingual Windows desktop application for gym operations, built with C# WinForms and SQL Server support.**

AshkanFit Pro demonstrates pragmatic desktop engineering: member lifecycle management, subscription renewals, attendance tracking, trainers and workouts, financial reporting, administrative dashboards, and data integrity workflows.

## Highlights

- **Bilingual UX:** Persian (RTL) and English (LTR).
- **Membership operations:** member records, expiry monitoring, renewal alerts, attendance and training plans.
- **Finance:** payments, expenses, CSV exports, financial trends and executive overview.
- **Data quality (Stage 51):** admin-only diagnostics with actionable findings and UTF-8 CSV export.
- **Persistence:** local JSON mode or SQL Server snapshot persistence with relational reporting mirrors.
- **Recovery and audit:** snapshot history, integrity verification, verified restore workflow and partial SQL audit trail.
- **Access:** admin and limited operational roles; some restrictions are enforced at the UI layer only.

## Technology

C# 7.3 · .NET Framework 4.8 · Windows Forms · Visual Studio 2022 · SQL Server · ADO.NET · JSON serialization

## Quick start

1. Open `AshkanFitPro.sln` in **Visual Studio 2022** on Windows.
2. Install the .NET Framework 4.8 targeting pack if prompted.
3. Build and run the Windows Forms application. Create the administrator account when prompted; **no default credentials** are supplied.
4. Default storage is JSON. For SQL Server, configure `AshkanFitPro/App.config`, provision the database and review the scripts under `database/` in order.
5. Back up existing databases before migrations or any restore operation.

## Architecture notes

The SQL Server implementation stores a serialized snapshot as its primary state and maintains relational reporting tables. It is **not yet** a complete entity-level repository/CRUD architecture. The audit trail does not cover every user action. A verified restore does not roll back SQL user accounts or external files. Do not deploy for sensitive production use without a full security and integration review.

## Portfolio walkthrough

1. Create an admin account and add a member with a membership end date.
2. Register attendance and a payment; inspect the executive dashboard and finance trends.
3. Open **Data quality** to review findings and export a CSV.
4. In SQL mode, review snapshot history and security audit before testing recovery in an isolated database.

## Validation status

ZIP integrity, `.csproj` file references and XML parsing are checked for this release. **Compilation on Windows, automated runtime tests and SQL Server integration have not been completed.**

## Repository topics

`csharp` · `winforms` · `dotnet-framework` · `sql-server` · `gym-management` · `desktop-application` · `bilingual` · `rtl` · `ado-net` · `portfolio-project`

## License

See `LICENSE`.
"# ashkanfit-pro-gym-management-platform" 
