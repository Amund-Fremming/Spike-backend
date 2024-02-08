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

                Device device = new Device(deviceId);
                _context.Add(device);
                await _context.SaveChangesAsync();

                transaction.Commit();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<Device?> GetDeviceById(string deviceId)
    {
        return await _context.Devices
            .FirstOrDefaultAsync(d => d.UserDeviceId == deviceId);
    }

    public async Task<bool> DoesDeviceExist(string deviceId)
    {
        return await _context.Devices
            .AnyAsync(d => d.UserDeviceId == deviceId);
    }
}