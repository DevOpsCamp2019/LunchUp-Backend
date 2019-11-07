namespace LunchUp.WebHost
{
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    /// Database AppSettings Object
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Name of the Database
        /// </summary>
        public string Database { get; set; }
        
        /// <summary>
        /// Hostname of the Database
        /// </summary>
        public string Host { get; set; }
        
        /// <summary>
        /// Port of the Database
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Username of the user of the Database
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Username of the user of the Database
        /// </summary>
        public string Password { get; set; }
    }
}