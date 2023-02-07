using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services;
using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.Services.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace BuberBreakfast.Controllers;
public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        this._breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = BreakfastMapping.MapFrom(request);
        ErrorOr<Created> errorOrCreated = this._breakfastService.CreateBreakfast(breakfast);
        return errorOrCreated.Match<IActionResult>(
            _ => CreatedAtActionResult(breakfast),
            errors => Problem(errors)
        );
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> errorOrBreakfast = this._breakfastService.GetBreakfast(id);
        return errorOrBreakfast.Match<IActionResult>(
            _ => Ok(BreakfastMapping.MapResponseFrom(errorOrBreakfast.Value)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = BreakfastMapping.MapFrom(id, request);
        ErrorOr<UpsertionResult> errorOrUpsertionResult = this._breakfastService.UpsertBreakfast(breakfast);
        return errorOrUpsertionResult.Match<IActionResult>(
            upsertionResult => upsertionResult.IsCreated ? 
                CreatedAtActionResult(breakfast) :
                NoContent(),
                errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> errorOrDeleted = this._breakfastService.DeleteBreakfast(id);
        return errorOrDeleted.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
    
    private CreatedAtActionResult CreatedAtActionResult(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: BreakfastMapping.MapResponseFrom(breakfast)
        );
    }
}