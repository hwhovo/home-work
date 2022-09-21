import { ChangeEvent, useRef, useState } from "react";
import { AddTodoItemProps } from "./types";
import './styles.css';

function AddTodoItem({ addTaskSuccess }: AddTodoItemProps) {
    const inputRef = useRef<HTMLInputElement>(null);
    const [buttonEnabled, setButtonEnabled] = useState(false);

    const addClick = () => {
        const inputedValue = inputRef?.current?.value;

        if (inputedValue) {
            addTaskSuccess(inputedValue);
        }
    };

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const inputedValue = e.target.value;

        setButtonEnabled(!!inputedValue); 
    }

    return (<>
        <div>
            <input
                className="add-input"
                ref={inputRef}
                type="text"
                onChange={handleChange}
                placeholder="Task description" />
            <button 
                disabled={!buttonEnabled} 
                style={{background: buttonEnabled ? '#282c34' : 'silver', cursor: buttonEnabled ? 'pointer' : 'default'}} 
                className='add-button' 
                onClick={addClick}
            >
                Add task
            </button>
        </div>
    </>);
}

export default AddTodoItem;
