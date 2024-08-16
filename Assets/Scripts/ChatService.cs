using System;
using System.Collections.Generic;
using GoogleSpreadsheets;
using Newtonsoft.Json;
using UnityEngine;

namespace TestChat
{
    public class ChatService
    {
        private List<MessageModel> _history = new();
        private List<MessageModel> _answers = new();

        public event Action<MessageModel> OnMessageAdded;
        public event Action<string> OnMessageDeleted; 

        public List<MessageModel> GetAllMessages()
        {
                //получить из файла с историей
            return _history;
        }

        public void ImportMessages()
        {
            var textAsset = Resources.Load<TextAsset>("messages");
            var chatContent = JsonConvert.DeserializeObject<ChatContent>(textAsset.text);
            foreach (var message in chatContent.messages)
            {
                var messageModel = new MessageModel(message.Avatar, message.Message, message.Nickname, message.Time, message.MessageID, message.UserID);
                _answers.Add(messageModel);
            }
                _history.Add(_answers[UnityEngine.Random.Range(0, _answers.Count)]);
        }

        public void AddNewMessage(MessageModel messageModel)
        {
            _history.Add(messageModel);
            OnMessageAdded?.Invoke(messageModel);
        }

        public MessageModel GetRandomAnswer()
        {
            
            var randomAnswer = _answers[UnityEngine.Random.Range(0, _answers.Count)];
            MessageModel messageModel = new MessageModel(randomAnswer.AvatarPath, randomAnswer.Message,
                randomAnswer.Nickname, DateTime.Now, Guid.NewGuid().ToString(), randomAnswer.UserID);

            return messageModel;
        }

        public void DeleteMessage(string messageModelID)
        { 
            int index = _history.FindIndex(m => m.MessageID == messageModelID);
            if (index == -1)
                return;
            _history.RemoveAt(index);
            OnMessageDeleted?.Invoke(messageModelID);
        }
    }
}