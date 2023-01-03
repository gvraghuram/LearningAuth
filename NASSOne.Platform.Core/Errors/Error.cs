using NASSOne.Platform.Core.Common;
using System.Text.Json.Serialization;

namespace NASSOne.Platform.Core.Errors
{
    public sealed class Error : ValueObject
    {
        public string Code { get; }
        public string Message { get; }
        public IEnumerable<object> Details { get; }

        [JsonConstructor] // Might want to remove this and use a custom JsonConverter in tests.
        internal Error(string code, string message, IEnumerable<object> details = null)
        {
            Code = code;
            Message = message;
            Details = details;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Only the Code property is considered the equality value for
            // an Error. The Message property can vary across usages.
            yield return Code;
        }
    }
}
