<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
       // Pkurg.PWorldBPM.Common.Log.Logger.Write(this.GetType(), Pkurg.PWorldBPM.Common.Log.EnumLogLevel.Info, "**Application_Start***");
    }

    void Application_End(object sender, EventArgs e)
    {
        //Pkurg.PWorldBPM.Common.Log.Logger.Write(this.GetType(), Pkurg.PWorldBPM.Common.Log.EnumLogLevel.Info, "**Application_End***");
    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception LastError = Server.GetLastError();
        string errMessage = LastError.ToString();
        string msg = Environment.NewLine + "【Who】:" + this.Request.LogonUserIdentity.Name
                    + Environment.NewLine + "【Where】:" + Request.Url.ToString()
                    + Environment.NewLine + "【What】" + errMessage;

        string position = Pkurg.PWorldBPM.Common.Log.Logger.LogPosition();
        Exception ex = Server.GetLastError();
        Pkurg.PWorldBPM.Common.Log.Logger.Write(this.GetType(), Pkurg.PWorldBPM.Common.Log.EnumLogLevel.Error, msg);

        //开发环境注释，正式环境打开
        //ExceptionHander.GoToErrorPage();
    }

    void Session_Start(object sender, EventArgs e)
    {
        try
        {
            string fromUserCode = HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
           Pkurg.PWorldBPM.Common.Log.Logger.Write(this.GetType(), Pkurg.PWorldBPM.Common.Log.EnumLogLevel.Info, "**Session_Start*** - " + fromUserCode);
            //Delete
            //new SwitchUser().Delete(fromUserCode);
        }
        catch (Exception ex)
        {
            Pkurg.PWorldBPM.Common.Log.Logger.Write(this.GetType(), Pkurg.PWorldBPM.Common.Log.EnumLogLevel.Error, "**Session_Start*** - " + ex.Message);
        }
    }

    void Session_End(object sender, EventArgs e)
    {

    }
       
</script>
