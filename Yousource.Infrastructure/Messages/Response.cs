﻿namespace Yousource.Infrastructure.Messages
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Runtime.Serialization;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Response<T> : Response
    {
        public Response(T data)
        {
            this.Data = data;
        }

        public Response()
        {
        }

        [DataMember]
        public T Data { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Response
    {
        public Response()
        {
            this.ErrorCode = string.Empty;
            this.Message = string.Empty;
        }

        [DataMember]
        public string Message { get; protected set; }

        [DataMember]
        public string ErrorCode { get; protected set; }

        #region Set Error Methods
        public void SetError(string errorCode, string error = "")
        {
            this.ErrorCode = errorCode;
            this.Message = error;
        }

        public void SetError(string errorCode, ICollection<string> errors)
        {
            this.ErrorCode = errorCode;
            this.Message = string.Join(". ", errors);
        }
        #endregion
    }
}
