using MinAPI.Models;

namespace MinAPI.Algorithms
{
    public class Validation
    {
        public bool isTableRowValid(string? modelRowField)
        {
            return !string.IsNullOrEmpty(modelRowField) && modelRowField.Length >= 3 && modelRowField.Length <= 20;
        }
    }
}