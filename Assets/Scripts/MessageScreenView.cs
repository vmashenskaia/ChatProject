using System.Collections.Generic;
using TMPro;
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
        
        
        private string _myID = "0";//delete later

        
        private readonly List<MessageView> _messageViews = new();
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
    }
}
