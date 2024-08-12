using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class ConfigImportsMenu
    {
        private const string spreadsheetId = "1JTzuJ2HViS-pQmcgdEeIK0nB6xPFGWwIujpHv-vP7qs";
        private const string itemsSheetsName = "ChatConfigs";
        private const string credentialsPath = "chatproject-431905-033d7d352279.json";
        private const string chatContententFileName = "chatContent";
        [MenuItem("ChatProject/ImportConfigs")]
        private static async void LoadConfigs()
        {
            var sheetsImporter = new GoogleSheetsImporter(credentialsPath, spreadsheetId);

            
            var messageParser = new MessageInfoParser();
            var messages = await sheetsImporter.DownloadAndParseSheet<MessageInfo>(itemsSheetsName, messageParser);
            ChatContent chatContent = new();
            chatContent.messages = messages;

            var jisonForSaving = JsonConvert.SerializeObject(chatContent);
            Debug.Log(jisonForSaving);

            var asset = new TextAsset(jisonForSaving);
            AssetDatabase.CreateAsset(asset, "Assets/Resources/messages.asset");
            AssetDatabase.SaveAssets();
        }
    }
}