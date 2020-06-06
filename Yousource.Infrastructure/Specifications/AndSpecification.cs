namespace Yousource.Infrastructure.Specifications
{
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override bool IsSatisfiedBy(T entity, ref ICollection<string> errors)
        {
            var left = this.left.IsSatisfiedBy(entity, ref errors);
            var right = this.right.IsSatisfiedBy(entity, ref errors);

            return left && right;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class AndSpecification<TEntity, TResult> : Specification<TEntity, TResult>
    {
        private readonly ISpecification<TEntity, TResult> left;
        private readonly ISpecification<TEntity, TResult> right;

        public AndSpecification(ISpecification<TEntity, TResult> left, ISpecification<TEntity, TResult> right)
        {
            this.left = left;
            this.right = right;
        }

        public override bool IsSatisfiedBy(TEntity entity, ref TResult result)
        {
            var left = this.left.IsSatisfiedBy(entity, ref result);
            var right = this.right.IsSatisfiedBy(entity, ref result);

            return left && right;
        }
    }
}
