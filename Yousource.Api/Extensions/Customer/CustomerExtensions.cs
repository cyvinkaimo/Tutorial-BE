namespace Yousource.Api.Extensions.Customer
{
    using System.Collections.Generic;
    using System.Linq;
    using Yousource.Api.Messages;
    using Yousource.Api.Messages.Customers.Requests;
    using Yousource.Api.Models.Customers;
    using Yousource.Infrastructure.Messages.Customers.Requests;
    using Yousource.Infrastructure.Messages.Customers.Responses;
    using Yousource.Infrastructure.Models.Customers;

    public static class CustomerExtensions
    {
        public static CreateCustomerRequest AsRequest(this CreateCustomerWebRequest request)
        {
            var result = new CreateCustomerRequest
            {
                Name = request.Name,
                Email = request.Email
            };

            return result;
        }

        public static WebResponse AsWebResponse(this CreateCustomerResponse response)
        {
            var result = new WebResponse
            {
                Message = response.Message,
                ErrorCode = response.ErrorCode,
                StatusCode = response.StatusCode
            };

            return result;
        }

        public static WebResponse<IEnumerable<CustomerWebModel>> AsWebResponse(this GetCustomersResponse response)
        {
            var result = new WebResponse<IEnumerable<CustomerWebModel>>(response.Data?.AsWebModel())
            {
                Message = response.Message,
                ErrorCode = response.ErrorCode,
                StatusCode = response.StatusCode
            };

            return result;
        }

        public static CustomerWebModel AsWebModel(this Customer model)
        {
            return new CustomerWebModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt.GetValueOrDefault()
            };
        }

        public static IEnumerable<CustomerWebModel> AsWebModel(this IEnumerable<Customer> models)
        {
            return models.Select(entity => entity.AsWebModel());
        }
    }
}
