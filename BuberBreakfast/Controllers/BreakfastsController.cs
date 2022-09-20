using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.Services.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        this._breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = MapBreakfast(request);
        this._breakfastService.CreateBreakfast(breakfast);
        var response = MapBreakfast(breakfast);
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = response.Id },
            value: response
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> errorOrBreakfast = this._breakfastService.GetBreakfast(id);
        if (errorOrBreakfast.IsError && errorOrBreakfast.FirstError == Errors.Breakfast.NotFound)
        {
            return NotFound();
        }

        var response = MapBreakfast(errorOrBreakfast.Value);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = MapBreakfast(id, request);
        this._breakfastService.UpsertBreakfast(breakfast);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        this._breakfastService.DeleteBreakfast(id);
        return NoContent();
    }

    private BreakfastResponse MapBreakfast(Breakfast breakfast)
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
    
    private Breakfast MapBreakfast(CreateBreakfastRequest request)
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

    private Breakfast MapBreakfast(Guid id, UpsertBreakfastRequest request)
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