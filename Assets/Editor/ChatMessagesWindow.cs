using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpreadsheets;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace TestChat.Editor
{
    public class ChatMessagesWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private List<SerializedObject> _objs;
        
        private List<MessageInfo> _messageInfos;
        
        [MenuItem("ChatProject/Message Editor")]
        public static void ShowWindow()
        {
            GetWindow<ChatMessagesWindow>("ChatMessages");
        }

        private void OnGUI()
        {
            if (_messageInfos == null)
            {
                var chatService = new ChatService();
                _messageInfos = chatService.LoadMessages();
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            var i = 0;
            while (i < _messageInfos.Count)
            {
                var messageInfo = _messageInfos[i];
                GUILayout.BeginHorizontal();
                GUILayout.Label("# " + i);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Remove message"))
                {
                    _messageInfos.RemoveAt(i);
                    GUILayout.EndHorizontal();
                    continue;
                }
                GUILayout.EndHorizontal();
                ShowMessage(messageInfo);
                GUILayout.Space(20);
                ++i;
            }

            if (GUILayout.Button("Add Message"))
                _messageInfos.Add(new MessageInfo());

            EditorGUILayout.EndScrollView();
            
            GUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                ChatContent chatContent = new();
                chatContent.messages = _messageInfos;
                var jisonForSaving = JsonConvert.SerializeObject(chatContent);
                var asset = new TextAsset(jisonForSaving);
                AssetDatabase.CreateAsset(asset, "Assets/Resources/messages.asset");
                AssetDatabase.SaveAssets();
            }
        }
        private void ShowMessage(MessageInfo messageInfo)
        {
            messageInfo.Avatar = EditorGUILayout.TextField(nameof(messageInfo.Avatar), messageInfo.Avatar);
            messageInfo.Message = EditorGUILayout.TextField(nameof(messageInfo.Message), messageInfo.Message);
            messageInfo.Nickname = EditorGUILayout.TextField(nameof(messageInfo.Nickname), messageInfo.Nickname);
            messageInfo.Time = EditorGUILayout.TextField(nameof(messageInfo.Time), messageInfo.Time);
            messageInfo.MessageID = EditorGUILayout.TextField(nameof(messageInfo.MessageID), messageInfo.MessageID);
            messageInfo.UserID = EditorGUILayout.TextField(nameof(messageInfo.UserID), messageInfo.UserID);
        }
    }
}
