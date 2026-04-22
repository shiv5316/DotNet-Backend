using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly CountriesContext _context;

    public LocationController(CountriesContext context)
    {
        _context = context;
    }

    // GET: api/location/countries
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _context.Countries
        .AsNoTracking()
        .Select(c => new
        {
            id = c.Id,
            name = c.Name
        })
        .ToListAsync();

        return Ok(countries);
    }

    // GET: api/location/states/1
    [HttpGet("states/{countryId}")]
    public async Task<IActionResult> GetStates(int countryId)
    {
        var states = await _context.States
        .AsNoTracking()
        .Where(s => s.CountryId == countryId)
        .Select(s => new
        {
            id = s.Id,
            name = s.Name,
            countryId = s.CountryId
        })
        .ToListAsync();

        return Ok(states);

    }

    // GET: api/location/lookup
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupData()
    {
        var countries = await _context.Countries
            .AsNoTracking()
            .Select(c => new
            {
                id = c.Id,
                name = c.Name
            })
            .ToListAsync();

        var states = await _context.States
            .AsNoTracking()
            .Select(s => new
            {
                id = s.Id,
                name = s.Name,
                countryId = s.CountryId
            })
            .ToListAsync();

        return Ok(new
        {
            countries,
            states
        });
    }
}