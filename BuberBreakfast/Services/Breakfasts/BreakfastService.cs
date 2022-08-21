using BuberBreakfast.Models;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService: IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> Breakfasts = new();
    
    public void CreateBreakfast(Breakfast breakfast)
    {
        Breakfasts.Add(breakfast.Id, breakfast);
    }

    public Breakfast GetBreakfast(Guid id)
    {
        return Breakfasts[id];
    }
}