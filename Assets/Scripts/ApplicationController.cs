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
        [SerializeField]
        private MessageScreenView _view;
        private List<MessageModel> _messageModels;
        private ChatService _chatService;
        private MessageScreenPresenter _messageScreenPresenter;
        
        private const string myID = "0";

        private void OnEnable()
        {
            _chatService.OnMessageAdded += OnMessageAddedHandler;
        }

        private void OnDisable()
        {
            _chatService.OnMessageAdded -= OnMessageAddedHandler;
        }

        private void Awake()
        {
            ScreenInitialization();
            _chatService.ImportMessages();
            _chatService.ImportAnswers();
        }

        private void Start()
        {
            _messageModels = _chatService.GetAllMessages();
            _messageScreenPresenter.LoadAndShowWindow(_messageModels);
        }

        private void ScreenInitialization()
        {
            _messageModels = new();
            _chatService = new();
            _messageScreenPresenter = new MessageScreenPresenter(_chatService, _view);
        }


        private void OnMessageAddedHandler(MessageModel messageModel)
        {
            if (messageModel.UserModel.UserID == myID)
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