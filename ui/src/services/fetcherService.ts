import { TodoItemType } from '../components/todoList/types';
import { TodoRequestModel, ResponseModel, StatusUpdateTodoModel } from './types';

const baseUrl = 'https://localhost:7178/todo';
const headers = new Headers({ 'Content-Type': 'application/json' });

export async function addTodo(todoItem: TodoRequestModel): Promise<TodoItemType> {
    return fetch(`${baseUrl}`, {
        method: 'POST',
        headers,
        body: JSON.stringify(todoItem),
    }).then(response => response.json())
    .then((response: ResponseModel<TodoItemType>) => ({ 
        ...response.result, 
        createdDate: new Date(response.result.createdDate), 
        modifiedDate: new Date(response.result.modifiedDate) 
    }));
};

export async function getTodoList(description?: string): Promise<TodoItemType[]> {
    const filter = description ? `?description=${description}` : '';
    return fetch(`${baseUrl}${filter}`, {
        method: 'GET',
        headers,
    }).then(response => response.json())
    .then((response: ResponseModel<TodoItemType[]>) => response.result.map(item => 
    ({ 
        ...item, 
        createdDate: new Date(item.createdDate), 
        modifiedDate: new Date(item.modifiedDate) 
    })));
};

export async function editTodoDescription(id: number, data: TodoRequestModel): Promise<TodoItemType> {
    return fetch(`${baseUrl}/${id}`, {
        method: 'PUT',
        headers,
        body: JSON.stringify(data),
    }).then(response => response.json())
    .then((response: ResponseModel<TodoItemType>) => ({ 
        ...response.result, 
        createdDate: new Date(response.result.createdDate), 
        modifiedDate: new Date(response.result.modifiedDate) 
    }));
};

export async function editTodoStatus(id: number, data: StatusUpdateTodoModel): Promise<void> {
    return fetch(`${baseUrl}/${id}/status`, {
        method: 'PATCH',
        headers,
        body: JSON.stringify(data),
      }).then(response => { if (response.status >= 400) throw new Error(); });
};

export async function deleteTodoItem(id: number): Promise<void> {
    return fetch(`${baseUrl}/${id}`, {
        method: 'DELETE',
        headers,
      }).then(response => { if (response.status >= 400) throw new Error(); });
};