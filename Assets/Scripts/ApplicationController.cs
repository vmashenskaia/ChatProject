using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace TestChat
{
    public class ApplicationController: MonoBehaviour
    {
        private List<MessageModel> _messageModels = new();
        private ChatService _chatService = new();
        
        private string _myID = "0";//delete later

        private void OnEnable()
        {
            _chatService.OnMessageAdded += OnMessageAddedHandler;
        }

        private void OnDisable()
        {
            _chatService.OnMessageAdded -= OnMessageAddedHandler;
        }

        private void Start()
        {
            _chatService.ImportMessages();
            _messageModels = _chatService.GetAllMessages();
            var presenter = new MessageScreenPresenter(_chatService);
            presenter.LoadAndShowWindow(_messageModels);
        }


        private void OnMessageAddedHandler(MessageModel messageModel)
        {
            if (messageModel.UserID == _myID)
                AddMessage(destroyCancellationToken).Forget();
        }

        private async UniTask AddMessage(CancellationToken token)
        {  
            var randomAmount = Random.Range(1, 7);
            for (int i = 0; i < randomAmount  ; ++i)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: token);
               _chatService.AddNewMessage(_chatService.GetRandomAnswer()); 
            }
        }
    }
}