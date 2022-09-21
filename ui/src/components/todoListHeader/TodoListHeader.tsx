import './styles.css';
import { sortColumn, todoHeaderProps } from './types';

function TodoListHeader({ sortTodo }: todoHeaderProps) {

    return (<>
    <div className="todo-header-wrapper">
        <span>Id</span>
        <span className='description-header' onClick={() => sortTodo(sortColumn.description)} >Description</span>
        <span onClick={() => sortTodo(sortColumn.createdDate)} style={{cursor: 'pointer'}}>Created Date</span>
        <span>Modified Date</span>
        <span>Status</span>
        <span>Actions</span>
    </div>
    </>);
}

export default TodoListHeader;
