using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BrandSystems.Cryptography;
using AV.Development.Core.User.Interface;
using AV.Development.Core.User;
using System.Net.Mail;
using System.Web;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Xml.Linq;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml;


namespace Development.Utility
{
    public class ClsDb
    {
        string strcon = ConfigurationSettings.AppSettings["conn"].ToString();

        public int CheckValidUserID(string query, CommandType type, string pwd)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("password");
            dt.Columns.Add("saltpassword");

            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand();

                    sqlcmd.CommandType = type;
                    sqlcmd.Connection = sqlcon;
                    sqlcmd.CommandText = query;
                    SqlDataReader dr;
                    dr = sqlcmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        if ((BCrypt.CheckBytePassword(pwd, dr["PasswordSalt"].ToString(), (byte[])dr["Password"]) == true) || pwd.Length == 0)
                        {
                            return (int)dr["Id"];
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }

                }
                catch (Exception ex)
                {
                }



            }
            return 0;
        }

        public IUser GetUserByID(string query, CommandType type)
        {


            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand();

                    sqlcmd.CommandType = type;
                    sqlcmd.Connection = sqlcon;
                    sqlcmd.CommandText = query;
                    SqlDataReader dr;
                    dr = sqlcmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        IUser user = new AV.Development.Core.User.User();
                        user.Email = dr["Email"].ToString();
                        user.FirstName = System.Uri.UnescapeDataString(dr["FirstName"].ToString());
                        user.Id = (int)dr["Id"];
                        user.Image = (dr["Image"] == null ? "" : dr["Image"].ToString());
                        user.LastName = System.Uri.UnescapeDataString(dr["LastName"].ToString());
                        user.Password = (byte[])dr["Password"];
                        user.SaltPassword = dr["PasswordSalt"].ToString();
                        user.UserName = dr["UserName"].ToString();
                        user.Currentwebid = (int)dr["au_current_webid"];
                        user.UserType = (int)dr["au_usertype"];
                        return user;

                    }

                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }

        public IUser UserData(string query, CommandType type, string pwd)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("password");
            dt.Columns.Add("saltpassword");

            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand();

                    sqlcmd.CommandType = type;
                    sqlcmd.Connection = sqlcon;
                    sqlcmd.CommandText = query;
                    SqlDataReader dr;
                    dr = sqlcmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        if ((BCrypt.CheckBytePassword(pwd, dr["PasswordSalt"].ToString(), (byte[])dr["Password"]) == true) || pwd.Length == 0)
                        {
                            IUser user = new AV.Development.Core.User.User();
                            user.Email = dr["Email"].ToString();
                            user.FirstName = System.Uri.UnescapeDataString(dr["FirstName"].ToString());
                            user.Id = (int)dr["Id"];
                            user.Image = (dr["Image"] == null ? "" : dr["Image"].ToString());
                            user.LastName = System.Uri.UnescapeDataString(dr["LastName"].ToString());
                            user.Password = (byte[])dr["Password"];
                            user.SaltPassword = dr["PasswordSalt"].ToString();
                            user.UserName = dr["UserName"].ToString();
                            return user;
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }

        public bool Resetpwdlink(string query, CommandType type, string Guid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = query;
                SqlDataReader dr;
                dr = sqlcmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    return ((int)dr["count"] == 0 ? false : true);

                }

            }


            return false;
        }

        public bool DataExist(string query, CommandType type)
        {
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = query;
                SqlDataReader dr;
                dr = sqlcmd.ExecuteReader();
                if (dr.HasRows)
                {

                    return true;
                }
                //dr.Close();
            }
            return false;
        }

        public int ForgotPwd(string strqry, CommandType type, string emailId, bool isResetpwd)
        {
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = strqry;
                SqlDataReader dr = sqlcmd.ExecuteReader();
                int i = 2;
                if (dr.HasRows)
                {
                    dr.Read();
                    string query = "";
                    int id = (int)dr["Id"];
                    

                    string ToMail = emailId;
                    string ResetPasswordDate;
                    ResetPasswordDate = (DateTime.Now).ToString("yyyy-MM-dd hh:mm");
                    Guid TrackingID;
                    TrackingID = Guid.NewGuid();
                    sqlcon.Close();
                    sqlcon.Open();
                    query = "insert into UM_ResetPasswordTrack(TrackingID,UserID,ResetPasswordDate) values('" + TrackingID + "','" + id + "','" + ResetPasswordDate + "') ";
                    sqlcmd.CommandText = query;
                    sqlcmd.Connection = sqlcon;
                    var s = sqlcmd.ExecuteScalar();

                    string Resetpwd = "Reset password";
                    string Forgetpwd = "Forget password";

                    SmtpClient objsmtp = new SmtpClient();
                    MailMessage _email = new MailMessage();
                    StringBuilder body = new StringBuilder();

                   // _email.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["Email"]);
                    _email.IsBodyHtml = true;
                    _email.To.Add(ToMail);
                    if (isResetpwd == true) { _email.Subject = Resetpwd; }
                    else { _email.Subject = Forgetpwd; }

                    
                    string str = "http" + "://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                    body.Append("<div style='width: 600px;'><p><img style='height:45px;height:10px;' src='" + str + "/img/logo.png'/></p><hr>");
                    body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> Dear Member,</p>");
                    body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> Please click the link below to reset your password. That will take you to a web page where you can create a new password.</p>");
                    body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> Please note that the link will expire in 24hours after this email was sent.</p>");
                    if (isResetpwd)
                    {
                        body.Append(@"<a style='font-size: 10pt;color: #00B0F0;padding-left: 20px;' href='" + str + "/reset-password.html?TrackID=" + TrackingID + "'>Click here to set a new password</a>");
                    }
                    else
                    {
                        body.Append(@"<a style='font-size: 10pt;color: #00B0F0;padding-left: 20px;' href='" + str + "/setnew-password.html?TrackID=" + TrackingID + "'>Click here to set a new password</a>");
                    }
                    body.Append("<hr><span style='font-size: 12px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'>This is a autogenerated mail from Development manager platform.You cannot respond to this mail in any form. If you encounter any problem with Development Manager, please contact the helpdesk administrator.</span><br><br>");
                    body.Append("<span style='font-size: 11px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'><span>Legal Notice:</span><br>");
                    body.Append("<span style='font-size: 11px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'><span>Unless expressly stated otherwise, this message is confidential and may be privileged and it is intended for the addressee(s) only.Access to this e-mail by anyone else is unauthorised. If you are not an addressee, any disclosure or copying of the contents of this e-mail or any action taken (or not taken) in reliance on it is unauthorised and may be unlawful.  If you are not an addressee, please contact the helpdesk administrator. </span></div>");
                    _email.Body = body.ToString();

                    try
                    {
                        objsmtp.Send(_email);

                        return i;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return -1;
        }

        public string GetLoginLogoDetails()
        {
            try
            {

                using (SqlConnection sqlcon = new SqlConnection(strcon))
                {
                    sqlcon.Open();
                    string title = "";

                    string xmlpath = Path.Combine(HttpRuntime.AppDomainAppPath, "AdminSettings.xml");
                    XDocument adminXmlDoc = XDocument.Load(xmlpath);
                    //The Key is root node current Settings
                    string xelementName = "LogoSettings";
                    var xelementFilepath = XElement.Load(xmlpath);
                    var xmlElement = xelementFilepath.Element(xelementName);
                    foreach (var c in xmlElement.Descendants())
                    {
                        if (c.Name == "Title")
                        {
                            title = c.Value;
                            return title;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public string GetClientIntranetUrl()
        {
            try
            {

                using (SqlConnection sqlcon = new SqlConnection(strcon))
                {
                    sqlcon.Open();
                    string title = "";

                    string xmlpath = Path.Combine(HttpRuntime.AppDomainAppPath, "AdminSettings.xml");
                    XDocument adminXmlDoc = XDocument.Load(xmlpath);
                    //The Key is root node current Settings
                    string xelementName = "IntranetLoginUrl";
                    var xelementFilepath = XElement.Load(xmlpath);
                    var xmlElement = xelementFilepath.Element(xelementName);


                    string path = "";
                    if (xmlElement != null)
                        if (xmlElement.Value.Length > 0)
                            path = xmlElement.Value;

                    return path;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public int RegisterData(JObject jobj)
        {
            int userpendingId = 0;
            try
            {


                string saltpwd = "";
                string password = "";
                string query = "";
                bool userexists = false;
                bool userexists1 = false;
                SqlCommand sqlcmd1 = new SqlCommand();
                string query1 = "Select id from UM_User where Email='" + (string)jobj["Email"] + "'";
                userexists = DataExist(query1, CommandType.Text);
                string query2 = "Select id from UM_Pending_User where Email='" + (string)jobj["Email"] + "'";
                userexists1 = DataExist(query2, CommandType.Text);
                using (SqlConnection sqlcon = new SqlConnection(strcon))
                {
                    sqlcon.Open();
                    if (userexists1 == true)
                    {
                        return userpendingId;
                    }
                    if (userexists == false)
                    {
                        password = (string)jobj["Password"];
                        sqlcmd1.Connection = sqlcon;
                        sqlcmd1.CommandType = CommandType.Text;
                        saltpwd = BCrypt.GenerateSalt();
                        sqlcmd1.Parameters.Add("@saltpwd", SqlDbType.VarChar).Value = saltpwd;
                        sqlcmd1.Parameters.Add("@pwd", SqlDbType.VarBinary).Value = BCrypt.HashByteArrayPassword(password, saltpwd);
                        query = "insert into um_pending_user(firstname,lastname,username,password,PasswordSalt,email,title,department) values('" + (string)jobj["FirstName"] + "','" + (string)jobj["LastName"] + "','" + (string)jobj["UserName"] + "', @pwd ,@saltpwd ,'" + (string)jobj["Email"] + "','" + (string)jobj["Title"] + "','" + (string)jobj["Department"] + "');Select Scope_Identity()";
                        sqlcmd1.CommandText = query;
                        userpendingId = Convert.ToInt32(sqlcmd1.ExecuteScalar());

                        string xmlpath = Path.Combine(HttpRuntime.AppDomainAppPath, "AdminSettings.xml");
                        XDocument adminXmlDoc = XDocument.Load(xmlpath);
                        //The Key is root node current Settings
                        string xelementName = "MailConfig";
                        var xelementFilepath = XElement.Load(xmlpath);
                        var xmlElement = xelementFilepath.Element(xelementName);
                        string[] arr = null;
                        foreach (var ele in xmlElement.Descendants())
                        {
                            if (ele.Name.ToString() == "AdminEmailID")
                            {
                                arr = ele.Value.Trim().Split(';');
                            }
                        }

                        //   string[] arr = xmlElement.Value.Trim().Split(';');

                        string Subject = "New User Request";
                        SmtpClient objsmtp = new SmtpClient();
                        MailMessage _email = new MailMessage();
                        StringBuilder body = new StringBuilder();


                        _email.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["Email"]);
                        _email.IsBodyHtml = true;

                        _email.Subject = Subject;

                        string str = "http" + "://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                        body.Append("<div style='width: 600px;'><p><img style='height:45px;height:10px;' src='" + str + "/img/logo.png'/></p><hr>");
                        body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> Dear Member,</p>");
                        body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> You have new user request for approval by " + (string)jobj["FirstName"] + " " + (string)jobj["LastName"] + ".</p>");
                        body.Append("<hr><span style='font-size: 12px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'>This is a autogenerated mail from Brandsystems Planning tool. You can´t respond to this mail in any form. If you encounter any problem with Brandsystems Planning tool, please contact the Helpdesk, at email:myprivacy2015@gmail.com</span><br><br>");
                        body.Append("<span style='font-size: 11px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'><span>Legal Notice:</span><br>");
                        body.Append("<span style='font-size: 11px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'><span>Unless expressly stated otherwise, this message is confidential and may be privileged.It is intended for the addressee(s) only. Access to this e-mail by anyone else is unauthorized. If you are not an addressee, any disclosure or copying of the contents of this e-mail or any action taken (or not taken) in reliance on it is unauthorized and may be unlawful. If you are not an addressee,please inform the sender immediately.</span></div>");
                        _email.Body = body.ToString();
                        foreach (string ToMail in arr)
                        {
                            _email.To.Add(ToMail);
                        }
                        objsmtp.Send(_email);

                        return userpendingId;
                    }
                    else
                    {
                        return userpendingId;
                    }

                }

            }
            catch (Exception ex)
            {
                return -1;
            }
            return userpendingId;
        }


        public DataSet MailData(string query, CommandType type)
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = query;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = sqlcmd;
                dataAdapter.Fill(dataSet);
            }


            return dataSet;
        }

        public int InsertUpdateMailData(string query, CommandType type)
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = query;
                int x = sqlcmd.ExecuteNonQuery();
                return x;
            }

            return 0;

        }


        public DateTime ConvertTimestamp(double timestamp)
        {

            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp);

        }

        public bool IsValidEmail(string emailAddress)
        {
            string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            Match emailAddressMatch = Regex.Match(emailAddress, pattern);
            if (emailAddressMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool[] pwdreset(string strqry, CommandType type, string TrackingID, string Pwd, string oldPassword, bool flag, bool fwdpwd)
        {
            try
            {
                bool[] retunBool = new bool[2];
                bool flagcheck = flag;
                using (SqlConnection sqlcon = new SqlConnection(strcon))
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandType = type;
                    sqlcmd.Connection = sqlcon;
                    sqlcmd.CommandText = "SELECT um.UserID as UserID ,us.Email as Email,us.Password as Password,us.PasswordSalt as PasswordSalt  FROM UM_ResetPasswordTrack um inner join UM_User us on um.UserID=us.ID  where  um.TrackingID='" + TrackingID + "'";
                    SqlDataReader dr;
                    dr = sqlcmd.ExecuteReader();
                    int userid = 0;
                    string emailID = "";
                    {
                        dr.Read();
                        userid = (int)dr["UserID"];
                        emailID = (string)dr["Email"];
                    }




                    if (userid > 0 && (fwdpwd == false ? (BCrypt.CheckBytePassword(oldPassword, dr["PasswordSalt"].ToString(), (byte[])dr["Password"]) == true) : fwdpwd))
                    {
                        sqlcon.Close();
                        sqlcon.Open();
                        sqlcmd.CommandType = type;
                        sqlcmd.Connection = sqlcon;
                        sqlcmd.CommandText = strqry;
                        string saltPwd = BCrypt.GenerateSalt();
                        sqlcmd.Parameters.AddWithValue("@saltpassword", saltPwd);
                        sqlcmd.Parameters.AddWithValue("@password", BCrypt.HashByteArrayPassword(Pwd, saltPwd));
                        sqlcmd.Parameters.AddWithValue("@userID", userid);
                        bool result = (sqlcmd.ExecuteNonQuery() == 1 ? true : false);
                        flagcheck = true;
                        retunBool[0] = flagcheck;
                        retunBool[1] = result;
                        if (result == true)
                        {
                            string Subject = "Password has been reset";
                            SmtpClient objsmtp = new SmtpClient();
                            MailMessage _email = new MailMessage();
                            StringBuilder body = new StringBuilder();
                            string ToMail = emailID;

                            _email.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["Email"]);
                            _email.IsBodyHtml = true;
                            _email.To.Add(ToMail);
                            _email.Subject = Subject;

                            string str = "http" + "://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                            body.Append("<div style='width: 600px;'><p><img style='height:45px;height:10px;' src='" + str + "/img/logo.png'/></p><hr>");
                            body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> Dear Member,</p>");
                            body.Append("<p style='font-size: 13px;color: #4A4A4A;padding-left: 20px;'> The password for your username <FONT color=\"#00B0F0\"> " + emailID + " </FONT> has been successfully reset.</p>");
                            body.Append("<hr><span style='font-size: 12px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'>This is a autogenerated mail from Development manager platform.You cannot respond to this mail in any form. If you encounter any problem with Development Manager, please contact the helpdesk administrator.</span><br><br>");
                            body.Append("<span style='font-size: 11px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'><span>Legal Notice:</span><br>");
                            body.Append("<span style='font-size: 11px;color: #999999;font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;'><span>Unless expressly stated otherwise, this message is confidential and may be privileged and it is intended for the addressee(s) only.Access to this e-mail by anyone else is unauthorised. If you are not an addressee, any disclosure or copying of the contents of this e-mail or any action taken (or not taken) in reliance on it is unauthorised and may be unlawful.  If you are not an addressee, please contact the helpdesk administrator. </span></div>");

                            _email.Body = body.ToString();
                            objsmtp.Send(_email);
                        }

                    }
                }
                return retunBool;
            }
            catch
            {

                throw;
            }
        }

        public DataSet GetUserRegistrationAttributes(string query, CommandType type)
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = query;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = sqlcmd;
                dataAdapter.Fill(dataSet);
            }


            return dataSet;
        }


        public bool UpdateSetpassword(string strqry, CommandType type, string TrackingID, string Pwd, string oldPassword, bool setpwd)
        {
            try
            {

                string query1 = "select count(*) as count from UM_SetUpToolUsersRegister pt WHERE TrackingID='" + TrackingID + "' and setpassword=1";
                using (SqlConnection sqlcon = new SqlConnection(strcon))
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd1 = new SqlCommand();
                    sqlcmd1.CommandType = type;
                    sqlcmd1.Connection = sqlcon;
                    sqlcmd1.CommandText = query1;
                    int count = (int)sqlcmd1.ExecuteScalar();
                    if (count == 1)
                    {
                        return false;
                    }
                    else
                    {
                        sqlcon.Close();
                        sqlcon.Open();
                        SqlCommand sqlcmd = new SqlCommand();
                        sqlcmd.CommandType = type;
                        sqlcmd.Connection = sqlcon;
                        sqlcmd.CommandText = "SELECT um.UserID as UserID ,us.Email as Email,us.Password as Password,us.PasswordSalt as PasswordSalt  FROM UM_SetUpToolUsersRegister um inner join UM_User us on um.UserID=us.ID  where  um.TrackingID='" + TrackingID + "'";
                        SqlDataReader dr;
                        dr = sqlcmd.ExecuteReader();
                        int userid = 0;
                        string emailID = "";
                        if (dr.HasRows)
                        {
                            dr.Read();
                            userid = (int)dr["UserID"];
                            emailID = (string)dr["Email"];
                        }
                        if (userid > 0 && (setpwd == false ? (BCrypt.CheckBytePassword(oldPassword, dr["PasswordSalt"].ToString(),
                            (byte[])dr["Password"]) == true) : setpwd))
                        {
                            sqlcon.Close();
                            sqlcon.Open();
                            sqlcmd.CommandType = type;
                            sqlcmd.Connection = sqlcon;
                            sqlcmd.CommandText = strqry;
                            string saltPwd = BCrypt.GenerateSalt();
                            sqlcmd.Parameters.AddWithValue("@saltpassword", saltPwd);
                            sqlcmd.Parameters.AddWithValue("@password", BCrypt.HashByteArrayPassword(Pwd, saltPwd));
                            sqlcmd.Parameters.AddWithValue("@userID", userid);
                            bool result = (sqlcmd.ExecuteNonQuery() > 0 ? true : false);
                        }
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }


        public bool IsUserExistsForSetpassword(string query, CommandType type)
        {
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = type;
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = query;

                int count = (int)sqlcmd.ExecuteScalar();
                sqlcon.Close();
                if (count == 1)
                {
                    return false;
                }
                return true;
            }
        }



    }
}
