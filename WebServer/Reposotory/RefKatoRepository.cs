using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class RefKatoRepository : IRefKato
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<Ref_Kato> _dbSet;
        private readonly DbSet<Ref_Street> _dbSetStreet;
        private readonly DbSet<Ref_Building> _dbSetBuilding;
        public RefKatoRepository(WaterDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Ref_Kato>();
            _dbSetStreet = _context.Set<Ref_Street>();
            _dbSetBuilding = _context.Set<Ref_Building>();
        }

        public async Task<List<RefKatoTreeDto>> GetRefKatoAsync(int parentId, bool? getNested)
        {
            try
            {
                if (!getNested.HasValue || getNested.Value == false)
                {
                    return await _dbSet.Where(x => x.ParentId == parentId).Select(x => new RefKatoTreeDto()
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Code = x.Code,
                        Description = x.Description,
                        IsReportable = x.IsReportable,
                        KatoLevel = x.KatoLevel,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        Name = x.NameRu,
                        Children = new List<RefKatoTreeDto>(),
                    }).ToListAsync();
                }
                else
                {
                    var rootList = await _dbSet.Where(x => x.ParentId == parentId).Select(x => new RefKatoTreeDto()
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Code = x.Code,
                        Description = x.Description,
                        IsReportable = x.IsReportable,
                        KatoLevel = x.KatoLevel,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        Name = x.NameRu,
                        Children = new List<RefKatoTreeDto>(),
                    }).ToListAsync();

                    if (rootList != null && rootList.Count > 0)
                    {
                        foreach (var root in rootList)
                        {
                            root.Children = await GetChildren(root.Id);

                        }
                        return rootList;
                    }
                }
                return new List<RefKatoTreeDto>();
            }
            catch (Exception ex)
            {
                return new List<RefKatoTreeDto>();
            }
        }

        private async Task<List<RefKatoTreeDto>> GetChildren(int parentId)
        {
            var children = await _dbSet.Where(x => x.ParentId == parentId).Select(x => new RefKatoTreeDto()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Code = x.Code,
                Description = x.Description,
                IsReportable = x.IsReportable,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Name = x.NameRu,
                Children = new List<RefKatoTreeDto>(),
            }).ToListAsync();
            foreach (var child in children)
            {
                child.Children = await GetChildren(child.Id);
            }
            return children;
        }

        public async Task<List<Ref_Street>> GetRefStreetByKatoId(int id)
        {
            return await _dbSetStreet.Where(x => x.RefKatoId == id).ToListAsync();
        }
        public async Task<List<Ref_Building>> GetRefBuildingByStreetId(int id)
        {
            return await _dbSetBuilding.Where(x => x.RefStreetId == id).ToListAsync();
        }

        public async Task<Ref_Street> AddStreet(Ref_Street row)
        {
            try
            {
                await _dbSetStreet.AddAsync(row);
                await _context.SaveChangesAsync();
                return row;
            }
            catch (Exception)
            {
                throw new Exception("ошибка при добавлении улицы");
            }
        }

        public async Task<Ref_Street> UpdateStreet(Ref_Street row, int id)
        {
            _dbSetStreet.Attach(row);
            _context.Entry(row).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return row;
        }

        public async Task DeleteStreet(int id)
        {
            var entity = await _dbSetStreet.FindAsync(id);
            if (entity != null)
            {
                _dbSetStreet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return;
        }

        public async Task<Ref_Building> AddBuilding(Ref_Building row)
        {
            _dbSetBuilding.Add(row);
            await _context.SaveChangesAsync();
            return row;
        }

        public async Task<Ref_Building> UpdateBuilding(Ref_Building row, int id)
        {
            _dbSetBuilding.Attach(row);
            _context.Entry(row).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return row;
        }

        public async Task DeleteBuilding(int id)
        {
            var entity = await _dbSetBuilding.FindAsync(id);
            if (entity != null)
            {
                _dbSetBuilding.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return;
        }
        public async Task<bool> IsReportable(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                return entity.IsReportable;
            }
            return false;
        }
    }
}
