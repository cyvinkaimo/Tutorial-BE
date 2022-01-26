namespace Yousource.TestHelpers.Helpers
{
    using FluentValidation.Results;

    public static class FluentValidationTestHelper
    {
        public static ValidationResult CreateErrorResult(IEnumerable<ValidationFailure> errors = null)
        {
            errors = errors ?? new List<ValidationFailure>
            {
                new ValidationFailure("TestProperty", "TestErrorMessage")
            };

            return new ValidationResult(errors);
        }

        public static ValidationResult CreateValidResult()
        {
            return new ValidationResult();
        }
    }
}
