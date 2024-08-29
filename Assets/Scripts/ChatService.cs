using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GoogleSpreadsheets;
using NUnit.Framework;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace TestChat
{
    public class ChatService
    {
        private const string messagesAsset = "messages";
        private const string directoryForSaveHistory = "directoryForSaveHistory";
        private const string historyFileName = "historyFile2";

        private DataParser _dataParser = new();

        private List<MessageModel> _history = new();
        private List<MessageModel> _answers = new();

        public event Action<MessageModel> OnMessageAdded;
        public event Action<string> OnMessageDeleted;

        public List<MessageModel> GetAllMessages()
        {
            return _history;
        }
        
        public void ImportMessages()
        {
            bool isExistHistory = IsExistHistory();
            if (!isExistHistory)
                return;

            var loadedMessages = LoadMessagesFromHistory();
            foreach (var message in loadedMessages)
            {
                _history.Add(new MessageModel(message.Avatar, message.Message, message.Nickname, message.Time,
                    message.MessageID, message.UserID));
            }
        }

        public void ImportAnswers()
        {
            var loadedMessages = LoadDataFromResources();
            foreach (var message in loadedMessages)
            {
                _answers.Add(new MessageModel(message.Avatar, message.Message, message.Nickname, message.Time,
                    message.MessageID, message.UserID));
            }
        }

        public List<MessageInfo> LoadMessagesFromHistory()
        {
            var historyPath = GetHistoryPath();
            using var file = new StreamReader(historyPath, Encoding.UTF8);
            var data = file.ReadToEnd();
            file.Close();
            //var data = File.ReadAllText(historyPath);
            return _dataParser.Deserialize<List<MessageInfo>>(data);
        }


        public void AddNewMessage(MessageModel messageModel)
        {
            _history.Add(messageModel);
            var historyMessageInfo = ConvertModelToInfo(_history);
            var serializedData = _dataParser.Serialize(historyMessageInfo);
            SaveDataToLocalFile(directoryForSaveHistory, historyFileName, serializedData);
            OnMessageAdded?.Invoke(messageModel);
        }

        public List<MessageInfo> ConvertModelToInfo(List<MessageModel> list)
        {
            var historyMessageInfo = new List<MessageInfo>();
            foreach (var message in _history)
            {
                var messageInfo = new MessageInfo();
                messageInfo.MessageID = message.MessageID;
                messageInfo.Message = message.Message;
                messageInfo.Avatar = message.UserModel.AvatarPath;
                messageInfo.Nickname = message.UserModel.Nikname;
                messageInfo.UserID = message.UserModel.UserID;
                messageInfo.Time = message.Time;
                historyMessageInfo.Add(messageInfo);
            }

            return historyMessageInfo;
        }

        public MessageModel GetRandomAnswer()
        {

            var randomAnswer = _answers[UnityEngine.Random.Range(0, _answers.Count)];
            MessageModel messageModel = new MessageModel(randomAnswer.UserModel.AvatarPath, randomAnswer.Message,
                randomAnswer.UserModel.AvatarPath, DateTime.Now.ToString(), Guid.NewGuid().ToString(),
                randomAnswer.UserModel.UserID);

            return messageModel;
        }

        public void DeleteMessage(string messageModelID)
        {
            int index = _history.FindIndex(m => m.MessageID == messageModelID);
            if (index == -1)
                return;

            _history.RemoveAt(index);
            
            var historyMessageInfo = ConvertModelToInfo(_history);
            var serializedData = _dataParser.Serialize(historyMessageInfo);
            SaveDataToLocalFile(directoryForSaveHistory, historyFileName, serializedData);
            OnMessageDeleted?.Invoke(messageModelID);
        }

        public string GetHistoryPath()
        {
            var path = Application.persistentDataPath + "/" + directoryForSaveHistory + "/" + historyFileName;
            return path;
        }

        public void SaveDataToLocalFile(string directory, string fileName, string serializedData)
        {
            var persistanceDataPath = Application.persistentDataPath + "/" + directory;
            var savePath = persistanceDataPath + "/" + fileName;

            if (!Directory.Exists(persistanceDataPath))
                Directory.CreateDirectory(persistanceDataPath);

            using var file = new StreamWriter(savePath, false, Encoding.UTF8);
            file.Write(serializedData);
            file.Close();

        }

        public List<MessageInfo> LoadDataFromResources()
        {
            var textAsset = Resources.Load<TextAsset>(messagesAsset);
            if (textAsset == null)
                return new List<MessageInfo>();

            var chatContent = _dataParser.Deserialize<ChatContent>(textAsset.text);
            return chatContent.messages;
        }

        private bool IsExistHistory()
        { 
            var path = GetHistoryPath();
            return File.Exists(path);
        }

        public void DeleteHistory(string path)
        {
            File.Delete(path);
        }
        
        
    }
}