using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpreadsheets;
using Newtonsoft.Json;
using UnityEngine;

namespace TestChat
{
    public class ApplicationController: MonoBehaviour
    {
        private void Start()
        {
            var messages = ImportMessages();
            new ChatPresenter().LoadAndShowWindow(messages.OrderByDescending(m => m.Time).ToList());
        }

        private List<MessageModel> ImportMessages()
        {
            var textAsset = Resources.Load<TextAsset>("messages");
            var chatContent = JsonConvert.DeserializeObject<ChatContent>(textAsset.text);
            var messages = new List<MessageModel>();
            foreach (var message in chatContent.messages)
            {
                var messageModel = new MessageModel(message.Avatar, message.Message, message.Nickname, message.Time, message.MessageID, message.UserID);
                messages.Add(messageModel);
            }

            return messages;
        }
    }
}