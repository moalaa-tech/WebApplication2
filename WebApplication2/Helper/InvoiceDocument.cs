using CRM.WebApp.DTOs.OrderDetails;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CRM.WebApp.Helper
{
    public class InvoiceDocument : IDocument
    {
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public List<OrderDetailsDto> Items { get; set; }


        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(14));


                page.Header().Column(col =>
                {
                    col.Item().Text($"Invoice #{InvoiceNumber}")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);
                    col.Item().Text($"Customer: {CustomerName}");
                    col.Item().Text($"Date: {DateTime.Now:yyyy-MM-dd}");
                    col.Item().PaddingVertical(10).LineHorizontal(1);
                });



                page.Content().Element(ComposeTable);



                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                    x.Span(" of ");
                    x.TotalPages();
                });
            });
        }

        static IContainer CellStyle(IContainer container)
        {
            return container
                .PaddingVertical(5)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2);
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // Column widths
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Item
                    columns.RelativeColumn(1); // Qty
                    columns.RelativeColumn(2); // Unit price
                    columns.RelativeColumn(2); // Total
                });

                // Header row
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Item").SemiBold();
                    header.Cell().Element(CellStyle).AlignRight().Text("Qty").SemiBold();
                    header.Cell().Element(CellStyle).AlignRight().Text("Unit Price").SemiBold();
                    header.Cell().Element(CellStyle).AlignRight().Text("Total").SemiBold();
                });

                decimal Itemstotal = 0;
                // Body rows
                foreach (var item in Items)
                {
                    table.Cell().Element(CellStyle).Text(item.Product.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.ItemsCount.ToString());
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Product.TotalCost:F2}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Product.TotalCost:F2}");

                    Itemstotal += item.Product.TotalCost.GetValueOrDefault(0);
                }

                // Total row
                var total = Itemstotal;
                table.Cell().ColumnSpan(3).Element(CellStyle).AlignRight().Text("Grand Total").SemiBold();
                table.Cell().Element(CellStyle).AlignRight().Text($"{total:F2}").SemiBold();
            });
        }

    }
}
