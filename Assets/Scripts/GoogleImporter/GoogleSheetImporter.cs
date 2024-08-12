using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace GoogleSpreadsheets  
{
    public class GoogleSheetsImporter
    {
        private readonly SheetsService _service;
        private readonly string _spreadsheetId;
        
        private readonly List<string> _headers = new();

        public GoogleSheetsImporter(string credentialsPath, string spreadsheetId)
        {
            _spreadsheetId = spreadsheetId;
        
            GoogleCredential credential;
            using (var stream = new System.IO.FileStream(credentialsPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
            }
        
            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }
    
        public async Task<List<T>> DownloadAndParseSheet<T>(string sheetName, IGoogleSheetParser parser)
        where T : new()
        {
            Debug.Log($"Starting downloading sheet (${sheetName})...");

            // Define the range of the table to download
            var range = $"{sheetName}!A1:Z";
            // Make the request to Google Sheets API
            var request = _service.Spreadsheets.Values.Get(_spreadsheetId, range);
                var list = new List<T>();

            ValueRange response;
            try
            {
                response = await request.ExecuteAsync();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error retrieving Google Sheets data: {e.Message}");
                return list;
            }

            // Parse the received data
            if (response != null && response.Values != null)
            {
                var tableArray = response.Values;
                Debug.Log($"Sheet downloaded successfully: {sheetName}. Parsing started.");

                
                var firstRow = tableArray[0];
                foreach (var cell in firstRow)
                {
                    _headers.Add(cell.ToString());
                }

                var rowsCount = tableArray.Count;
                for (var i = 1; i < rowsCount; i++)
                    list.Add((T) Convert.ChangeType(parser.Parse(_headers, tableArray[i]), typeof(T)));
                
                Debug.Log($"Sheet parsed successfully.");
            }
            else
            {                                                   
                Debug.LogWarning("No data found in Google Sheets.");
            }

            return list;
        }
    }
}
