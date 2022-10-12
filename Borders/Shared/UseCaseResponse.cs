using Borders.Models;
using System.Collections.Generic;

namespace Borders.Shared
{
    public class UseCaseResponse<T> : IResponse
    {
        private UseCaseResponseKind ErrorKind { get; set; }
        public string ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; }

        public T Result { get; private set; }

        public UseCaseResponse<T> SetResult(T result)
        {
            ErrorMessage = null;
            Result = result;

            return this;
        }

        public UseCaseResponse<T> SetError(string errorMessage, UseCaseResponseKind errorKind, IEnumerable<ErrorMessage> errors = null)
        {
            ErrorMessage = errorMessage;
            ErrorKind = errorKind;
            Errors = errors;

            return this;
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.InternalServerError, errors);
        }

        public UseCaseResponse<T> SetBadRequest(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.BadRequest, errors);
        }

        public UseCaseResponse<T> SetNotFound(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.NotFound, errors);
        }

        public bool Success()
        {
            return ReferenceEquals(ErrorMessage, null);
        }

        public UseCaseResponseKind? GetErrorKind()
        {
            if (ReferenceEquals(ErrorMessage, null))
                return null;

            return ErrorKind;
        }
    }
}