using ClosedXML.Excel;
using Newtonsoft.Json.Linq;

namespace SolveX.Framework.Utilities.Common;
public static class ExportUtilities
{
    public static MemoryStream ExportExcel<T>(T obj) where T : class
    {
        using (MemoryStream stream = new MemoryStream())
        {
            var workbook = new XLWorkbook();

            IXLWorksheet worksheet = workbook.Worksheets.Add("Exported asset");

            int row = 1;
            foreach (var prop in obj.GetType().GetProperties())
            {
                
                try
                {
                    
                    JObject jsonObject = JObject.Parse((string)prop.GetValue(obj));

                    Dictionary<string, string> dictionary = new Dictionary<string, string>();

                    AddProperties(jsonObject, dictionary);

                    foreach (KeyValuePair<string, string> entry in dictionary)
                    {
                        worksheet.Cell(row, 1).Value = entry.Key;
                        worksheet.Cell(row, 2).Value = entry.Value;
                        row++;
                    }
                }
                catch
                {
                    worksheet.Cell(row, 1).Value = prop.Name;
                    worksheet.Cell(row, 2).Value = (string)prop.GetValue(obj);
                }
                finally
                {
                    row++;
                }
            }

            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }

    static void AddProperties(JToken token, Dictionary<string, string> dictionary)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
                foreach (JProperty property in token.Children<JProperty>())
                {
                    AddProperties(property.Value, dictionary);
                }
                break;
            case JTokenType.Array:
                foreach (JToken arrayItem in token.Children())
                {
                    AddProperties(arrayItem, dictionary);
                }
                break;
            default:
                dictionary.Add(token.Path, ((JValue)token).Value.ToString());
                break;
        }
    }
}
