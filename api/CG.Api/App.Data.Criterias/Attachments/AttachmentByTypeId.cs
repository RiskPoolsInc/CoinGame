using System.Linq.Expressions;
using App.Data.Criterias.Core;

namespace App.Data.Criterias.Attachments;

public class AttachmentByTypeId<TAttachment> : ACriteria<TAttachment> where TAttachment : Attachment
{
    private readonly int _typeId;

    public AttachmentByTypeId(int typeId)
    {
        _typeId = typeId;
    }

    public override Expression<Func<TAttachment, bool>> Build()
    {
        return a => a.ObjectTypeId == _typeId;
    }
}