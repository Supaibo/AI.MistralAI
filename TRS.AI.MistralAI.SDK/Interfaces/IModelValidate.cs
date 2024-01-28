using System.ComponentModel.DataAnnotations;

namespace TRS.AI.MistralAI.Interfaces
{
    public interface IModelValidate
    {
        IEnumerable<ValidationResult> Validate();
    }
}
