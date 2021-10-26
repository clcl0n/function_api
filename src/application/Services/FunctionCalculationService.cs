using Infrastructure.Persistance;

namespace Application.Services
{
    public class FunctionCalculationService
    {
        private readonly FunctionResultStorage _functionResultStorage;

        public FunctionCalculationService(FunctionResultStorage functionResultStorage)
        {
            _functionResultStorage = functionResultStorage;
        }

        public (double newFunctionResult, double? oldFunctionResult) Calculate(int key, double input)
        {
            // Tu je mozne zobrat z dictionary informacie ci je treba vypocitat result alebo pouzit default
            // ale pre znizenie komplexity a prevencie racecondition kde pri kontrole ako ContainsKey
            // plus pocitanie vzorca moze prist iny request a zmenit obsah dictionary
            // je vypocet posunuty priamo do storage objektu s pouzitim nativnej funkcie AddOrUpdate
            return _functionResultStorage.AddAndCalculate(key, input);
        }
    }
}