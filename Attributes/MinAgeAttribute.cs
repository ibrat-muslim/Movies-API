using System.ComponentModel.DataAnnotations;

namespace moviesApi.Attributes;

public class MinAgeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var age = (int)value;

        return age is >= 18 and <= 60;
    }
}
