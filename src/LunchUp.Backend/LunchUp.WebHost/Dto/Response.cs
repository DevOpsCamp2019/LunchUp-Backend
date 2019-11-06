namespace LunchUp.WebHost.Dto
{
    /// <summary>
    ///     The object representing the response to a suggestion
    /// </summary>
    public class Response
    {
        /// <summary>
        ///     Indicates whether the suggestion has been accepted or declined
        /// </summary>
        public bool Accepted { get; set; }
    }
}