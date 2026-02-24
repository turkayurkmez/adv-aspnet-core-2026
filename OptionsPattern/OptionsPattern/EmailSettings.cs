namespace OptionsPattern
{
    public class EmailSettings
    {
        /*
         *  "SmtpServer": "smpt.gmail.com",
    "Port": 567,
    "EnableSSL": true,
    "UserName": "user",
    "Password": "pass"
         */

        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}
