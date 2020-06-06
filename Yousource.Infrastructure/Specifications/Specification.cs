namespace Yousource.Infrastructure.Specifications
{
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity, ref ICollection<string> errors);

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public abstract class Specification<TEntity, TResult> : ISpecification<TEntity, TResult>
    {
        public abstract bool IsSatisfiedBy(TEntity entity, ref TResult result);
        
        public ISpecification<TEntity, TResult> And(ISpecification<TEntity, TResult> specification)
        {
            return new AndSpecification<TEntity, TResult>(this, specification);
        }
    }
}
