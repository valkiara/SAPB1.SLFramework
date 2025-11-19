namespace SAPB1.SLFramework.Settings
{
    /// <summary>
    /// Configuration settings for SAP Business One Service Layer connection.
    /// Bind from configuration section "SapB1".
    /// </summary>
    public class SapB1Settings
    {
        /// <summary>
        /// The base URL of the Service Layer, e.g. "https://myserver:50000/b1s/v1".
        /// </summary>
        public string ServiceLayerUrl { get; set; } = string.Empty;

        /// <summary>
        /// The target company database name (schema) in SAP B1.
        /// </summary>
        public string CompanyDB { get; set; } = string.Empty;

        /// <summary>
        /// The user name for Service Layer authentication.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// The password for Service Layer authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// (Optional) The language code for the Service Layer session (e.g., 1033 for English).
        /// </summary>
        public int? Language { get; set; }

        /// <summary>
        /// Number of retry attempts for transient Service Layer calls.
        /// </summary>
        public int NumberOfAttempts { get; set; } = 3;

        public Dictionary<string, string>? ExtraHeaders { get; set; }
    }
}
