namespace SmartBatteryTesterWebApp.BLL.Infrastructure
{
    public class ValidationExeption : Exception
    {
        public string Property { get; private set; }

        public ValidationExeption(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
