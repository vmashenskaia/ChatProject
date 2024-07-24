using System.Collections.Generic;
using UnityEngine;

namespace TestChat
{
    public class ChatPresenter
    {
        private ChatView _view;
        
        public void LoadAndShowWindow(List<MessageModel> messages)
        {
            _view = GameObject.FindFirstObjectByType<ChatView>();
            _view.ApplyMessages(messages);
        }
    }
}