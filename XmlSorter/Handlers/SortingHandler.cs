using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XmlSorter.Extension;

namespace XmlSorter.Handlers
{
    public class SortingHandler
    {
        private readonly string _rootElement;
        private readonly string _elementName;
        private readonly string[] _orderBy;

        public SortingHandler(string rootElement, string elementName, params string[] orderBy)
        {
            _rootElement = rootElement;
            _elementName = elementName;
            _orderBy = orderBy;
        }

        public XDocument Handle(XDocument xDocument)
        {
            if (xDocument == null || xDocument.Root == null)
            {
                return xDocument;
            }

            var sortedElements = Sort(xDocument.Root.Descendants(_rootElement));
            
            xDocument.Root.Descendants(_rootElement).First().ReplaceNodes(sortedElements);
            
            return xDocument;
        }

        private IEnumerable<XElement> Sort(IEnumerable<XElement> elements)
        {
            var result = elements.Elements(_elementName);
            
            foreach (var sortingParams in _orderBy)
            {
                if (result is IOrderedEnumerable<XElement> orderedResult)
                {
                    result = orderedResult.ThenBy(elem => elem.GetAttributeByPath(sortingParams)?.Value);
                    continue;
                }

                if (result is IEnumerable<XElement> enumerableResult)
                {
                    result = enumerableResult.OrderBy(elem => elem.GetAttributeByPath(sortingParams)?.Value);
                }
            }

            return result.ToList();
        }
    }
}
