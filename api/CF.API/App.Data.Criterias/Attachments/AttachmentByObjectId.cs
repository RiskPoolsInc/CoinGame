using System.Linq.Expressions;
using App.Data.Criterias.Core;

namespace App.Data.Criterias.Attachments;

public class AttachmentByObjectId<TEntity> : ACriteria<TEntity> where TEntity : Attachment
{
    private readonly Guid _objectId;

    public AttachmentByObjectId(Guid objectId)
    {
        _objectId = objectId;
    }

    public override Expression<Func<TEntity, bool>> Build()
    {
        return a => a.ObjectId == _objectId;
    }
}

public class AttachmentByObjectId : AttachmentByObjectId<Attachment>
{
    public AttachmentByObjectId(Guid objectId) : base(objectId)
    {
    }
}