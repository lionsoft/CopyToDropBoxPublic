using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CopyToDropBoxPublic.Properties;

namespace CopyToDropBoxPublic
{
    internal class Program
    {
        const string FileNameTemplate = "yyyy-MM-dd_HHmmss";
        const string DropboxPublicLinkTemplate = "http://dl.dropbox.com/u/{0}/{1}";
        private static void Message(string text, ToolTipIcon icon, string title = null, int delay = 2000)
        {
            var notifyIcon = new NotifyIcon {Icon = SystemIcons.Information, Visible = true};
            notifyIcon.ShowBalloonTip(delay, title ?? "Copy to Dropbox", text, icon);
            Thread.Sleep(delay);
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }

        private static void Info(string text)
        {
            Message(text, ToolTipIcon.Info);
        }

        private static void Error(string text)
        {
            Message(text, ToolTipIcon.Error, null, 6000);
        }
        private static void ShowHelp(string title)
        {
            Message("Usage:\r\n> CopyToDropBoxPublic <imagefilepath>", ToolTipIcon.Info, title, 10000);
        }

        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    ShowHelp("File name must be set as a first program argument.");
                    return;
                }
                var publicFolder = Settings.Default.PublicFolder;
                var userId = Settings.Default.UserId;
                var subFolder = Settings.Default.SubFolder;
                if (string.IsNullOrEmpty(publicFolder) || !Directory.Exists(publicFolder) || string.IsNullOrEmpty(userId))
                {
                    ShowHelp("User name and public dropbox folder must be set in .config file.");
                    return;
                }
                var fileName = Path.GetFileName(args[0]);
                if (fileName == null)
                {
                    ShowHelp("File name must be set.");
                    return;
                }
                var targetFolder = Path.Combine(publicFolder, subFolder);
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);
                var extension = Path.GetExtension(fileName);
                if (extension.ToUpper() == ".BMP")
                {
                    // Convert .bmp file to .png
                    fileName = DateTime.Now.ToString(FileNameTemplate) + ".png";
                    new Bitmap(args[0]).Save(Path.Combine(targetFolder, fileName), ImageFormat.Png);
                }
                else
                {
                    var destFileName = Path.Combine(targetFolder, fileName);
                    File.Copy(args[0], destFileName, true);
                }
                var text = string.IsNullOrEmpty(publicFolder)
                    ? string.Format(DropboxPublicLinkTemplate, userId, fileName)
                    : string.Format(DropboxPublicLinkTemplate + "/{2}", userId, subFolder, fileName);
                Clipboard.SetText(text);
                Info(text);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

    }
}
