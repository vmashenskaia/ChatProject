using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class MessageInfoParser: IGoogleSheetParser
    {
        public MessageInfo Parse(ICollection<string> headers, ICollection<object> elements)
        {
            MessageInfo messageInfo = new();
            for (int i = 0; i < headers.Count; i++)
            {
                var header = headers.ElementAt(i);
                var element = elements.ElementAt(i);
                switch (header)
                {
                    case "MessageID":
                        messageInfo.MessageID = element.ToString();
                        break;
                    case "Nickname":
                        messageInfo.Nickname = element.ToString();
                        break;
                    case "Message" :
                        messageInfo.Message = element.ToString();
                        break;
                    case "Time":
                        messageInfo.Time = element.ToString();
                        break;
                    case "Avatar":
                        messageInfo.Avatar = element.ToString();
                        break;
                    case "UserID":
                        messageInfo.UserID = element.ToString();
                        break;
                    default:
                        throw new Exception($"Invalid header: {header}");
                } 
            }
            
            Debug.LogWarning(JsonUtility.ToJson(messageInfo));
            return messageInfo;
        }
    }
}