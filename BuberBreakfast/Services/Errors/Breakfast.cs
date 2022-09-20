using ErrorOr;

namespace BuberBreakfast.Services.Errors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound("Breakfast.NotFound", "Breakfast not found~");
    }
}