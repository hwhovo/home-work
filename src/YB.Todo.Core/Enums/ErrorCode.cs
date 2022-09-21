using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YB.Todo.Core
{
    public enum ErrorCode
    {
        DESCRIPTION_IS_EMPTY,
        TODO_NOT_FOUND,
        COMPLETED_TASK_IS_READ_ONLY,
        YOU_CAN_NOT_CHANGE_DESCRIPTION_AND_STATUS_SAME_TIME,
        SOMETHING_WENT_WRONG
    }
}
