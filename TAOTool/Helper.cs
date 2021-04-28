using System.Configuration;

namespace TAOTool
{
    public static class Helper
    {
        //helper class to setup connectionstring from app.config
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
