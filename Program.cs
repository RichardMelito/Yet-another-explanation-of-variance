using System;

namespace Yet_another_explanation_of_variance
{
    class BaseClass { }
    class DerivedClass : BaseClass { }

    delegate TOutput InvariantFunc<TOutput>();
    delegate void InvariantAction<TInput>(TInput input);

    class Program
    {
        DerivedClass ReturnDerivedClass()
        {
            return new DerivedClass();
        }

        void ArgumentBaseClass(BaseClass input)
        {

        }

        void LegalInvariance()
        {
            InvariantFunc<BaseClass> notCovariance = ReturnDerivedClass;

            InvariantAction<DerivedClass> notContravariance = ArgumentBaseClass;
        }

        void IllegalInvariance()
        {
            InvariantFunc<DerivedClass> returnDerived = ReturnDerivedClass;
            // illegal:
            InvariantFunc<BaseClass> needsCovariance = returnDerived;


            InvariantAction<BaseClass> argumentBase = ArgumentBaseClass;
            // illegal:
            InvariantAction<DerivedClass> needsContravariance = argumentBase;
        }

        void Covariance()
        {
            Func<DerivedClass> returnDerived = ReturnDerivedClass;
            // legal:
            Func<BaseClass> usesCovariance = returnDerived;
        }

        void Contravariance()
        {
            Action<BaseClass> argumentBase = ArgumentBaseClass;
            // legal:
            Action<DerivedClass> usesContravariance = argumentBase;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
