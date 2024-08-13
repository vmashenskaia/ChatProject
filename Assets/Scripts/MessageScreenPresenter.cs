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
        
        private string _myID = "0";//delete later
        private string _myNickname = "Viktoria";

        public MessageScreenPresenter(ChatService chatService)
        {
            _chatService = chatService;
        }

        public void LoadAndShowWindow(List<MessageModel> messages)
        {
            _view = GameObject.FindFirstObjectByType<MessageScreenView>();
            _view.ApplyMessages(messages);
            _view.OnMessageSended += OnMessageSendedHandler;
            _chatService.OnMessageAdded += OnMessageAddedHandler;
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

        //private async Unitask AddMessage(string text, CancellationToken token)
        //{
        //}
        public void Dispose()
        {
            _view.OnMessageSended -= OnMessageSendedHandler;
            _chatService.OnMessageAdded -= OnMessageAddedHandler;
        }
    }
}