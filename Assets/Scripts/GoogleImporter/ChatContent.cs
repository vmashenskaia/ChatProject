using System;
using System.Collections.Generic;

namespace GoogleSpreadsheets
{
    [Serializable]
    public class ChatContent
    {
        public List<MessageInfo> messages;
    }
}