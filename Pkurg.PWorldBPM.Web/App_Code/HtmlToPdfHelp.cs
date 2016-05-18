using System;
using System.Diagnostics;
using System.Security;
using System.Web;

/// <summary>
/// Summary description for HtmlToPdfHelp
/// </summary>
public class HtmlToPdfHelp
{
	public HtmlToPdfHelp()
	{
		
	}
    public static bool HtmlToPdf(string url, string pdfFileName,string pdfFilePath,string pageAccount,string pagePassword)
    {
        bool isScucess = false;
        try
        {
            string filename = pdfFilePath + "\\" + pdfFileName + ".pdf";
            string dllstr = HttpContext.Current.Server.MapPath(@"/") + "wkhtmltopdf\\bin\\wkhtmltopdf.exe";

            Process p = new System.Diagnostics.Process();

            p.StartInfo.FileName = dllstr;
            //p.StartInfo.UserName = "administrator"; //用户名
            //p.StartInfo.Password = StringToSecureString("password01!");//用户密码


            // p.StartInfo.Arguments = " " + "\"" + url + "\"" + " " + "\"" + filename + "\"";
            p.StartInfo.Arguments = string.Format("  \"{0}\" \"{1}\" --password \"{2}\" --username \"{3}\"", url, filename, pagePassword, pageAccount);
            p.StartInfo.UseShellExecute = false; // needs to be false in order to redirect output 
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true; // redirect all 3, as it should be all 3 or none 
            p.StartInfo.WorkingDirectory = pdfFilePath;

            p.Start();

            // read the output here... 
            string output = p.StandardOutput.ReadToEnd();

            // ...then wait n milliseconds for exit (as after exit, it can't read the output) 
            p.WaitForExit(6000);

            // read the exit code, close process 
            int returnCode = p.ExitCode;
            p.Close();

            // if 0 or 2, it worked (not sure about other values, I want a better way to confirm this) 
             //;
            if (returnCode == 0 || returnCode == 2)
            {
                isScucess = true;
            }

        }
        catch 
        {

        }
        return isScucess;
    }
    public static  SecureString StringToSecureString(String str)
    {
        SecureString secureStr = new SecureString();
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            secureStr.AppendChar(chars[i]);
        }
        return secureStr;
    }

}