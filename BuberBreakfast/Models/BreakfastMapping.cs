using BuberBreakfast.Contracts.Breakfast;

namespace BuberBreakfast.Models;

public class BreakfastMapping
{
    public static BreakfastResponse MapResponseFrom(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
    }
    
    public static Breakfast MapFrom(CreateBreakfastRequest request)
    {
        return new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
    }

    public static Breakfast MapFrom(Guid id, UpsertBreakfastRequest request)
    {
        return new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
    }
}