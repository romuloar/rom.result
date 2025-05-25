namespace Rom.Result.Domain
{
    /// <summary>
    /// Represents the type of result for an operation.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Indicates a successful operation.
        /// </summary>
        Success,

        /// <summary>
        /// Indicates an error occurred during the operation.
        /// </summary>
        Error,

        /// <summary>
        /// Indicates informational messages related to the operation.
        /// </summary>
        Info,

        /// <summary>
        /// Indicates a warning related to the operation, which may not be critical but should be noted.
        /// </summary>
        Warning
    }
}
