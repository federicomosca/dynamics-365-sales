using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ImportPayments
{
    public sealed class ParametersIn
    {
        public static string debug => "debug";
        public static string file => "file";
    }

    public sealed class ParametersOut
    {
        public static string result => "result";
        public static string error => "error";
    }

    public class FileIn
    {
        public static string mimetype => "mimetype";
        public static string name => "name";
        public static string size => "size";
        public static string content => "content";
        public static string datauri => "datauri";
    }

    public class Configuration
    {
        public int header_line { get; set; }
        public List<Field> fields { get; set; }
        public int number_lines_remove { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string name_payment { get; set; }
        public int position { get; set; }
    }
}
