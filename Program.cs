using System;

namespace Yet_another_explanation_of_variance
{
    class Base { }

    class Derived : Base { }


    delegate TOutput InvariantFunc<TOutput>();

    delegate void InvariantAction<TInput>(TInput input);



    class Program
    {
        Derived ReturnDerived() { return new Derived(); }

        void InputBase(Base input) { }

        void LegalInvariance()
        {
            InvariantFunc<Base> notCovariance = ReturnDerived;
            Base aBaseClassInstance = notCovariance();

            InvariantAction<Derived> notContravariance = InputBase;
            notContravariance(new Derived());
        }

        void IllegalInvariance()
        {
            InvariantFunc<Derived> returnDerived = ReturnDerived;
            // illegal:
            InvariantFunc<Base> needsCovariance = returnDerived;


            InvariantAction<Base> inputBase = InputBase;
            // illegal:
            InvariantAction<Derived> needsContravariance = inputBase;
        }

        void CovarianceAndContravariance()
        {
            Func<Derived> returnDerived = ReturnDerived;
            // legal because TOutput in Func<TOutput> is covariant
            Func<Base> usesCovariance = returnDerived;


            Action<Base> inputBase = InputBase;
            // legal because TInput in Action<TInput> is contravariant
            Action<Derived> usesContravariance = inputBase;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
