using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Core.Entities;

namespace TaskFlow.Domain.Entities
{
    public class Comment : EntityBase<Guid>
    {
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Comment() : base() { }

        public Comment(string text)
            : base(Guid.NewGuid())
        {
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }

        // Método de domínio, se houver
        public void Edit(string newText)
        {
            Text = newText;
            // ...
        }
    }
}
