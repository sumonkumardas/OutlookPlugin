using System.Collections.Generic;
using System.Xml;

namespace Util.Utilities
{
    public class XmlParseValue
    {
        /// <summary>
        /// Find the node value of a xml string
        /// </summary>
        /// <param name="innerXml">xml string</param>
        /// <param name="searchValue">value of the xml node</param>
        /// <returns></returns>
        public List<string> GetNodeValue(string innerXml, string searchValue)
        {
            List<string> nodeValues = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(innerXml);


            XmlNodeList tagsList = doc.GetElementsByTagName(searchValue);
            if (tagsList.Count == 0)
            {
                return nodeValues;
            }

            int tagsListLength = tagsList.Count;
            for (int i = 0; i < tagsListLength; i++)
            {
                nodeValues.Add(tagsList[i].InnerText);
            }
            return nodeValues;
        }
    }
}
