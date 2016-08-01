using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace XmlElementValuesInDirectory {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter directory of xml files:");
            string parentPath = Console.ReadLine();

            Console.WriteLine("Enter xml element name:");
            string xmlElementName = Console.ReadLine();

            string[] files = Directory.GetFiles(parentPath);

            IEnumerable<string> specifiedXmlElementValues = files.SelectMany(file => {
                var convertedFile = new XmlDocument();
                var specifiedXmlElementValuesStringList = new List<string>();

                using (StreamReader sr = new StreamReader(file, detectEncodingFromByteOrderMarks: true)) {  // Let the streamreader handle encoding.
                    convertedFile.Load(sr);

                    var valuesFromCurrentFile = convertedFile.DocumentElement.SelectNodes(string.Format("//{0}", xmlElementName));

                    foreach (XmlNode value in valuesFromCurrentFile) {
                        specifiedXmlElementValuesStringList.Add(value.InnerText);
                    }
                }

                return specifiedXmlElementValuesStringList;
            });

            if (specifiedXmlElementValues.Any()) Console.WriteLine(string.Join(Environment.NewLine, specifiedXmlElementValues));
            else Console.WriteLine(string.Format("Found no values for xml element '{0}'", xmlElementName));

            Console.ReadLine();
        }
    }
}