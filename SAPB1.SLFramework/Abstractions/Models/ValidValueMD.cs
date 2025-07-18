namespace SAPB1.SLFramework.Abstractions.Models
{
    public class ValidValueMD
    {
        /// <summary>
        /// Sets or returns the valid value. 
        /// Field name: FldValue.
        /// Length: 254 characters.
        /// </summary>
        public required string Value { get; set; }

        /// <summary>
        /// Sets or returns the description of the valid value. 
        /// Field name: Descr.
        /// Length: 254 characters.
        /// </summary>
        public required string Description { get; set; }
    }
}
