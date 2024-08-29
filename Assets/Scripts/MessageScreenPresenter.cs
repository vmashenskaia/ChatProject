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
        
        private const string myID = "0";
        private const string myNickname = "Viktoria";
        private const string myAvatarPath = "default";

        public MessageScreenPresenter(ChatService chatService, MessageScreenView view)
        {
            _chatService = chatService;
            _view = view;
        }

        public void LoadAndShowWindow(List<MessageModel> messages)
        {
            _view.ApplyMessages(messages, false);
            _view.OnMessageSended += OnMessageSendedHandler;
            _chatService.OnMessageAdded += OnMessageAddedHandler;
            _view.OnDeleteModeChange += OnDeleteModeChangeHandler;
            _view.OnDeleteViewMessage += OnViewDeleteViewMessageHandler;
            _chatService.OnMessageDeleted += OnMessageDeletedHandler;
        }
        

        private void OnMessageSendedHandler(string text)
        {
            var newMessage = new MessageModel(myAvatarPath, text, myNickname, DateTime.Now.ToString(), Guid.NewGuid().ToString(), myID);
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