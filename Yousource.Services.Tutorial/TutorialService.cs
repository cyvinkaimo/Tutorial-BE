namespace Yousource.Services.Tutorial
{
    using Yousource.ExceptionHandling;
    using Yousource.Infrastructure.Data.Interfaces;
    using Yousource.Infrastructure.Exceptions;
    using Yousource.Infrastructure.Logging;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Messages.Tutorial.Requests;
    using Yousource.Infrastructure.Models.Tutorial;
    using Yousource.Infrastructure.Services.Interfaces;
    using Yousource.Services.Tutorial.Constants;
    using Yousource.Services.Tutorial.Extensions;
    using Yousource.Services.Tutorial.Specifications;

    public class TutorialService : ITutorialService, IExceptionHandleable
    {
        private readonly ITutorialDataGateway dataGateway;

        public TutorialService(ITutorialDataGateway dataGateway, ILogger logger)
        {
            this.dataGateway = dataGateway;
            this.Logger = logger;
        }

        public ILogger Logger { get; private set; }

        [HandleServiceException(typeof(TutorialServiceException))]
        public async Task<Response<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            var result = new Response<IEnumerable<Employee>>();

            result.Data = (await this.dataGateway.GetEmployeesAsync()).AsModel();

            return result;
        }

        [HandleServiceException(typeof(TutorialServiceException))]
        public async Task<Response> InsertEmployeeAsync(InsertEmployeeRequest request)
        {
            var result = new Response();
            var errors = new List<string>() as ICollection<string>;

            //// Validate using Specification classes. You can leverage factories to inject
            //// your specifications if it touches the database
            var spec = new ValidInsertEmployeeRequestSpecification();
            if (spec.IsSatisfiedBy(request, ref errors))
            {
                //// Decouple Service Models from Shared models i.e. Create `AsEntity` extension to convert vice-versa
                await this.dataGateway.InsertEmployeeAsync(request.AsEntity());
            }
            else
            {
                //// Communicate Specification-added errors, and return appropriate error.
                result.SetError(Errors.CreateValidationError, errors);
            }

            return result;
        }
    }
}