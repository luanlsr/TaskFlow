using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Handlers
{
    public class CreateWorkItemHandler : IRequestHandler<CreateWorkItemCommand, WorkItemDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWorkItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WorkItemDTO> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            // Inicia a transação
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Converte o comando para a entidade de domínio
                var workItem = _mapper.Map<WorkItem>(request);

                // Adiciona a entidade no repositório
                await _unitOfWork.WorkItemRepository.AddAsync(workItem);

                // Se precisar flushar mudanças antes do commit:
                // await _unitOfWork.SaveChangesAsync();

                // Faz o commit da transação
                await _unitOfWork.CommitAsync();

                // Retorna um DTO para a camada de apresentação
                return _mapper.Map<WorkItemDTO>(workItem);
            }
            catch
            {
                // Se ocorrer qualquer erro, faz rollback
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
