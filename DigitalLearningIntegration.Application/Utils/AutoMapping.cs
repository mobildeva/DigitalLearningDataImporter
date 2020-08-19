using AutoMapper;
using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Application.GobEntity.Dto;
using DigitalLearningIntegration.Application.Services.Seg.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        //CreateMap<GopEntityDto, GopEntity>();

        //CreateMap<GopEntity, GopEntityDto>();

        CreateMap<Users, UserDto>();

        CreateMap<UserDto, Users>();
    }

    //public IConfigurationProvider ConfigurationProvider => throw new NotImplementedException();

    //public Func<Type, object> ServiceCtor => throw new NotImplementedException();

    //public TDestination Map<TDestination>(object source)
    //{
    //    throw new NotImplementedException();
    //    //var entity = source as Users;
    //    //if (entity != null)
    //    //{
    //    //    return new UserDto(entity);
    //    //}
    //}

    //public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
    //{
    //    throw new NotImplementedException();
    //}

    //public TDestination Map<TSource, TDestination>(TSource source)
    //{
    //    throw new NotImplementedException();
    //}

    //public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
    //{
    //    throw new NotImplementedException();
    //}

    //public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    //{
    //    throw new NotImplementedException();
    //}

    //public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
    //{
    //    throw new NotImplementedException();
    //}

    //public object Map(object source, Type sourceType, Type destinationType)
    //{
    //    throw new NotImplementedException();
    //}

    //public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
    //{
    //    throw new NotImplementedException();
    //}

    //public object Map(object source, object destination, Type sourceType, Type destinationType)
    //{
    //    throw new NotImplementedException();
    //}

    //public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
    //{
    //    throw new NotImplementedException();
    //}

    //public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
    //{
    //    throw new NotImplementedException();
    //}

    //public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
    //{
    //    throw new NotImplementedException();
    //}
}
