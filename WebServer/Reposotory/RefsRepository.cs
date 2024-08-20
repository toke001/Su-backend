using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Emuns;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class RefsRepository: IRefs
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<Ref_Role> _dbSet;
        private readonly DbSet<Universal_Refference> _dbSetUniver;
        private readonly DbSet<Business_Dictionary> _dbSetBusines;

        public RefsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Ref_Role>();
            _dbSetUniver = _context.Set<Universal_Refference>();
            _dbSetBusines = _context.Set<Business_Dictionary>();
        }

        public async Task<List<RefRoleDto>> GetRefRolesList()
        {
            var list = await _dbSet.Select(x => new RefRoleDto { Id = x.Id, Label = x.Code }).ToListAsync();
            return list;
        }

        public Dictionary<int,string> GetDataTypes()
        {
            Dictionary<int,string> enums = new Dictionary<int,string>();
            foreach (var item in Enum.GetValues(typeof(Enums.DataTypeEnum)))
            {
                enums.Add((int)item, item.ToString());
            }
            return enums;
        }

        #region Universal_Refferences
        public async Task<List<RefUniverRefDto>> GetRefUniverList()
        {
            return await _dbSetUniver.Where(x=>x.IsDel==false).Select(x => new RefUniverRefDto 
            { 
                Id = x.Id, 
                ParentId = x.ParentId,
                Code = x.Code,
                Type = x.Type,
                BusinessDecription = x.BusinessDecription,
                NameRu = x.NameRu,
                NameKk = x.NameKk,
                DescriptionKk = x.DescriptionKk,
                DescriptionRu = x.DescriptionRu,
                IsDel = x.IsDel
            }).ToListAsync();            
        }        

        public async Task<RefUniverRefDto> GetRefUniverById(Guid Id)
        {           
           var res = await _dbSetUniver.Where(x=>x.Id==Id&&x.IsDel==false).Select(x => new RefUniverRefDto
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Code = x.Code,
                Type = x.Type,
                BusinessDecription = x.BusinessDecription,
                NameRu = x.NameRu,
                NameKk = x.NameKk,
                DescriptionKk = x.DescriptionKk,
                DescriptionRu = x.DescriptionRu,
                IsDel = x.IsDel
            }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<Universal_Refference> AddRefUniver(Universal_Refference model)
        {            
            await _dbSetUniver.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;            
        }

        public async Task<RefUniverRefDto> UpdateRefUniver(Universal_Refference model)
        {
            var univ = await _dbSetUniver.FindAsync(model.Id);
            if (univ == null) { throw new Exception("Объект не найден"); }
            univ.Code = model.Code;
            univ.Type = model.Type;
            univ.BusinessDecription = model.BusinessDecription;
            univ.NameRu = model.NameRu;
            univ.NameKk = model.NameKk;
            univ.DescriptionKk = model.DescriptionKk;
            univ.DescriptionRu = model.DescriptionRu;
            univ.IsDel = model.IsDel;
            _context.Entry(univ).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetRefUniverById(univ.Id);
        }

        public async Task<RefUniverRefDto> DeleteRefUniver(Guid id)
        {
            var item = await _dbSetUniver.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Объект не найден");
            }
            item.IsDel = true;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetRefUniverById(item.Id);
        }

        #endregion Universal_Refferences

        #region Business_Dictionary
        public async Task<List<RefBusinesDictDto>> GetBusinesDictList(Guid? parentId, string? type)
        {
            var query = _dbSetBusines.Where(x => x.IsDel == false).AsQueryable();
            if(parentId.HasValue) query = query.Where(x=>x.ParentId == parentId.Value);
            if (!string.IsNullOrEmpty(type)) query = query.Where(x => x.Type == type);

            return await query.Select(x => new RefBusinesDictDto
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Code = x.Code,
                Type = x.Type,
                BusinessDecription = x.BusinessDecription,
                NameRu = x.NameRu,
                NameKk = x.NameKk,
                DescriptionKk = x.DescriptionKk,
                DescriptionRu = x.DescriptionRu,
                IsDel = x.IsDel
            }).ToListAsync();
        }

        public async Task<RefBusinesDictDto> GetBusinesDictById(Guid Id)
        {
            var res = await _dbSetBusines.Where(x => x.Id == Id && x.IsDel == false).Select(x => new RefBusinesDictDto
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Code = x.Code,
                Type = x.Type,
                BusinessDecription = x.BusinessDecription,
                NameRu = x.NameRu,
                NameKk = x.NameKk,
                DescriptionKk = x.DescriptionKk,
                DescriptionRu = x.DescriptionRu,
                IsDel = x.IsDel
            }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<Business_Dictionary> AddBusinesDict(Business_Dictionary model)
        {
            await _dbSetBusines.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<RefBusinesDictDto> UpdateBusinesDict(Business_Dictionary model)
        {
            var obj = await _dbSetBusines.FindAsync(model.Id);
            if (obj == null) { throw new Exception("Объект не найден"); }
            obj.Code = model.Code;
            obj.Type = model.Type;
            obj.BusinessDecription = model.BusinessDecription;
            obj.NameRu = model.NameRu;
            obj.NameKk = model.NameKk;
            obj.DescriptionKk = model.DescriptionKk;
            obj.DescriptionRu = model.DescriptionRu;
            obj.IsDel = model.IsDel;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetBusinesDictById(obj.Id);
        }

        public async Task<RefBusinesDictDto> DeleteBusinesDict(Guid id)
        {
            var item = await _dbSetBusines.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Объект не найден");
            }
            item.IsDel = true;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetBusinesDictById(item.Id);
        }

        #endregion Business_Dictionary
    }
}
