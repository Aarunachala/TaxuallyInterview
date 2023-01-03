using System.Xml.Serialization;

namespace Taxually.TechnicalTest.Utils
{
    public static class TaxuallyXMLSerializer
    {
        
        public static async Task<string> SerializeXML<T>(object xmlContent)
        {
            try
            {
                using (var stringwriter = new StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stringwriter, xmlContent);
                    var xml = stringwriter.ToString();
                    return xml;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
