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
        private MessageView _messageViewPrefab;
        [SerializeField]
        private MessageView _answerMessageViewPrefab;
        [SerializeField]
        private Transform _content;
        private bool _isDeletingMode;
        
        private readonly Dictionary<string, MessageView> _dictionaryMessageViews = new();
        
        private string _myID = "0";//delete later

        public event Action<string> OnMessageSended;
        public event Action<bool> OnDeleteModeChange;
        public event Action<MessageModel> OnDeleteViewMessage;

        private void OnEnable()
        {
            _sendButton.onClick.AddListener(OnSendButtonClickHandler);
            _deleteModeButton.onClick.AddListener(OnDeleteModeButtonHandler);
        }

        private void OnDisable()
        {
            _sendButton.onClick.RemoveListener(OnSendButtonClickHandler);
            _deleteModeButton.onClick.RemoveListener(OnDeleteModeButtonHandler);
        }

        public void ApplyMessages(List<MessageModel> messageModels, bool isDeletingMode)
        {
            _isDeletingMode = isDeletingMode;
            foreach (var messageModel in messageModels)
            {
                AddMessage(messageModel);
            } 
        }
        

        public void AddMessage(MessageModel messageModel)
        {
            MessageView view;
            view = Instantiate(messageModel.UserID == _myID ? _messageViewPrefab : _answerMessageViewPrefab, _content);
            view.ApplyMessage(messageModel, _isDeletingMode);
            _dictionaryMessageViews.Add(messageModel.MessageID, view);
            view.OnDeleteMessage += OnDeleteMessageHandler;
        }

        public void ChangeDeleteMode(bool isDeletemode)
        {
            _isDeletingMode = isDeletemode;
            foreach (var messageView in _dictionaryMessageViews.Values)
            {
                messageView.ToggleDeleteMode(_isDeletingMode);
            }
        }

        public void DeleteMessage(string messageID)
        {
            var messageView = _dictionaryMessageViews[messageID];
            messageView.OnDeleteMessage -= OnDeleteMessageHandler;
            Destroy(messageView.gameObject);
            _dictionaryMessageViews.Remove(messageID);
        }

        private void OnSendButtonClickHandler()
        {
            OnMessageSended?.Invoke(_inputField.text);
            _inputField.text = "";
        }

        private void OnDeleteModeButtonHandler()
        {
            OnDeleteModeChange?.Invoke(_isDeletingMode);
        }

        private void OnDeleteMessageHandler(MessageModel messageModel)
        {
            OnDeleteViewMessage?.Invoke(messageModel);
        }
    }
}
