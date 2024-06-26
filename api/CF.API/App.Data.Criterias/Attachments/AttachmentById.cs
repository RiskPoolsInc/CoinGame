using System.Linq.Expressions;
using App.Data.Criterias.Core;

namespace App.Data.Criterias.Attachments;

public class AttachmentById<TEntity> : ACriteria<TEntity> where TEntity : Attachment
{
    private readonly Guid _Id;

    public AttachmentById(Guid Id)
    {
        _Id = Id;
    }

    public override Expression<Func<TEntity, bool>> Build()
    {
        return a => a.Id == _Id;
    }
}

public class AttachmentById : AttachmentById<Attachment>
{
    public AttachmentById(Guid Id) : base(Id)
    {
    }
}