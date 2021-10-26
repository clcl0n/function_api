using System;
using System.Collections.Concurrent;
using Infrastructure.Persistance.Models;
using Infrastructure.Services;
using Domain.Functions;

namespace Infrastructure.Persistance
{
    public class FunctionResultStorage
    {
        private readonly ConcurrentDictionary<int, FunctionResult> _storage;
        private readonly FunctionResultStorageLogger _functionResultStorageLogger;

        public FunctionResultStorage(FunctionResultStorageLogger functionResultStorageLogger)
        {
            _functionResultStorageLogger = functionResultStorageLogger;
            _storage = new ConcurrentDictionary<int, FunctionResult>();
        }

        public (double newFunctionResult, double? oldFunctionResult) AddAndCalculate(int key, double input)
        {
            double? oldFunctionResult = null;
            FunctionResult newFunctionResult = new FunctionResult();
            _storage.AddOrUpdate(
                key,
                (key) => {
                    return newFunctionResult;
                },
                (_, currentFunctionResult) =>
                {
                    oldFunctionResult = currentFunctionResult.Value;
                    if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - currentFunctionResult.LastModified > 15000)
                    {
                        return newFunctionResult;
                    }

                    newFunctionResult.Value = Function.Calculate(input, currentFunctionResult.Value);
                    return newFunctionResult;
                }
            );

            _functionResultStorageLogger.Write(input, newFunctionResult.Value, newFunctionResult.LastModified);
            return (newFunctionResult.Value, oldFunctionResult);
        }
    }
}