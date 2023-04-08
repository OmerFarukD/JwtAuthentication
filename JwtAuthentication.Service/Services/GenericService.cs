using System.Linq.Expressions;
using JwtAuthentication.Core.Repository;
using JwtAuthentication.Core.Services;
using JwtAuthentication.Core.UnitOfWork;
using JwtAuthentication.Service.DtoMappers;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;

namespace JwtAuthentication.Service.Services;

public class GenericService<TEntity,TDto> : IServiceGeneric<TEntity,TDto> where TEntity: class where TDto: class
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IGenericRepository<TEntity> _genericRepository;

    public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }


    public async Task<Response<TDto>> GetByIdAsync(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return Response<TDto>.Fail("EntityNotFound",400,true);
        }
        
        var dto = ObjectMapper.Mapper.Map<TDto>(entity);
        return Response<TDto>.Success(dto,200);
    }

    public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
    {
        var listOfEntity = await _genericRepository.GetAllAsync();
        var dtoList = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(listOfEntity);
        return Response<IEnumerable<TDto>>.Success(dtoList,200);
    }

    public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        var listOfEntityByWhere =_genericRepository.Where(predicate);
        return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await listOfEntityByWhere.ToListAsync()),200);
    }

    public async Task<Response<TDto>> AddAsync(TDto dto)
    {
        var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);

        await _genericRepository.AddAsync(newEntity);
        await _unitOfWork.SaveChangesAsync();
        return Response<TDto>.Success(dto,200);
    }

    public async Task<Response<NoDataDto>> Remove(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return Response<NoDataDto>.Fail("Entity is not found", 400, true);
        }
        _genericRepository.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response<NoDataDto>.Success(200);
    }

    public async Task<Response<NoDataDto>> Update(TDto dto,int id)
    {
        var result = await _genericRepository.GetByIdAsync(id);
        if (result is null)
        {
            return Response<NoDataDto>.Fail("Entity is not found",400,true);
        }
        var entity = ObjectMapper.Mapper.Map<TEntity>(dto);
        _genericRepository.Update(entity);
       await _unitOfWork.SaveChangesAsync();
        return  Response<NoDataDto>.Success(200);
    }
}