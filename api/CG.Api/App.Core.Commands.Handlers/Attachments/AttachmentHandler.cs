using App.Core.Commands.Attachments;
using App.Core.Enums;
using App.Core.Requests.Attachments;
using App.Core.Requests.Handlers.Attachments;
using App.Core.ViewModels.Attachments;
using App.Data.Entities.Attachments;
using App.Interfaces.Data.Storage;
using App.Interfaces.Repositories.Core;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.Attachments
{
    public abstract class AttachmentHandler<TEntity> where TEntity : Attachment
    {
        private readonly ICurrentUser _currentUser;

        private readonly GetAttachmentHandler _getAttachmentHandler;
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private readonly IAttachmentStorage _storage;

        public AttachmentHandler(GetAttachmentHandler getAttachmentHandler,
            IAttachmentStorage attachmentStorage,
            IRepository<TEntity> repository,
            IContextProvider contextProvider,
            IMapper mapper
        )
        {
            _getAttachmentHandler = getAttachmentHandler;
            _repository = repository;
            _currentUser = contextProvider.Context;
            _storage = attachmentStorage;
            _mapper = mapper;
        }

        protected async Task<AttachmentView> CreateAttachmentAsync(AttachmentCommand request,
            CancellationToken cancellationToken
        )
        {
            var entity = _mapper.Map<TEntity>(request);
            var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            entity.CreatedByUserId = !_currentUser.UserId.Equals(Guid.Empty)
                ? _currentUser.UserId
                : adminId;
            var isFile = request.File != null && request.TypeId != (int) AttachmentTypes.Image;
            if (isFile)
                entity.FileName = $"{Guid.NewGuid():N}{Path.GetExtension(request.File.FileName)}";
            _repository.Add(entity);
            if (isFile)
            {
                //attachment container -> entity folder -> year -> month -> day structure.            
                var storageFile = _storage.GetDirectory(request.ObjectType.ToString().ToLowerInvariant())
                    .GetDirectory(entity.CreatedOn).GetFile(entity.FileName);

                await storageFile.SaveStreamAsync(request.File.OpenReadStream(), cancellationToken,
                    request.File.ContentType);
            }

            await _repository.SaveAsync(cancellationToken);
            return await _getAttachmentHandler.Handle(new GetAttachmentRequest(entity.Id), cancellationToken);
        }
    }
}