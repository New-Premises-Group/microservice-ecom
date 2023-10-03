namespace IW.Common
{
    public abstract class GenericQueryError : IError
    {
        public GenericQueryError(string message)
        {
            Message = message;
        }
        public string Message { get; set; }

        public string? Code { get; set; }

        public HotChocolate.Path? Path { get; set; }

        public IReadOnlyList<Location>? Locations { get; set; }

        public IReadOnlyDictionary<string, object?>? Extensions { get; set; }

        public Exception? Exception { get; set; }

        public IError RemoveCode()
        {
            Code = "Unknown";
            return this;
        }

        public IError RemoveException()
        {
            throw new NotImplementedException();
        }

        public IError RemoveExtension(string key)
        {
            throw new NotImplementedException();
        }

        public IError RemoveExtensions()
        {
            throw new NotImplementedException();
        }

        public IError RemoveLocations()
        {
            throw new NotImplementedException();
        }

        public IError RemovePath()
        {
            throw new NotImplementedException();
        }

        public IError SetExtension(string key, object? value)
        {
            throw new NotImplementedException();
        }

        public IError WithCode(string? code)
        {
            Code = code;
            return this;
        }

        public IError WithException(Exception? exception)
        {
            throw new NotImplementedException();
        }

        public IError WithExtensions(IReadOnlyDictionary<string, object?> extensions)
        {
            Extensions = extensions;
            return this;
        }
        public IError WithExtensions()
        {
            Dictionary<string, object> extensions = new() { };
            extensions.Add(nameof(Code), Code);
            Extensions = extensions;
            return this;
        }

        public IError WithLocations(IReadOnlyList<Location>? locations)
        {
            throw new NotImplementedException();
        }

        public IError WithMessage(string message)
        {
            Message = message;
            return this;
        }

        public IError WithPath(HotChocolate.Path? path)
        {
            throw new NotImplementedException();
        }

        public IError WithPath(IReadOnlyList<object>? path)
        {
            throw new NotImplementedException();
        }
    }
}
