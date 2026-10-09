using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AshkanFitPro
{
    public partial class FinancialReportForm : Form
    {
        private readonly Store store;
        private readonly bool persian;
        private sealed class FinancialRow
        {
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public decimal Income { get; set; }
            public decimal Expense { get; set; }
            public decimal Net { get { return Income - Expense; } }
        }
        private List<FinancialRow> current = new List<FinancialRow>();
        public FinancialReportForm(Store data, bool isPersian)
        {
            if (data == null) throw new ArgumentNullException("data");
            store = data;
            persian = isPersian;
            InitializeComponent();
            Text = persian ? "گزارش مالی باشگاه | AshkanFit" : "Financial analytics | AshkanFit";
            RightToLeft = persian ? RightToLeft.Yes : RightToLeft.No;
            rightToLeftLayout();
            dateFrom.Value = DateTime.Today.AddMonths(-3);
            dateTo.Value = DateTime.Today;
            refreshButton.Text = persian ? "نمایش گزارش" : "Refresh report";
            exportButton.Text = persian ? "خروجی CSV" : "Export CSV";
            refreshButton.Click += (s,e) => RefreshReport();
            exportButton.Click += (s,e) => ExportCsv();
            dateFrom.ValueChanged += (s,e) => RefreshReport();
            dateTo.ValueChanged += (s,e) => RefreshReport();
            RefreshReport();
        }
        private void rightToLeftLayout() { reportGrid.RightToLeft = persian ? RightToLeft.Yes : RightToLeft.No; }
        private void RefreshReport()
        {
            var start = dateFrom.Value.Date;
            var end = dateTo.Value.Date;
            if (start > end) { reportGrid.DataSource = null; summaryLabel.Text = persian ? "بازه تاریخ نامعتبر است" : "Invalid date range"; current.Clear(); return; }
            current = store.Payments.Where(x => x.Date.Date >= start && x.Date.Date <= end)
                .Select(x => new FinancialRow { Date=x.Date, Type=persian ? "درآمد" : "Income", Description=x.Note ?? "", Income=x.Amount })
                .Concat(store.Expenses.Where(x => x.Date.Date >= start && x.Date.Date <= end)
                .Select(x => new FinancialRow { Date=x.Date, Type=persian ? "هزینه" : "Expense", Description=(x.Category ?? "") + " - " + (x.Description ?? ""), Expense=x.Amount }))
                .OrderByDescending(x => x.Date).ToList();
            decimal income = current.Sum(x => x.Income), expenses = current.Sum(x => x.Expense);
            summaryLabel.Text = string.Format(CultureInfo.CurrentCulture,
                persian ? "درآمد: {0:N0}    |    هزینه: {1:N0}    |    خالص: {2:N0}    |    تراکنش: {3}" : "Income: {0:N0}    |    Expenses: {1:N0}    |    Net: {2:N0}    |    Transactions: {3}",
                income, expenses, income - expenses, current.Count);
            reportGrid.DataSource = current.Select(x => new { Date=x.Date.ToString("yyyy-MM-dd HH:mm"), x.Type, x.Description, x.Income, x.Expense, x.Net }).ToList();
        }
        private static string Csv(string value) { return "\"" + (value ?? "").Replace("\"", "\"\"") + "\""; }
        private void ExportCsv()
        {
            using (var dialog = new SaveFileDialog { Filter="CSV (*.csv)|*.csv", FileName="ashkanfit-financial-report.csv" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                var sb = new StringBuilder("Date,Type,Description,Income,Expense,Net\r\n");
                foreach (var row in current)
                    sb.Append(row.Date.ToString("o", CultureInfo.InvariantCulture)).Append(',')
                      .Append(Csv(row.Type)).Append(',').Append(Csv(row.Description)).Append(',')
                      .Append(row.Income.ToString(CultureInfo.InvariantCulture)).Append(',')
                      .Append(row.Expense.ToString(CultureInfo.InvariantCulture)).Append(',')
                      .Append(row.Net.ToString(CultureInfo.InvariantCulture)).Append("\r\n");
                try { File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true)); }
                catch (Exception ex) { MessageBox.Show(this, ex.Message, "AshkanFit", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
