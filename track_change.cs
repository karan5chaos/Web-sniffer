using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Apis.Drive.v3.DriveService;

namespace Searcher_A
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }

    public static class StringExtender
    {
        public static bool Contains(this string s, string str, StringComparison comparer)
        {
            return s.IndexOf(str, comparer) >= 0;
        }
    }

    class track_change
    {
        public static bool changed { get; set; }
        public static bool g_drive_process { get; set; }
        public static bool page_saved { get; set; }


        public static string link_path = Properties.Settings.Default.lnk_path;
        public static string pages_path = Properties.Settings.Default.pages_path;
        public static string message = "";


        public static void Check_folder(string folder_search)
        {
            // string folder_name = "";
            try
            {
                DriveService service = GetService();

                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 10;
                listRequest.Q = "mimeType = 'application/vnd.google-apps.folder' and name = '" + folder_search + "' and trashed=false";
                listRequest.Fields = "nextPageToken, files(id)";

                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        Properties.Settings.Default.d_folder_name = file.Id;
                    }
                }
                else
                {
                    Properties.Settings.Default.d_folder_name = CreateFolder(folder_search);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error while running drive_check service..\n" + exc.Message);

            }

        }

        public static string CreateFolder(string folderName)
        {
                var service = GetService();
                var driveFolder = new Google.Apis.Drive.v3.Data.File();
                driveFolder.Name = folderName;
                driveFolder.MimeType = "application/vnd.google-apps.folder";
                //driveFolder.Parents = new string[] { parent };
                var command = service.Files.Create(driveFolder);
                var file = command.Execute();
                return file.Id;
        }


        public static void UploadFile(Stream file, string fileName, string fileMime, string folder, string fileDescription)
        {
            DriveService service = GetService();

            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Q = "mimeType = 'application/pdf' and name = '" + fileName + "' and trashed=false";
            listRequest.Fields = "nextPageToken, files(name)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
            .Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file1 in files)
                {

                    message = file1.Name + " already uploaded..";
                }
               
                //MessageBox.Show("File already uploaded..");
            }
            else
            {
                var driveFile = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    Description = fileDescription,
                    MimeType = fileMime,
                    //driveFile.DriveId = folder;
                    Parents = new List<string> { folder }
                };

                message = "Uploading " + fileName +" ...";

                var request = service.Files.Create(driveFile, file, fileMime);
                request.SupportsAllDrives = true;
                request.SupportsTeamDrives = true;
                request.Fields = "id";


                var response = request.Upload();
                if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                    throw response.Exception;

            }


            

           // return request.ResponseBody.Id;
        }

        private static DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.A0ARrdaM8oWQJkIYn6HiFnbBrr35xUxxYJY7IO5knh3M8knlfAHboD1SzPBTU8fQ7TMKHjKuzeP6tebnqReXux15VdXKP7BE3n_BxwwdLUm3PF7H5R3XiXETmI9fXJjRXtS-4nqmgKkHN2sU0TWSdT0kfbZ8J1YUNnWUtBVEFTQVRBU0ZRRl91NjFWMWwzWU9jTjFvZjVpR0xHMWZkZ2JiZw0163",
                RefreshToken = "1//04ob0seiPu7hICgYIARAAGAQSNwF-L9IrWu7_2KwHzj9sjgozu2Ky0gQ061Gme-ATkgj7NE9XvZcbb5m9d8IO93ZC_-20KuAzCxo",
            };


            var applicationName = "fileupload"; // Use the name of the project in Google Cloud
            var username = "karan@nextschool.org"; // Use your email


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "180144865796-le5qsum0oq9pfp9ch3if5cbtjmik6b4a.apps.googleusercontent.com",
                    ClientSecret = "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"
                },

                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            return service;
        }
    }
}
