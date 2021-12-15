using System.Linq;
using System.Xml.Linq;

namespace XmlSorter.Extension
{
    public static class XElementExtension
    {
        public static XAttribute GetAttributeByPath(this XElement xElement, string path)
        {
            var pathParams = path.Split('/', '\\');

            if (pathParams.Length == 1)
            {
                return xElement.Attribute(pathParams[0]);
            }

            if (pathParams.Length > 1)
            {
                return xElement.Descendants(pathParams[pathParams.Length - 2]).First().Attribute(pathParams[pathParams.Length - 1]);
            }

            return default;
        }
    }
}
