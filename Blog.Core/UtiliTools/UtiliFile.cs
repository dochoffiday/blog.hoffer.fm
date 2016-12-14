using System;
using System.Web;
using System.IO;
using Microsoft.Win32;

namespace AJ.UtiliTools
{
    public class UtiliFile
    {
        public static void WriteToFile(String path, String text, bool append)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            String content = Helper.IIF<String>(append, ReadFile(path), "");

            using (TextWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(content);
                writer.Write(text);
            }
        }

        public static String ReadFile(String path)
        {
            String text = "";

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    text = reader.ReadToEnd();
                }
            }
            catch { }

            return text;
        }

        public static void OutputCsv(String filename, String text)
        {
            #region Configure CSV File

            if (!filename.EndsWith(".csv"))
            {
                filename = filename + ".csv";
            }

            string attachment = "attachment; filename=" + filename;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");

            #endregion

            HttpContext.Current.Response.Write(text);

            #region Close CSV

            HttpContext.Current.Response.End();

            #endregion
        }

        public static void OutputXls(String filename, String text)
        {
            #region Configure XLS File

            if (!filename.EndsWith(".xls"))
            {
                filename = filename + ".xls";
            }

            string attachment = "attachment; filename=" + filename;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Pragma", "public");

            #endregion

            HttpContext.Current.Response.Write(text);

            #region Close XLS

            HttpContext.Current.Response.End();

            #endregion
        }

        public static void OutputText(String filename, String text)
        {
            #region Configure TXT File

            if (!filename.EndsWith(".txt"))
            {
                filename = filename + ".txt";
            }

            string attachment = "attachment; filename=" + filename;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.AddHeader("Pragma", "public");

            #endregion

            HttpContext.Current.Response.Write(text);

            #region Close TXT

            HttpContext.Current.Response.End();

            #endregion
        }

        private void OutputFile(String path, String filename, String extension)
        {
            FileInfo fi = new FileInfo(filename);

            if (fi.Extension.IsNullOrEmpty())
            {
                if (!extension.StartsWith("."))
                    extension = "." + extension;

                filename = filename + extension;
            }

            OutputFile(path + filename, filename);
        }

        public static void OutputFile(String fullpath, String filename)
        {
            FileStream liveStream = new FileStream(fullpath, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[(int)liveStream.Length]; liveStream.Read(buffer, 0, (int)liveStream.Length);

            liveStream.Close();

            HttpResponse Response = HttpContext.Current.Response;

            FileInfo fi = new FileInfo(filename);

            Response.Clear();
            Response.ContentType = GetFileContentType(fi.Extension);

            Response.AddHeader("Content-Length", buffer.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

            Response.BinaryWrite(buffer);

            Response.End();
        }

        public static string GetFileContentType(string fileExtension)
        {
            //set the default content-type  
            String DEFAULT_CONTENT_TYPE = "application/unknown";

            RegistryKey regKey, fileExtKey;
            string fileContentType;

            try
            {
                //look in HKCR  
                regKey = Registry.ClassesRoot;

                //look for extension  
                fileExtKey = regKey.OpenSubKey(fileExtension);

                //retrieve Content Type value  
                fileContentType = fileExtKey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();

                //cleanup  
                fileExtKey = null;
                regKey = null;
            }
            catch
            {
                fileContentType = DEFAULT_CONTENT_TYPE;
            }

            //print the content type  
            return fileContentType;
        }
    }
}