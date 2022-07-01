namespace Yousource.TurorialApi.Extensions
{
    using Yousource.Infrastructure.Messages.Tutorial.Requests;
    using Yousource.TutorialApi.Messages.Identity;

    public static class TutorialExtensions
    {
        public static InsertEmployeeRequest AsRequest(this InsertEmployeeWebRequest webRequest)
        {
            return new InsertEmployeeRequest
            {
                Address = webRequest.Address,
                FirstName = webRequest.FirstName,
                LastName = webRequest.LastName,
                Age = webRequest.Age
            };
        }
    }
}
