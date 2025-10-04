using AutoMapper;
using CMSDb.DbModels;
using CMSRep.DbModels;
using CMSRep.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSRep.Services
{
    public class CMSService : ICMSService
    {
        private readonly CmsContext _context;
        private readonly IMapper _mapper;
        public CMSService(CmsContext _context, IMapper mapper)
        {
            this._context = _context;
            this._mapper = mapper;
        }
        // CREATE
        public async Task<(bool IsSuccess, ClientDTO? Data, string? ErrorMessage)> CreateClient(ClientDTO createDto)
        {
            try
            {
                var client = _mapper.Map<Client>(createDto);
                await _context.Clients.AddAsync(client);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    var clientDto = _mapper.Map<ClientDTO>(client);
                    return (true, clientDto, null);
                }

                return (false, null, "Insert failed: No rows affected.");
            }
            catch (Exception ex)
            {
                return (false, null, $"Exception during insert: {ex.Message}");
            }
        }

        // READ ALL
        public async Task<(bool IsSuccess, IEnumerable<Client>? Data, string? ErrorMessage)> GetAllClient()
        {
            try
            {
                var clients = await _context.Clients.ToListAsync();
                return (true, clients, null);
            }
            catch (Exception ex)
            {
                return (false, null, $"Exception during fetch: {ex.Message}");
            }
        }

        // READ BY ID
        public async Task<(bool IsSuccess, Client? Data, string? ErrorMessage)> GetClientById(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null)
                    return (false, null, $"Client with ID {id} not found.");

                return (true, client, null);
            }
            catch (Exception ex)
            {
                return (false, null, $"Exception during fetch by ID: {ex.Message}");
            }
        }

        // UPDATE
        public async Task<(bool IsSuccess, string? ErrorMessage)> Update(ClientDTO updateDto)
        {
            try
            {
                var client = _mapper.Map<Client>(updateDto);
                _context.Clients.Update(client);
                var result = await _context.SaveChangesAsync();

                return result > 0
                    ? (true, null)
                    : (false, "Update failed: No rows affected.");
            }
            catch (Exception ex)
            {
                return (false, $"Exception during update: {ex.Message}");
            }
        }

        // DELETE
        public async Task<(bool IsSuccess, string? ErrorMessage)> Delete(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null)
                    return (false, "Delete failed: Client not found.");

                _context.Clients.Remove(client);
                var result = await _context.SaveChangesAsync();

                return result > 0
                    ? (true, null)
                    : (false, "Delete failed: No rows affected.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return (false, "Delete failed due to concurrency conflict.");
            }
            catch (Exception ex)
            {
                return (false, $"Exception during delete: {ex.Message}");
            }
        }


    }
}
