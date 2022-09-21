import { TodoItemProps } from './types';
import './styles.css';
import { ChangeEvent, useRef, useState } from 'react';

function TodoItem({
    id, 
    description, 
    isComplete, 
    createdDate, 
    modifiedDate, 
    onEditClick,
    onDeleteClick,
    onStatusChange,
}: TodoItemProps) {
    const [editMode, setEditMode] = useState(false);
    const [descriptionInputValue, setDescriptionValue] = useState(description);
    const inputRef = useRef<HTMLInputElement>(null);
    const [buttonEnabled, setButtonEnabled] = useState(false);

    const editClickHandler = () => {
        if (isComplete) {
            return;
        }

        if (editMode && inputRef?.current?.value) {
            onEditClick(id, inputRef?.current?.value);
        }

        setEditMode(!editMode);
    }

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const inputedValue = e.target.value;
        setDescriptionValue(inputedValue);
        setButtonEnabled(!!inputedValue); 
    }

    return (<>
        <div className='todo-item-wrapper'>
            <span className='todo-item-id'>{id}</span>
            <span className='todo-item-description'>{
                editMode ? (
                    <input
                        className='todo-item-description-input'
                        ref={inputRef}
                        type="text"
                        value={descriptionInputValue}
                        onChange={handleChange}
                     />)
                    : (<span title={description}> { description }</span>)
            }</span>
            <span className='todo-item-created'>{createdDate.toLocaleString()}</span>
            <span className='todo-item-modified'>{modifiedDate.toLocaleString()}</span>
            <span 
                style={{'color': isComplete ? 'red' : 'green'}} 
                className={'todo-item-complete cursor-pointer'}
                onClick={() => onStatusChange(id, isComplete)}
                >
                    {isComplete ? 'Complete' : 'To do'}
            </span>
            <span
                title={isComplete ? 'Complete task is readonly' : ''}
                style={{'color': editMode ? 'orange' : 'green'}} 
                className={`${isComplete ? ' edit-disable' : 'cursor-pointer'}`}
                onClick={() => editClickHandler()}
            >
                {
                    editMode ?
                    (buttonEnabled ? 'Save' : 'Undo') :
                    'Edit' 
                }
            </span>
            <span
                style={{'color': 'red'}}
                onClick={() => onDeleteClick(id)}
                className={'cursor-pointer'}
                >
                Delete
            </span>
        </div>
    </>);
}

export default TodoItem;
