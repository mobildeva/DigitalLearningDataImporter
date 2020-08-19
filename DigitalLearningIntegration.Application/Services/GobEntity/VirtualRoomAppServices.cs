using AutoMapper;
using DigitalLearningIntegration.Application.Dto;
//using DigitalLearningIntegration.Domain.Entities;
//using DigitalLearningIntegration.Infraestructure.Dto;
//using DigitalLearningIntegration.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Application.Services
{
    /*public class VirtualRoomAppServices : IVirtualRoomAppServices
    {
        private readonly IMapper _mapper;
        private readonly IVirtualRoomRepository _repository;

        public VirtualRoomAppServices(IMapper mapper, IVirtualRoomRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public ResultDto AddOrUpdate(VirtualRoomRequestDto request)
        {
            try
            {
                var entity = _mapper.Map<VirtualRoom>(request);
                return _repository.CreatedOrUpdate(entity);
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }

        public ResultDto Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return new ResultDto()
                {
                    Result = true,
                    Message = "successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }

        public VirtualRoomListResponseDto GetAll()
        {
            try
            {
                var listcampus = _repository.GetAll();
                List<VirtualRoomDto> results = _mapper.Map<List<VirtualRoomDto>>(listcampus);

                return new VirtualRoomListResponseDto()
                {
                    ListVirtualRoomDto = results.ToList(),
                    Message = "Succeds",
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new VirtualRoomListResponseDto()
                {
                    ListVirtualRoomDto = null,
                    Result = false,
                    Message = ex.Message
                };
            }
        }

            public VirtualRoomResponseDto GetById(int id)
        {
            try
            {
                var entity = _repository.GetById(id);
                VirtualRoomDto results = _mapper.Map<VirtualRoomDto>(entity);
               
                return new VirtualRoomResponseDto()
                {
                    VirtualRoomDto = results,
                    Message = "success",
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new VirtualRoomResponseDto()
                {
                    VirtualRoomDto = null,
                    Message = ex.Message,
                    Result = false
                };
            }
        }
    }*/
}
