using System;

namespace Yet_another_explanation_of_variance
{
    class BaseClass { }

    class DerivedClass : BaseClass { }

    delegate TOutput InvariantFunc<TOutput>();

    delegate void InvariantAction<TInput>(TInput input);



    class Program
    {
        DerivedClass ReturnDerivedClass() { return new DerivedClass(); }

        void InputBaseClass(BaseClass input) { }

        void LegalInvariance()
        {
            InvariantFunc<BaseClass> notCovariance = ReturnDerivedClass;
            BaseClass aBaseClassInstance = notCovariance();

            InvariantAction<DerivedClass> notContravariance = InputBaseClass;
            notContravariance(new DerivedClass());
        }

        void IllegalInvariance()
        {
            InvariantFunc<DerivedClass> returnDerived = ReturnDerivedClass;
            // illegal:
            InvariantFunc<BaseClass> needsCovariance = returnDerived;


            InvariantAction<BaseClass> argumentBase = InputBaseClass;
            // illegal:
            InvariantAction<DerivedClass> needsContravariance = argumentBase;
        }

        void CovarianceAndContravariance()
        {
            Func<DerivedClass> returnDerived = ReturnDerivedClass;
            // legal because TOutput in Func<TOutput> is covariant
            Func<BaseClass> usesCovariance = returnDerived;


            Action<BaseClass> argumentBase = InputBaseClass;
            // legal because TInput in Action<TInput> is contravariant
            Action<DerivedClass> usesContravariance = argumentBase;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
