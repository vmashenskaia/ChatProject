using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TestChat
{
    public class MessageView : MonoBehaviour
    {
        [SerializeField]
        private Image _avatar;
        [SerializeField]
        private Button _deleteButton;
        [SerializeField]
        private TMP_Text _messageLabel;
        [SerializeField]
        private TMP_Text _nicknameLabel;
        [SerializeField]
        private TMP_Text _timeLabel;
        public event Action<MessageModel> OnDeleteMessage;
        private MessageModel _messageModel;

        private void OnEnable()
        {
            _deleteButton.onClick.AddListener(OnDeleteMessageHandler);
        }

        private void OnDisable()
        {
            _deleteButton.onClick.RemoveListener(OnDeleteMessageHandler);
        }
        

        public void ApplyMessage(MessageModel messageModel, bool isActiveDeleteMode)
        {
            _avatar.sprite = Resources.Load<Sprite>(messageModel.AvatarPath);
            _messageLabel.text = messageModel.Message;
            _nicknameLabel.text = messageModel.Nickname;
            _timeLabel.text = messageModel.Time;
            _messageModel = messageModel;
            ToggleDeleteMode(isActiveDeleteMode);
            var cv = this.AddComponent<CanvasGroup>();
            cv.alpha = 0;
            cv.DOFade(1, (float) 0.5).OnComplete(() =>
            {
                var canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                    Destroy(canvasGroup);
            });
        }

        public void ToggleDeleteMode(bool active)
        {
            _deleteButton.gameObject.SetActive(active);
        }

        private void OnDeleteMessageHandler()
        {
            OnDeleteMessage?.Invoke(_messageModel);
        }
    }
}