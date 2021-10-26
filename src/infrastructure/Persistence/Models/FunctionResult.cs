using System;

namespace Infrastructure.Persistance.Models
{
    public class FunctionResult
    {
        public double Value { get; set; } = 2;
        public long LastModified { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}