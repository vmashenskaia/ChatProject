using System.Collections.Generic;

namespace GoogleSpreadsheets
{
    public  interface IGoogleSheetParser
    { 
        public MessageInfo Parse(ICollection<string> headers, ICollection<object> elements);
    }
}