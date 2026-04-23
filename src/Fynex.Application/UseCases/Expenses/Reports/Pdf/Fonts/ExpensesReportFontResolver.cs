using PdfSharp.Fonts;
using System.Reflection;

namespace Fynex.Application.UseCases.Expenses.Reports.Pdf.Fonts;

public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        if (stream is null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }

        var length = (int)stream!.Length;
        var data = new byte[length];
        stream.ReadExactly(buffer: data, offset: 0, count: length);
        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        return new FontResolverInfo(familyName);
    }

    public Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return assembly.GetManifestResourceStream($"Fynex.Application.UseCases.Expenses.Reports.Pdf.Fonts.{faceName}.ttf");

    }
}