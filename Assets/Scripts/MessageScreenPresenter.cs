using System;
using System.Collections.Generic;
using System.Threading;
//using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TestChat
{
    public class MessageScreenPresenter: IDisposable
    {
        private MessageScreenView _view;
        private ChatService _chatService;
        private bool _isDeletingMode;
        
        private string _myID = "0";//delete later
        private string _myNickname = "Viktoria";

        public MessageScreenPresenter(ChatService chatService)
        {
            _chatService = chatService;
        }

        public void LoadAndShowWindow(List<MessageModel> messages)
        {
            _view = GameObject.FindFirstObjectByType<MessageScreenView>();
            _view.ApplyMessages(messages, false);
            _view.OnMessageSended += OnMessageSendedHandler;
            _chatService.OnMessageAdded += OnMessageAddedHandler;
            _view.OnDeleteModeChange += OnDeleteModeChangeHandler;
            _view.OnDeleteViewMessage += OnViewDeleteViewMessageHandler;
            _chatService.OnMessageDeleted += OnMessageDeletedHandler;
        }
        

        private void OnMessageSendedHandler(string text)
        {
            var newMessage = new MessageModel("avatarPath", text, _myNickname, DateTime.Now, Guid.NewGuid().ToString(), _myID);
            _chatService.AddNewMessage(newMessage);
            
        }

        private void OnMessageAddedHandler(MessageModel messageModel)
        {
            _view.AddMessage(messageModel);
        }

        private void OnDeleteModeChangeHandler(bool isDeletingMode)
        {
            _isDeletingMode = !isDeletingMode;
            _view.ChangeDeleteMode(_isDeletingMode);
        }

        private void OnViewDeleteViewMessageHandler(MessageModel messageModel)
        {
            _chatService.DeleteMessage(messageModel.MessageID);
        }

        private void OnMessageDeletedHandler(string messageID)
        {
            _view.DeleteMessage(messageID);
        }

        public void Dispose()
        {
            _view.OnMessageSended -= OnMessageSendedHandler;
            _chatService.OnMessageAdded -= OnMessageAddedHandler;
            _view.OnDeleteModeChange -= OnDeleteModeChangeHandler;
            _view.OnDeleteViewMessage -= OnViewDeleteViewMessageHandler;
            _chatService.OnMessageDeleted -= OnMessageDeletedHandler;
        }
    }
}