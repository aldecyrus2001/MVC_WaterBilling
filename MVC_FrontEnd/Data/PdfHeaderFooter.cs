using iTextSharp.text.pdf;
using iTextSharp.text;

public class PdfHeaderFooter : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfPTable header = new PdfPTable(1);
        header.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

        PdfPCell cell = new PdfPCell(new Phrase("Reading Records", new Font(Font.HELVETICA, 12, Font.BOLD)));
        cell.Border = Rectangle.NO_BORDER;
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        header.AddCell(cell);

        header.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 10, writer.DirectContent);
    }
}
