using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AuthenticateData
{
    public class Language
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<LanguageResourceData> Resource { get; set; }
        public List<ComboLanguage> Languages { get; set; }
        public string Code { get; set; }
    }
    public class ComboLanguage
    {
        /// <summary>
        /// this dummy class is used for load lanuage dropdown
        ///  when we got the language json we remove it
        /// </summary>
        public string Key { get; set; }
        public string Value { get; set; }
        public string url { get; set; }
    }
}
