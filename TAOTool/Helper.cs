using System.Configuration;

namespace TAOTool
{
    public static class Helper
    {
        //helper class to setup connectionstring from app.config
        public static string CnnVal(string name= "tao1DB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
