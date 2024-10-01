using System;

using SilentNotary.Common;

namespace SBN.Domain.Models {
    /// <summary>
    ///  Message sending storage model
    /// </summary>
    public class MessageHistory : IMessageResult {
        public string Body { get; set; }
        public string Info { get; set; }
        public bool Socceed { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Identity { get; set; }
    }
}