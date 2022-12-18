using Canopus.API.DTOs;
using Canopus.API.Infrastructure.Context;
using Canopus.API.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Canopus.API.Services.Domain.Customer;

public class CustomerService : ICustomerService
{
    private readonly CanopusContext _context;

    public CustomerService(CanopusContext context)
    {
        _context = context;
    }

    public async Task<(IList<CustomerDto> Customers, PaginationDto PaginationDto)> Get(
        int pageIndex,
        int pageSize,
        CancellationToken token,
        string? search = "")
    {
        if (pageIndex < 1)
            throw new BadRequestException("Page index cannot be lower than 1.");

        if (pageSize < 1)
            throw new BadRequestException("Page size cannot be lower than 1.");

        var query = _context
            .Set<API.Domain.Customer>()
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            //ToDo: search up why freetext for functions it's giving an error about version with 6.0.12
            query = query.Where(e =>
                e.Name.ToLower().Contains(search.ToLower())
                || e.Email.Contains(search.ToLower()));
        }

        var customers = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new CustomerDto(e.Id, e.Name, e.Email))
            .ToListAsync(token)
            .ConfigureAwait(false);

        var totalItems = await query.CountAsync(token).ConfigureAwait(false);

        return (customers, new PaginationDto(totalItems, pageIndex, pageSize));
    }

    public async Task<CustomerDto> Add(AddCustomerDto dto, CancellationToken token)
    {
        var emailAlreadyInUse = await CheckEmailAlreadyInUse(dto.Email);

        if (emailAlreadyInUse)
            throw new UnprocessableEntityException("Email already in use.");

        var customer = new API.Domain.Customer
        {
            Name = dto.Name,
            Email = dto.Email
        };
        
        await _context.AddAsync(customer, token).ConfigureAwait(false);

        await _context.SaveChangesAsync(token).ConfigureAwait(false);

        return new CustomerDto(customer.Id, customer.Name, customer.Email);
    }

    private async Task<bool> CheckEmailAlreadyInUse(string email)
    {
        return await _context
            .Set<API.Domain.Customer>()
            .AsNoTracking()
            .AnyAsync(e => e.Email == email);
    }
}