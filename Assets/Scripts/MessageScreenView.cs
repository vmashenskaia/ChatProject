using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace TestChat
{
    public class MessageScreenView : MonoBehaviour
    {
        [SerializeField]
        private Button _sendButton;
        [SerializeField]
        private Button _deleteModeButton;
        [SerializeField]
        private TMP_InputField _inputField;
        [SerializeField]
        private MessageView _messageView;
        [SerializeField]
        private MessageView _answerMessageView;
        [SerializeField]
        private Transform _content;
        
        private readonly List<MessageView> _messageViews = new();
        
        private string _myID = "0";//delete later

        public event Action<string> OnMessageSended;

        private void OnEnable()
        {
            _sendButton.onClick.AddListener(OnSendButtonClickHandler);
        }

        private void OnDisable()
        {
            _sendButton.onClick.RemoveListener(OnSendButtonClickHandler);
        }

        public void ApplyMessages(List<MessageModel> messageModels)
        {
            foreach (var messageModel in messageModels)
            {
                MessageView view;
                view = Instantiate(messageModel.UserID == _myID ? _messageView : _answerMessageView, _content);
                view.ApplyMessage(messageModel, false);
                _messageViews.Add(view);
            } 
        }

        public void AddMessage(MessageModel messageModel)
        {
            MessageView view;
            view = Instantiate(messageModel.UserID == _myID ? _messageView : _answerMessageView, _content);
            view.ApplyMessage(messageModel, false);
            _messageViews.Add(view);
        }

        private void OnSendButtonClickHandler()
        {
            OnMessageSended?.Invoke(_inputField.text);
            _inputField.text = "";
        }
    }
}
