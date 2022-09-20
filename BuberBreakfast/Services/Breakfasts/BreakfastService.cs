using BuberBreakfast.Models;
using BuberBreakfast.Services.Errors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService: IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> Breakfasts = new();
    
    public void CreateBreakfast(Breakfast breakfast)
    {
        Breakfasts.Add(breakfast.Id, breakfast);
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        return Breakfasts.TryGetValue(id, out var breakfast) ? breakfast : Errors.Errors.Breakfast.NotFound;
    }
    
    public void UpsertBreakfast(Breakfast breakfast)
    {
        Breakfasts[breakfast.Id] = breakfast;
    }
    
    public void DeleteBreakfast(Guid id)
    {
        Breakfasts.Remove(id);
    }
}