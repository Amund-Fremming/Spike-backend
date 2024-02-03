using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class DeviceRepository(AppDbContext context) {

    public readonly AppDbContext _context = context;

    public async Task<bool> AddDevice(string deviceId) 
    {
        await using(var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                bool doesExist = await _context.Devices
                    .AnyAsync(d => d.UserDeviceId == deviceId);
                
                if(doesExist)
                {
                    return false;
                }

                await _context.AddAsync(new Device(deviceId));
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}