namespace Yousource.Infrastructure.Specifications
{
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity, ref ICollection<string> errors);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public interface ISpecification<TEntity, TResult>
    {
        bool IsSatisfiedBy(TEntity entity, ref TResult result);

        ISpecification<TEntity, TResult> And(ISpecification<TEntity, TResult> specification);
    }
}
