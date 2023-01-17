using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace open_ai_example.Exceptions
{
    public class HttpException : SystemException
    {

        private string _message { get; set; } = null!;
        private HttpStatusCode _statusCode { get; set; }

        public HttpException(string message, HttpStatusCode statusCode) : base(message)
        {
            _message = message;
            _statusCode = statusCode;
        }

        public HttpRequestException GetHttpRequestException()
        {
            return new HttpRequestException(_message, this, _statusCode);
        }
    }
}
