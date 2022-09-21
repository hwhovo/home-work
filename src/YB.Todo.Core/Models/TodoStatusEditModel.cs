using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YB.Todo.Core.Models
{
    public class TodoStatusEditModel
    {
        [Required]
        public bool IsComplete { get; set; }
    }
}
