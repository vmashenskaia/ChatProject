using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace TestChat
{
    public class ChatView : MonoBehaviour
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
        private Transform _content;

        private readonly List<MessageView> _messageViews = new();

        public void ApplyMessages(List<MessageModel> messageModels)
        {
            foreach (var messageModel in messageModels)
            {
                var view = Instantiate(_messageView, _content);
                view.ApplyMessage(messageModel, false);
                _messageViews.Add(view);
            } 
        }
    }
}
