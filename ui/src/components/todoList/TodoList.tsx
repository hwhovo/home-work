import { useEffect, useState } from "react";
import { addTodo, deleteTodoItem, editTodoDescription, editTodoStatus, getTodoList } from "../../services/fetcherService";
import AddTodoItem from "../addTodoItem";
import Loader from "../loader";
import TodoItem from "../todoItem";
import TodoListHeader from "../todoListHeader";
import { SortColumn } from "../todoListHeader/types";
import "./styles.css";
import { TodoItemType } from "./types";

function TodoList() {
    const [todoList, setTodoList] = useState<TodoItemType[]>([]);
    const [sortColumnName, setSortColumn] = useState<SortColumn>();
    const [sortByAsc, setSortByAsc] = useState<boolean>();
    const [isLoading, setIsLoading] = useState<boolean>();

    useEffect(() => {
        setIsLoading(true);
        getTodoList().then(todoListData => {
            setTodoList(todoListData);
        }).catch(console.error)
            .finally(() => { setIsLoading(false) });
    }, [])

    const addTask = (description: string) => {
        setIsLoading(true);
        addTodo({ description }).then(addedTodo => {
            setTodoList([...todoList, addedTodo]);
        }).catch(console.error)
            .finally(() => { setIsLoading(false) });
    };

    const onItemEdit = (index: number, id: number, description: string) => {
        setIsLoading(true);
        editTodoDescription(id, { description }).then(response => {
            const tempData = [...todoList];
            tempData[index] = response;
            setTodoList(tempData);
        }).catch(console.error)
            .finally(() => { setIsLoading(false) });
    };

    const onItemDelete = (index: number, id: number) => {
        setIsLoading(true);
        deleteTodoItem(id).then(() => {
            const tempData = [...todoList];
            tempData.splice(index, 1);
            setTodoList(tempData);
        }).catch(console.error)
            .finally(() => { setIsLoading(false) });
    };

    const sortTodoList = (column: SortColumn) => {
        const sortByAscValue = column === sortColumnName ? !sortByAsc : true;
        setSortByAsc(sortByAscValue);
        setSortColumn(column);

        const sortedData = todoList.sort((a, b) => {
            // @ts-ignore
            if (a[column.toString()] > b[column.toString()]) {
                return sortByAscValue ? 1 : -1;
            } else {
                return sortByAscValue ? -1 : 1;
            }
        });
        setTodoList(sortedData);
    };

    const onItemStatusChange = (index: number, id: number, isComplete: boolean) => {
        setIsLoading(true);
        editTodoStatus(id, { isComplete: !isComplete }).then(() => {
            const tempData = [...todoList];
            tempData[index].isComplete = !tempData[index].isComplete;
            setTodoList(tempData);
        }).catch(console.error)
        .finally(() => { setIsLoading(false) });
    }

    return (<>
        {isLoading ? <Loader /> : <></>
    }
                <div className="todo-list-wrapper">
                    <AddTodoItem addTaskSuccess={addTask} />
                    <TodoListHeader sortTodo={sortTodoList} />
                    <div className={"todo-list-container"}>
                        {
                            todoList.map((item, index) =>
                            (<TodoItem
                                {...item}
                                key={`${item.id}-${index}`}
                                onEditClick={(id, description) => onItemEdit(index, id, description)}
                                onDeleteClick={id => onItemDelete(index, id)}
                                onStatusChange={(id, isComplete) => onItemStatusChange(index, id, isComplete)}
                            />))
                        }
                    </div>
                </div>
            </>);
}

        export default TodoList;
