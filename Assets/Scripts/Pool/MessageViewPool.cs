using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestChat
{
    public class MessageViewPool : MonoBehaviour
    {
        [SerializeField]
        private int _startCapacity;
        [SerializeField]
        private MessageView _myMessagesPrefab;
        [SerializeField]
        private MessageView _answerMessagesPrefab;
        [SerializeField]
        private Transform _content;
        private Stack<MessageView> _inactiveMyMessages = new();
        private Stack<MessageView> _inactiveAnswerMessages = new();
        

        private void Awake()
        {
            MessageView view;
            for (int i = 0; i < _startCapacity; ++i)
            {
                view = Instantiate(_myMessagesPrefab, _content);
                view.gameObject.SetActive(false);
                _inactiveMyMessages.Push(view);
            }
            for (int i = 0; i < _startCapacity; ++i)
            {
                view = Instantiate(_answerMessagesPrefab, _content);
                view.gameObject.SetActive(false);
                _inactiveAnswerMessages.Push(view);
            }
        }

        public bool IsInactiveMyMessagesEmpty()
        {
            return _inactiveMyMessages.Count == 0;
        }
        
        public bool IsInactiveAnswerMessagesEmpty()
        {
            return _inactiveAnswerMessages.Count == 0;
        }

        public MessageView SpawnMyMessage(MessageModel messageModel, bool isDeletingMode)
        {
            var view = _inactiveMyMessages.Pop();
            
            if (!IsInactiveMyMessagesEmpty())
            {
                view.transform.SetAsLastSibling();
                view.ApplyMessage(messageModel, isDeletingMode);
                view.gameObject.SetActive(true);
            }
            else
            {
                view = Instantiate(_myMessagesPrefab, _content);
                view.ApplyMessage(messageModel, isDeletingMode);
            }

            return view;
        }

        public MessageView SpawnAnswerMessage(MessageModel messageModel, bool isDeletingMode)
        {
            var view = _inactiveAnswerMessages.Pop();
            if (!IsInactiveAnswerMessagesEmpty())
            {
                view.transform.SetAsLastSibling();
                view.ApplyMessage(messageModel, isDeletingMode);
                view.gameObject.SetActive(true);
            }
            else
            {
                view = Instantiate(_answerMessagesPrefab, _content);
                view.ApplyMessage(messageModel, isDeletingMode);
            }

            return view;
        }
        public void DespawnMyMessage(MessageView view)
        {
            view.gameObject.SetActive(false);
            _inactiveMyMessages.Push(view);
        }
        
        public void DespawnAnswerMessage(MessageView view)
        {
            view.gameObject.SetActive(false);
            _inactiveAnswerMessages.Push(view);
        }
    }
}