//using System;
//using System.Collections.Generic;
//using System.Web;
//using System.Text;
//using System.Data.SqlClient;

//namespace AJ.UtiliTools
//{
//    public class UtiliBug
//    {
//        public static void LogJavascriptError(String description, String page, String line, String message)
//        {
//            if (SetupUtiliBug())
//            {
//                List<SqlParameter> prams = new List<SqlParameter>();

//                prams.AddParameter("@Url", HttpContext.Current.Request.Url.PathAndQuery);
//                prams.AddParameter("@UrlReferrer", HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.PathAndQuery);
//                prams.AddParameter("@UserName", UserTility.CurrentUserName);
//                prams.AddParameter("@UserIP", UserTility.CurrentUserIP);
//                prams.AddParameter("@Date", DateTime.UtcNow);
//                prams.AddParameter("@Description", description);
//                prams.AddParameter("@Page", page);
//                prams.AddParameter("@Line", line);
//                prams.AddParameter("@Message", message);

//                #region Build SQL

//                StringBuilder sqlOne = new StringBuilder("INSERT INTO BUG_JavascriptError (");
//                StringBuilder sqlTwo = new StringBuilder(" VALUES (");

//                sqlOne.Append("Url,");
//                sqlTwo.Append("@Url,");

//                sqlOne.Append("UrlReferrer,");
//                sqlTwo.Append("@UrlReferrer,");

//                sqlOne.Append("UserName,");
//                sqlTwo.Append("@UserName,");

//                sqlOne.Append("UserIP,");
//                sqlTwo.Append("@UserIP,");

//                sqlOne.Append("Date,");
//                sqlTwo.Append("@Date,");

//                sqlOne.Append("Description,");
//                sqlTwo.Append("@Description,");

//                sqlOne.Append("Page,");
//                sqlTwo.Append("@Page,");

//                sqlOne.Append("Line,");
//                sqlTwo.Append("@Line,");

//                sqlOne.Append("Message) ");
//                sqlTwo.Append("@Message)");

//                #endregion

//                DB.ExecuteSQL("{0}{1}".F(sqlOne.ToString(), sqlTwo.ToString()), prams.ToArray());
//            }
//        }

//        public static void LogError(Exception ex)
//        {
//            if (SetupUtiliBug())
//            {
//                try
//                {
//                    List<SqlParameter> prams = new List<SqlParameter>();

//                    prams.AddParameter("@Url", HttpContext.Current.Request.Url.PathAndQuery);
//                    prams.AddParameter("@UrlReferrer", HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.PathAndQuery);
//                    prams.AddParameter("@UserName", UserTility.CurrentUserName);
//                    prams.AddParameter("@UserIP", UserTility.CurrentUserIP);
//                    prams.AddParameter("@Date", DateTime.UtcNow);
//                    prams.AddParameter("@ErrorMessage", ex.Message);
//                    prams.AddParameter("@ErrorSource", ex.Source);
//                    prams.AddParameter("@ErrorStackTrace", ex.StackTrace);
//                    prams.AddParameter("@ErrorTargetSite", ex.TargetSite.ToString());

//                    #region Build SQL

//                    StringBuilder sqlOne = new StringBuilder("INSERT INTO BUG_Error (");
//                    StringBuilder sqlTwo = new StringBuilder(" VALUES (");

//                    sqlOne.Append("Url,");
//                    sqlTwo.Append("@Url,");

//                    sqlOne.Append("UrlReferrer,");
//                    sqlTwo.Append("@UrlReferrer,");

//                    sqlOne.Append("UserName,");
//                    sqlTwo.Append("@UserName,");

//                    sqlOne.Append("UserIP,");
//                    sqlTwo.Append("@UserIP,");

//                    sqlOne.Append("Date,");
//                    sqlTwo.Append("@Date,");

//                    sqlOne.Append("ErrorMessage,");
//                    sqlTwo.Append("@ErrorMessage,");

//                    sqlOne.Append("ErrorSource,");
//                    sqlTwo.Append("@ErrorSource,");

//                    sqlOne.Append("ErrorStackTrace,");
//                    sqlTwo.Append("@ErrorStackTrace,");

//                    sqlOne.Append("ErrorTargetSite) ");
//                    sqlTwo.Append("@ErrorTargetSite)");

//                    #endregion

//                    DB.ExecuteSQL("{0}{1}".F(sqlOne.ToString(), sqlTwo.ToString()), prams.ToArray());
//                }
//                catch { }
//            }
//        }

//        public static void Debug(String group, String message)
//        {
//            if (SetupUtiliBug())
//            {
//                if (Helper.Parse<bool>(UtiliSetting.AppSetting("IsDebugMode")))
//                {
//                    List<SqlParameter> prams = new List<SqlParameter>();

//                    prams.AddParameter("@Url", HttpContext.Current.Request.Url.PathAndQuery);
//                    prams.AddParameter("@UrlReferrer", HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.PathAndQuery);
//                    prams.AddParameter("@UserName", UserTility.CurrentUserName);
//                    prams.AddParameter("@UserIP", UserTility.CurrentUserIP);
//                    prams.AddParameter("@Date", DateTime.UtcNow);
//                    prams.AddParameter("@Group", group);
//                    prams.AddParameter("@Message", message);

//                    #region Build SQL

//                    StringBuilder sqlOne = new StringBuilder("INSERT INTO BUG_Debug (");
//                    StringBuilder sqlTwo = new StringBuilder(" VALUES (");

//                    sqlOne.Append("Url,");
//                    sqlTwo.Append("@Url,");

//                    sqlOne.Append("UrlReferrer,");
//                    sqlTwo.Append("@UrlReferrer,");

//                    sqlOne.Append("UserName,");
//                    sqlTwo.Append("@UserName,");

//                    sqlOne.Append("UserIP,");
//                    sqlTwo.Append("@UserIP,");

//                    sqlOne.Append("[Date],");
//                    sqlTwo.Append("@Date,");

//                    sqlOne.Append("[Group],");
//                    sqlTwo.Append("@Group,");

//                    sqlOne.Append("Message) ");
//                    sqlTwo.Append("@Message)");

//                    #endregion

//                    DB.ExecuteSQL("{0}{1}".F(sqlOne.ToString(), sqlTwo.ToString()), prams.ToArray());
//                }
//            }
//        }

//        public static bool SetupUtiliBug()
//        {
//            try
//            {
//                if (DB.ExecuteScalar<int>("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BUG_Error') SELECT '1' ELSE SELECT '0'") == 0)
//                {
//                    StringBuilder sql = new StringBuilder();

//                    sql.AppendLine("CREATE TABLE [dbo].[BUG_Error](");
//                    sql.AppendLine("[ErrorID] [int] IDENTITY(1,1) NOT NULL,");
//                    sql.AppendLine("[Url] [nvarchar](512) NULL,");
//                    sql.AppendLine("[UrlReferrer] [nvarchar](512) NULL,");
//                    sql.AppendLine("[UserName] [nvarchar](256) NULL,");
//                    sql.AppendLine("[UserIP] [nvarchar](20) NULL,");
//                    sql.AppendLine("[Date] [datetime] NOT NULL,");
//                    sql.AppendLine("[ErrorMessage] [nvarchar](512) NULL,");
//                    sql.AppendLine("[ErrorSource] [nvarchar](256) NULL,");
//                    sql.AppendLine("[ErrorStackTrace] [text] NULL,");
//                    sql.AppendLine("[ErrorTargetSite] [nvarchar](256) NULL,");
//                    sql.AppendLine("CONSTRAINT [PK_BUG_Error] PRIMARY KEY CLUSTERED ");
//                    sql.AppendLine("(");
//                    sql.AppendLine("[ErrorID] ASC");
//                    sql.AppendLine(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
//                    sql.AppendLine(")");// ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");=

//                    DB.ExecuteSQL(sql.ToString());
//                }
//                if (DB.ExecuteScalar<int>("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BUG_Debug') SELECT '1' ELSE SELECT '0'") == 0)
//                {
//                    StringBuilder sql = new StringBuilder();

//                    sql.AppendLine("CREATE TABLE [dbo].[BUG_Debug](");
//                    sql.AppendLine("[DebugID] [int] IDENTITY(1,1) NOT NULL,");
//                    sql.AppendLine("[Url] [nvarchar](512) NULL,");
//                    sql.AppendLine("[UrlReferrer] [nvarchar](512) NULL,");
//                    sql.AppendLine("[UserName] [nvarchar](256) NULL,");
//                    sql.AppendLine("[UserIP] [nvarchar](20) NULL,");
//                    sql.AppendLine("[Date] [datetime] NOT NULL,");
//                    sql.AppendLine("[Group] [nvarchar](256) NULL,");
//                    sql.AppendLine("[Message] [nvarchar](512) NULL,");
//                    sql.AppendLine("CONSTRAINT [PK_BUG_Debug] PRIMARY KEY CLUSTERED ");
//                    sql.AppendLine("(");
//                    sql.AppendLine("[DebugID] ASC");
//                    sql.AppendLine(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
//                    sql.AppendLine(")");// ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

//                    DB.ExecuteSQL(sql.ToString());
//                }
//                if (DB.ExecuteScalar<int>("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BUG_JavascriptError') SELECT '1' ELSE SELECT '0'") == 0)
//                {
//                    StringBuilder sql = new StringBuilder();

//                    sql.AppendLine("CREATE TABLE [dbo].[BUG_JavascriptError](");
//                    sql.AppendLine("[JavascriptErrorID] [int] IDENTITY(1,1) NOT NULL,");
//                    sql.AppendLine("[Url] [nvarchar](512) NULL,");
//                    sql.AppendLine("[UrlReferrer] [nvarchar](512) NULL,");
//                    sql.AppendLine("[UserName] [nvarchar](256) NULL,");
//                    sql.AppendLine("[UserIP] [nvarchar](20) NULL,");
//                    sql.AppendLine("[Date] [datetime] NOT NULL,");
//                    sql.AppendLine("[Description] [nvarchar](256) NULL,");
//                    sql.AppendLine("[Page] [nvarchar](512) NULL,");
//                    sql.AppendLine("[Line] [nvarchar](10) NULL,");
//                    sql.AppendLine("[Message] [nvarchar](256) NULL,");
//                    sql.AppendLine("CONSTRAINT [PK_BUG_JavascriptError] PRIMARY KEY CLUSTERED ");
//                    sql.AppendLine("(");
//                    sql.AppendLine("[JavascriptErrorID] ASC");
//                    sql.AppendLine(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
//                    sql.AppendLine(")");// ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

//                    DB.ExecuteSQL(sql.ToString());
//                }

//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}