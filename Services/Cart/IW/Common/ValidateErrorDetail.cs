namespace IW.Common
{
    public record struct ValidateErrorDetail
    {
        public string Property { get; init; }
        public string Error { get; init; }
        public override string ToString()
        {
            return String.Format("Field: {0} - Error: {1}", Property, Error);
        }
    }
}
