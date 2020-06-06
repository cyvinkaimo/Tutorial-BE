namespace Yousource.Infrastructure.Messages
{
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;

    public abstract class Response
    {
        public Response()
        {
            this.ErrorCode = string.Empty;
            this.Message = string.Empty;
        }

        public virtual Dictionary<string, int> StatusCodeMap => new Dictionary<string, int>
        {
            { string.Empty, (int)HttpStatusCode.OK }
        };

        [DataMember]
        public int StatusCode
        {
            get
            {
                return this.StatusCodeMap[this.ErrorCode];
            }
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
