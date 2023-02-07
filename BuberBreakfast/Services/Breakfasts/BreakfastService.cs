using BuberBreakfast.Models;
using BuberBreakfast.Services.Errors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService: IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> Breakfasts = new();
    
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        Breakfasts.Add(breakfast.Id, breakfast);
        return Result.Created;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        return Breakfasts.TryGetValue(id, out var breakfast) ? breakfast : Errors.Errors.Breakfast.NotFound;
    }
    
    public ErrorOr<UpsertionResult> UpsertBreakfast(Breakfast breakfast)
    {
        var isCreated = !Breakfasts.ContainsKey(breakfast.Id);
        Breakfasts[breakfast.Id] = breakfast;
        return new UpsertionResult(isCreated);
    }
    
    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        Breakfasts.Remove(id);
        return Result.Deleted;
    }
}