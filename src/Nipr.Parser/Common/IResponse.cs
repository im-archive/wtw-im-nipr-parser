namespace Nipr.Parser.Common
{
    public interface IResponse
    {
        TransactionType TransactionType { get; set; }

        Error Error { get; set; }
    }

    public static class IResponseExtensions
    {
        public static string GetErrorMessage(this IResponse response)
        {
            if (response.Error == null) return string.Empty;
            return response.Error.Description;
        }

        public static bool IsErrorResponse(this IResponse response)
        {
            return (response.TransactionType != null && response.TransactionType.Type != null && response.TransactionType.Type.Equals(TransactionTypes.Error));
        }
    }
}