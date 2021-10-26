using System.Text.Json.Serialization;

namespace Application.DTO
{
    public class PerformCalculationResponseDTO
    {
        [JsonPropertyName("computed_value")]
        public double ComputedValue { get; set; }
        [JsonPropertyName("input_value")]

        public double InputValue { get; set; }
        [JsonPropertyName("previous_value")]

        public double? PreviousValue { get; set; }
    }
}