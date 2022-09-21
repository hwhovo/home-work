import './styles.css';
import { SortColumn, TodoHeaderProps } from './types';

function TodoListHeader({ sortTodo }: TodoHeaderProps) {

    return (<>
    <div className="todo-header-wrapper">
        <span>Id</span>
        <span className='description-header' onClick={() => sortTodo(SortColumn.description)} >Description</span>
        <span onClick={() => sortTodo(SortColumn.createdDate)} style={{cursor: 'pointer'}}>Created Date</span>
        <span>Modified Date</span>
        <span>Status</span>
        <span>Actions</span>
    </div>
    </>);
}

export default TodoListHeader;
