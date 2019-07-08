using Xunit;
using Shouldly;
using Nipr.Parser.Common;

namespace Nipr.Parser.Test.Common
{
    public class IResponseExtensionTests
    {
        public class IsErrorRespones
        {
            [Fact]
            public void returns_true_when_type_is_error()
            {
                var response = new TestResponse
                {
                    TransactionType = new TransactionType
                    {
                        Type = TransactionTypes.Error
                    }
                };

                response.ShouldNotBeNull();
                response.IsErrorResponse().ShouldBeTrue();
            }

            [Fact]
            public void returns_false_when_value_transactiontype_is_not_set()
            {
                var response = new TestResponse();
                response.ShouldNotBeNull();
                response.IsErrorResponse().ShouldBeFalse();
            }

            [Fact]
            public void returns_false_when_type_is_not_set()
            {
                var response = new TestResponse();
                response.TransactionType = new TransactionType();

                response.ShouldNotBeNull();
                response.IsErrorResponse().ShouldBeFalse();
            }

            [Fact]
            public void returns_false_when_type_is_not_set_to_error()
            {
                var response = new TestResponse();
                response.TransactionType = new TransactionType { Type = "NOT_ERROR" };

                response.ShouldNotBeNull();
                response.IsErrorResponse().ShouldBeFalse();
            }
        }

        public class GetErrorMessage
        {
            [Fact]
            public void returns_empty_string_when_error_is_null()
            {
                var response = new TestResponse();

                response.GetErrorMessage().ShouldBeEmpty();
            }

            [Fact]
            public void returns_empty_string_when_error_description_is_not_specified()
            {
                var response = new TestResponse { Error = new Error() };

                response.GetErrorMessage().ShouldBeEmpty();
            }

            [Fact]
            public void returns_message_when_error_is_set()
            {
                var message = "Test error message";
                var response = new TestResponse
                {
                    Error = new Error
                    {
                        Description = message
                    }
                };

                response.GetErrorMessage().ShouldBe(message);
            }
        }
    }

    public class TestResponse : IResponse
    {
        public TransactionType TransactionType { get; set; }
        public Error Error { get; set; }
    }
}