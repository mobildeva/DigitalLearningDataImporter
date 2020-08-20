using System;
using Renci.SshNet;
using System.IO;
using Serilog;

namespace DigitalLearningIntegration.Application.Utils
{
    public class SftpManager
    {
        public static int Send(string fileName, string host, int port, string username, string password, string sftpPath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException(nameof(fileName));

                if (string.IsNullOrEmpty(host))
                    throw new ArgumentNullException(nameof(host));

                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException(nameof(username));

                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException(nameof(password));

                var connectionInfo = new ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));

                // Upload File
                Log.Debug("Uploading file to SFTP. " + "File: " + fileName);

                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.Connect();

                    if (!string.IsNullOrEmpty(sftpPath) && sftpPath != "\\")
                        sftp.ChangeDirectory(sftpPath);

                    using (var uplfileStream = File.OpenRead(fileName))
                    {
                        sftp.UploadFile(uplfileStream, Path.GetFileName(fileName), true);
                    }
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                return -1;
            }

            Log.Debug("Uploaded file to SFTP correctly. " + "File: " + fileName);

            return 0;
        }

        public static int Get(string fileName, string host, int port, string username, string password, string destPath, string sftpPath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException(nameof(fileName));

                if (string.IsNullOrEmpty(host))
                    throw new ArgumentNullException(nameof(host));

                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException(nameof(username));

                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException(nameof(password));

                if (string.IsNullOrEmpty(destPath))
                    throw new ArgumentNullException(nameof(destPath));

                var connectionInfo = new ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));

                Log.Debug("Downloading file to SFTP. " + "File: " + fileName);

                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.Connect();

                    if (!string.IsNullOrEmpty(sftpPath) && sftpPath != "\\")
                        sftp.ChangeDirectory(sftpPath);

                    var destFullPath = destPath + Path.GetFileName(fileName);

                    using (var uplfileStream = File.Create(destPath))
                    {
                        sftp.DownloadFile(fileName, uplfileStream);
                    }

                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                return -1;
            }

            Log.Debug("Download file from SFTP correctly. " + "File: " + fileName);

            return 0;
        }
    }
}
